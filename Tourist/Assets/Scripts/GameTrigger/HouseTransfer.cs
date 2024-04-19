using UnityEngine;
using DataNamespace;
using UnityEngine.Tilemaps;

public class HouseTransfer : MonoBehaviour
{
    [SerializeField] DialogBox dialogBox;
    [SerializeField] GameObject gridHouse;
    [SerializeField] GameObject[] gridMaps;

    private Vector2[] houseBounds;
    private Vector2[] gridBounds;

    private Vector3 playerPosHouse = new Vector3(-30.5f, -2, 0);
    private Vector3 playerPosGrid = new Vector3(-3, -1, 0);

    private CameraController cameraController;
    private DialogBoxData dialogData;

    private string playerTag = "Player";
    private int id = DataHolder.IdLocation;

    private static bool hasBeenCalled = false;

    public static bool IsHome { get; set; }

    private Vector2[] FindTilemapBounds(GameObject grids)
    {
        // Получаем tilemap для поиска границ
        Tilemap[] tilemaps = grids.GetComponentsInChildren<Tilemap>();
        Tilemap tilemap = System.Array.Find(tilemaps, tilemap => tilemap.name == "Ground Collision");

        // Левая нижняя и правая верхняя граница
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int bottomLeftTilePos = new Vector3Int(bounds.xMax, bounds.yMax, 0);
        Vector3Int topRightTilePos = new Vector3Int(bounds.xMin, bounds.yMin, 0);

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                bottomLeftTilePos.x = Mathf.Min(bottomLeftTilePos.x, pos.x);
                bottomLeftTilePos.y = Mathf.Min(bottomLeftTilePos.y, pos.y);

                topRightTilePos.x = Mathf.Max(topRightTilePos.x, pos.x);
                topRightTilePos.y = Mathf.Max(topRightTilePos.y, pos.y);
            }
        }

        /*
        // Устанавливаем цвет и флаги для угловых тайлов
        tilemap.SetTileFlags(bottomLeftTilePos, TileFlags.None);
        tilemap.SetColor(bottomLeftTilePos, UnityEngine.Color.black);

        tilemap.SetTileFlags(topRightTilePos, TileFlags.None);
        tilemap.SetColor(topRightTilePos, UnityEngine.Color.black);
        */

        // Корректируем значение
        topRightTilePos += Vector3Int.one;

        // Переводим координату tilemap в глобальную координату
        Vector2 bottomLeftWorldPos = tilemap.CellToWorld(bottomLeftTilePos);
        Vector2 topRightWorldPos = tilemap.CellToWorld(topRightTilePos);

        return new Vector2[] { bottomLeftWorldPos, topRightWorldPos };
    }

    private void LoadData()
    {
        // Получаем все Tilemap из дочерних объектов gridMaps
        Tilemap[] tilemaps = gridMaps[4].GetComponentsInChildren<Tilemap>();
        TilemapSaveLoad.LoadTilemapData(tilemaps);
    }

    private void Start()
    {
        // Устанавливаем первоначальное значение
        IsHome = true;
        cameraController = Camera.main.GetComponent<CameraController>();
        dialogData = DataLoader.GetDialogBoxData(gameObject.tag);

        // Включаем необходимую карту
        if (id > 4) id = 4;  
        gridMaps[id].SetActive(true);

        // Загружаем карту из бд, если необходимо
        if (id == 4 && !hasBeenCalled)
        {
            LoadData();
            hasBeenCalled = true;
        }

        // Получаем границы дома и карты
        houseBounds = FindTilemapBounds(gridHouse);
        gridBounds = FindTilemapBounds(gridMaps[id]);

        // Устанавлием границы локации
        cameraController.MovePlayer(houseBounds);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            bool isNotifyStart = DataHolder.IsNotifyStart;
            bool isAfterRoute = DataHolder.IsAfterRoute;

            if (!isNotifyStart && IsHome)
            {
                dialogBox.StartDialogBox(dialogData.TextBefore);
            }
            else if (isAfterRoute && IsHome)
            {
                dialogBox.StartDialogBox(dialogData.TextAfter);
            }
            else ChangeGrid();        
        }
    }

    // Метод для включения/выключения локации
    private void ChangeGrid()
    {
        if (!IsHome)
        {
            // Если игрок входит в дом
            IsHome = true;
            cameraController.MovePlayer(houseBounds, playerPosHouse);
        }
        else
        {
            // Если игрок выходит из дома
            IsHome = false;
            cameraController.MovePlayer(gridBounds, playerPosGrid);
        }
    }
}