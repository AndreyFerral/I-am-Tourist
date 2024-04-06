using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DataNamespace
{
    [System.Serializable]
    public class LevelsData
    {
        public List<TilemapData> tilemapDataList;
        public List<ObjectData> objectDataList;

        public LevelsData(List<TilemapData> tilemapData, List<ObjectData> gameObjects)
        {
            tilemapDataList = tilemapData;
            objectDataList = gameObjects;
        }
    }

    [System.Serializable]
    public class TilemapData
    {
        public string name;
        public List<TileData> tilesData;
    }

    [System.Serializable]
    public class TileData
    {
        public int xPos;
        public int yPos;
        public string tileName;
    }

    [System.Serializable]
    public class ObjectData
    {
        public float xPos;
        public float yPos;
        public string objectName;
    }
}