using DataNamespace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PrepareLevel : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap collisionGroundTilemap;
    [SerializeField] int sizeX, sizeY;

    [SerializeField] Tile standartTile;
    [SerializeField] Tile[] barrierTiles;

    private Vector3Int startingPosition;

    void Start()
    {
        startingPosition = new Vector3Int(-15, -6, 0); // Начальная позиция
        FillTilemapArea();
        TraverseBorder();
        SetEdges();
        Debug.Log("Уровень подготовлен");
    }

    void FillTilemapArea()
    {
        // Рисовать траву не по границе, а расширенно на add клеток
        int add = 3;

        for (int x = -add; x < sizeX + add; x++)
        {
            for (int y = -add; y < sizeY + add; y++)
            {
                Vector3Int tilePosition = startingPosition + new Vector3Int(x, y, 0);
                groundTilemap.SetTile(tilePosition, standartTile);
            }
        }
    }

    // Метод прохода по границе
    void TraverseBorder()
    {
        // Проходимся по верхней и нижней границе
        for (int x = startingPosition.x; x < startingPosition.x + sizeX; x++)
        {
            Vector3Int topTopPos = new Vector3Int(x, startingPosition.y + 1, 0);
            Vector3Int topDownPos = new Vector3Int(x, startingPosition.y, 0);

            Vector3Int downTopPos = new Vector3Int(x, startingPosition.y + sizeY - 1, 0);
            Vector3Int downDownPos = new Vector3Int(x, startingPosition.y + sizeY - 2, 0);

            collisionGroundTilemap.SetTile(topTopPos, barrierTiles[2]);
            collisionGroundTilemap.SetTile(topDownPos, barrierTiles[3]);

            collisionGroundTilemap.SetTile(downTopPos, barrierTiles[2]);
            collisionGroundTilemap.SetTile(downDownPos, barrierTiles[3]);
        }

        // Проходимся по левой и правой границе
        for (int y = startingPosition.y + 1; y < startingPosition.y + sizeY - 1; y++)
        {
            Vector3Int leftLeftPos = new Vector3Int(startingPosition.x, y, 0);
            Vector3Int leftRightPos = new Vector3Int(startingPosition.x + 1, y, 0);

            Vector3Int rightLeftPos = new Vector3Int(startingPosition.x + sizeX - 2, y, 0);
            Vector3Int rightRightPos = new Vector3Int(startingPosition.x + sizeX - 1, y, 0);

            collisionGroundTilemap.SetTile(leftLeftPos, barrierTiles[0]);
            collisionGroundTilemap.SetTile(leftRightPos, barrierTiles[1]);

            collisionGroundTilemap.SetTile(rightLeftPos, barrierTiles[0]);
            collisionGroundTilemap.SetTile(rightRightPos, barrierTiles[1]);
        }
    }

    void SetEdges() 
    {
        Vector3Int leftUp = new Vector3Int(startingPosition.x, startingPosition.y + sizeY - 1, 0);
        Vector3Int leftUpHelp = new Vector3Int(startingPosition.x + 1, startingPosition.y + sizeY - 2, 0);
        Vector3Int leftDown = new Vector3Int(startingPosition.x, startingPosition.y, 0);
        Vector3Int leftDownHelp = new Vector3Int(startingPosition.x + 1, startingPosition.y + 1, 0);

        Vector3Int rightUp = new Vector3Int(startingPosition.x + sizeX - 1, startingPosition.y + sizeY - 1, 0);
        Vector3Int rightUpHelp = new Vector3Int(startingPosition.x + sizeX - 2, startingPosition.y + sizeY - 2, 0);
        Vector3Int rightDown = new Vector3Int(startingPosition.x + sizeX - 1, startingPosition.y, 0);
        Vector3Int rightDownHelp = new Vector3Int(startingPosition.x - 1 + sizeX - 1, startingPosition.y + 1, 0);

        collisionGroundTilemap.SetTile(leftUp, barrierTiles[4]);
        collisionGroundTilemap.SetTile(leftDown, barrierTiles[6]);
        collisionGroundTilemap.SetTile(rightUp, barrierTiles[5]);
        collisionGroundTilemap.SetTile(rightDown, barrierTiles[7]);

        collisionGroundTilemap.SetTile(leftUpHelp, barrierTiles[9]);
        collisionGroundTilemap.SetTile(leftDownHelp, barrierTiles[11]);
        collisionGroundTilemap.SetTile(rightUpHelp, barrierTiles[8]);
        collisionGroundTilemap.SetTile(rightDownHelp, barrierTiles[10]);
    }
}