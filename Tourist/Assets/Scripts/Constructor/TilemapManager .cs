using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapManager
{
    private static Stack<TileBase[,]> tileStates = new Stack<TileBase[,]>();
    private static Stack<Tilemap> tilemapStack = new Stack<Tilemap>();

    private static Vector2Int minCameraPos;
    private static Vector2Int maxCameraPos;

    public static Vector2Int MinCameraPos { set => minCameraPos = value; }
    public static Vector2Int MaxCameraPos { set => maxCameraPos = value; }

    public static void SaveState(Tilemap tilemap)
    {
        BoundsInt bounds = new BoundsInt((Vector3Int)minCameraPos, new Vector3Int(maxCameraPos.x, maxCameraPos.y, 1));
        TileBase[,] currentTiles = new TileBase[bounds.size.x, bounds.size.y];

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            currentTiles[pos.x - minCameraPos.x, pos.y - minCameraPos.y] = tilemap.GetTile(pos);
        }

        tileStates.Push(currentTiles);
        tilemapStack.Push(tilemap);
    }

    public static void ReturnState()
    {
        if (tileStates.Count > 0 && tilemapStack.Count > 0)
        {
            TileBase[,] originalTiles = tileStates.Pop();
            Tilemap targetTilemap = tilemapStack.Pop();

            BoundsInt bounds = new BoundsInt((Vector3Int)minCameraPos, new Vector3Int(maxCameraPos.x, maxCameraPos.y, 1));

            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                targetTilemap.SetTile(pos, originalTiles[pos.x - minCameraPos.x, pos.y - minCameraPos.y]);
            }
        }
        else
        {
            Debug.LogWarning("Ќевозможно восстановить tilemap: сохраненного состо€ни€");
        }
    }
}