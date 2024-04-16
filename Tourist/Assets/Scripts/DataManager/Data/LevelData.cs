using System.Collections.Generic;

namespace DataNamespace
{
    [System.Serializable]
    public class LevelData
    {
        public string nameMap;
        public string descriptionMap;
        public int heightMap;
        public int weightMap;
        public List<TilemapData> tilemapDataList;
        public List<ObjectData> objectDataList;

        public LevelData(string name, string description, int height, int weight, List<TilemapData> tilemapData = default, List<ObjectData> gameObjects = default)
        {
            nameMap = name;
            descriptionMap = description;
            heightMap = height;
            weightMap = weight;
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