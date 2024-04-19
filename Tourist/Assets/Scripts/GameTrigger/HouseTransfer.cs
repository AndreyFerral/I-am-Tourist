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
        // �������� tilemap ��� ������ ������
        Tilemap[] tilemaps = grids.GetComponentsInChildren<Tilemap>();
        Tilemap tilemap = System.Array.Find(tilemaps, tilemap => tilemap.name == "Ground Collision");

        // ����� ������ � ������ ������� �������
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
        // ������������� ���� � ����� ��� ������� ������
        tilemap.SetTileFlags(bottomLeftTilePos, TileFlags.None);
        tilemap.SetColor(bottomLeftTilePos, UnityEngine.Color.black);

        tilemap.SetTileFlags(topRightTilePos, TileFlags.None);
        tilemap.SetColor(topRightTilePos, UnityEngine.Color.black);
        */

        // ������������ ��������
        topRightTilePos += Vector3Int.one;

        // ��������� ���������� tilemap � ���������� ����������
        Vector2 bottomLeftWorldPos = tilemap.CellToWorld(bottomLeftTilePos);
        Vector2 topRightWorldPos = tilemap.CellToWorld(topRightTilePos);

        return new Vector2[] { bottomLeftWorldPos, topRightWorldPos };
    }

    private void LoadData()
    {
        // �������� ��� Tilemap �� �������� �������� gridMaps
        Tilemap[] tilemaps = gridMaps[4].GetComponentsInChildren<Tilemap>();
        TilemapSaveLoad.LoadTilemapData(tilemaps);
    }

    private void Start()
    {
        // ������������� �������������� ��������
        IsHome = true;
        cameraController = Camera.main.GetComponent<CameraController>();
        dialogData = DataLoader.GetDialogBoxData(gameObject.tag);

        // �������� ����������� �����
        if (id > 4) id = 4;  
        gridMaps[id].SetActive(true);

        // ��������� ����� �� ��, ���� ����������
        if (id == 4 && !hasBeenCalled)
        {
            LoadData();
            hasBeenCalled = true;
        }

        // �������� ������� ���� � �����
        houseBounds = FindTilemapBounds(gridHouse);
        gridBounds = FindTilemapBounds(gridMaps[id]);

        // ����������� ������� �������
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

    // ����� ��� ���������/���������� �������
    private void ChangeGrid()
    {
        if (!IsHome)
        {
            // ���� ����� ������ � ���
            IsHome = true;
            cameraController.MovePlayer(houseBounds, playerPosHouse);
        }
        else
        {
            // ���� ����� ������� �� ����
            IsHome = false;
            cameraController.MovePlayer(gridBounds, playerPosGrid);
        }
    }
}