using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public static class TilemapManager
{
    private static Stack<TileBase[,]> tileStates = new Stack<TileBase[,]>();
    private static Stack<Tilemap> tilemapStack = new Stack<Tilemap>();
    private static Stack<List<GameObject>> gameObjectStack = new Stack<List<GameObject>>();

    private static Vector2Int minCameraPos;
    private static Vector2Int maxCameraPos;

    // Получаем значения границ мира у скрипта PrepareLevel.cs
    public static Vector2Int MinCameraPos { set => minCameraPos = value; }
    public static Vector2Int MaxCameraPos { set => maxCameraPos = value; }

    private static TileBase[,] GetCurrentTiles(Tilemap tilemap)
    {
        BoundsInt bounds = new BoundsInt((Vector3Int)minCameraPos, new Vector3Int(maxCameraPos.x, maxCameraPos.y, 1));
        TileBase[,] currentTiles = new TileBase[bounds.size.x, bounds.size.y];

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            currentTiles[pos.x - minCameraPos.x, pos.y - minCameraPos.y] = tilemap.GetTile(pos);
        }

        return currentTiles;
    }

    public static void SaveState(Tilemap tilemap, List<GameObject> gameObjects)
    {
        // Получение текущего состояния тайлов
        TileBase[,] currentTiles = GetCurrentTiles(tilemap);

        // Сохраняем значения
        tileStates.Push(currentTiles);
        tilemapStack.Push(tilemap);
        gameObjectStack.Push(new List<GameObject>(gameObjects));

        Debug.Log("Tilemap сохранен");
    }

    public static void CorrectState(Tilemap tilemap)
    {
        // Получение текущего состояния тайлов
        TileBase[,] currentTiles = GetCurrentTiles(tilemap); 

        if (tileStates.Count > 0 && AreTilemapsEqual(currentTiles, tileStates.Peek()))
        {
            // Если они одинаковы, то удаляем 
            tileStates.Pop();
            tilemapStack.Pop();
            gameObjectStack.Pop();

            Debug.Log("Tilemap скорректирован");
        }
    }

    private static bool AreTilemapsEqual(TileBase[,] curTiles, TileBase[,] lastTiles)
    {
        for (int x = 0; x < curTiles.GetLength(0); x++)
        {
            for (int y = 0; y < curTiles.GetLength(1); y++)
            {
                if (curTiles[x, y] != lastTiles[x, y])
                {
                    return false; // Не все блоки одинаковы
                }
            }
        }

        return true; // Все блоки одинаковы
    }

    public static List<GameObject> ReturnState(List<GameObject> gameObjects)
    {
        if (tileStates.Count > 0 && tilemapStack.Count > 0)
        {
            Debug.Log("Восстанавливаем tilemap");

            TileBase[,] originalTiles = tileStates.Pop();
            Tilemap targetTilemap = tilemapStack.Pop();
            List<GameObject> lastList = gameObjectStack.Pop();

            // Удаление всех объектов из gameObjects, которые отсутствуют в lastList
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (!lastList.Contains(gameObjects[i]))
                {
                    Debug.Log("Удаляем " + gameObjects[i].name);
                    Object.DestroyImmediate(gameObjects[i]);
                }
            }
            
            BoundsInt bounds = new BoundsInt((Vector3Int)minCameraPos, new Vector3Int(maxCameraPos.x, maxCameraPos.y, 1));

            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                targetTilemap.SetTile(pos, originalTiles[pos.x - minCameraPos.x, pos.y - minCameraPos.y]);
            }

            return lastList;
        }
        else
        {
            Debug.LogWarning("Невозможно восстановить tilemap");
            return new List<GameObject>();
        }
    }
}