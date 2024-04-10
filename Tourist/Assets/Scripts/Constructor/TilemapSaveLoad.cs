using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using DataNamespace;
using TileData = DataNamespace.TileData;

public static class TilemapSaveLoad
{
    public static void LoadTilemapData(Tilemap[] tilemaps)
    {
        LevelData levelData = DataLoader.GetLevelData("Тест");
        Debug.Log("Был загружен уровень: " + levelData.nameMap);

        for (int i = 0; i < tilemaps.Length; i++)
        {
            TilemapData tilemapData = levelData.tilemapDataList[i];

            foreach (TileData tileData in tilemapData.tilesData)
            {
                // Устанавливаем новый тайл на Tilemap
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

        LevelData data = new LevelData("Тест", tilemapDataList, objectDataList);
        JsonSaveLoadSystem.AddDataToList(data);
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