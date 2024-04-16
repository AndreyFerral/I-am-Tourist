using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using DataNamespace;
using TileData = DataNamespace.TileData;

public static class TilemapSaveLoad
{
    public static void LoadTilemapData(Tilemap[] tilemaps)
    {
        LevelData levelData = DataLoader.GetLevelData("����");
        Debug.Log("��� �������� �������: " + levelData.nameMap);
        
        // ������� �������/������� �� �������
        foreach (ObjectData objectData in levelData.objectDataList)
        {
            string prefabPath = "Objects/" + objectData.objectName; // ���� � �������
            GameObject prefab = Resources.Load<GameObject>(prefabPath); // �������� ������� �� ��������

            // �������� ���������� �������
            Vector3 position = new Vector3(objectData.xPos, objectData.yPos, 0f); 
            var item = Object.Instantiate(prefab, position, Quaternion.identity);
            item.gameObject.name = objectData.objectName;
        }
        
        // ������� ����� �� ������
        for (int i = 0; i < tilemaps.Length; i++)
        {
            TilemapData tilemapData = levelData.tilemapDataList[i];

            foreach (TileData tileData in tilemapData.tilesData)
            {
                if (i != 0 && tileData.tileName == "Overworld_0") continue;

                // ������������� ����� ���� �� Tilemap
                string tilePath = "Tiles/Ground/" + tileData.tileName;
                TileBase tile = Resources.Load<TileBase>(tilePath);
                Vector3Int position = new Vector3Int(tileData.xPos, tileData.yPos, 0);
                tilemaps[i].SetTile(position, tile);
            }
        }
    }

    public static void SaveTilemapData(List<Tilemap> tilemaps, List<GameObject> gameObjects)
    {
        List<TilemapData> tilemapDataList = new List<TilemapData>();
        List<ObjectData> objectDataList = new List<ObjectData>();

        foreach (Tilemap tilemap in tilemaps)
        {
            TilemapData tilemapData = new TilemapData() {
                name = tilemap.name,
                tilesData = GetTileData(tilemap)
            };
            tilemapDataList.Add(tilemapData);
        }

        foreach (GameObject gameObject in gameObjects)
        {
            Vector3 objectPos = gameObject.transform.position;

            ObjectData tileData = new ObjectData {
                xPos = objectPos.x,
                yPos = objectPos.y,
                objectName = gameObject.name
            };
            objectDataList.Add(tileData);
        }

        // ���� ���� �������� ��������, �� ������������� ��
        LevelData levelData = DataHolder.levelData;
        if (levelData is not null)
        {
            levelData.tilemapDataList = tilemapDataList;
            levelData.objectDataList = objectDataList;
            JsonSaveLoadSystem.AddDataToList(levelData);
        }
        else Debug.Log("������� �� ��� ��������, � �� ��� ������");
    }

    private static List<TileData> GetTileData(Tilemap tilemap)
    {
        List<TileData> tileDataList = new List<TileData>();
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                TileBase tile = tilemap.GetTile(position);
                TileData tileData = new TileData {
                    xPos = position.x,
                    yPos = position.y,
                    tileName = tile.name
                };
                tileDataList.Add(tileData);
            }
        }
        return tileDataList;
    }
}