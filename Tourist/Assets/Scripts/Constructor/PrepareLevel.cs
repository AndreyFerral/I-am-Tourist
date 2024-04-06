using DataNamespace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PrepareLevel : MonoBehaviour
{
    [SerializeField] Tilemap grassTilemap;
    [SerializeField] Tilemap collisionGroundTilemap;

    [SerializeField] Tile standartTile;
    [SerializeField] Tile[] barrierTiles;

    private Vector2Int minCameraPos = new Vector2Int(-15, -6);
    [SerializeField] Vector2Int maxCameraValue;

    void Start()
    {
        // Строим уровень
        FillTilemapArea();
        TraverseBorder();
        SetEdges();

        // Передаем значения в TilemapManager и CameraJoystick
        TilemapManager.MaxCameraValue = maxCameraValue;
        CameraJoystick.MaxCameraValue = maxCameraValue;

        Debug.Log("Уровень подготовлен");
    }

    void FillTilemapArea()
    {
        // Рисовать траву не по границе, а на add клеток
        int add = 3;

        for (int x = -add; x < maxCameraValue.x + add; x++)
        {
            for (int y = -add; y < maxCameraValue.y + add; y++)
            {
                Vector3Int tilePosition = new Vector3Int(minCameraPos.x + x, minCameraPos.y + y, 0);
                grassTilemap.SetTile(tilePosition, standartTile);
            }
        }
    }

    // Метод прохода по границе
    void TraverseBorder()
    {
        // Проходимся по верхней и нижней границе
        for (int x = minCameraPos.x; x < minCameraPos.x + maxCameraValue.x; x++)
        {
            Vector3Int topTopPos = new Vector3Int(x, minCameraPos.y + 1, 0);
            Vector3Int topDownPos = new Vector3Int(x, minCameraPos.y, 0);

            Vector3Int downTopPos = new Vector3Int(x, minCameraPos.y + maxCameraValue.y - 1, 0);
            Vector3Int downDownPos = new Vector3Int(x, minCameraPos.y + maxCameraValue.y - 2, 0);

            collisionGroundTilemap.SetTile(topTopPos, barrierTiles[2]);
            collisionGroundTilemap.SetTile(topDownPos, barrierTiles[3]);

            collisionGroundTilemap.SetTile(downTopPos, barrierTiles[2]);
            collisionGroundTilemap.SetTile(downDownPos, barrierTiles[3]);
        }

        // Проходимся по левой и правой границе
        for (int y = minCameraPos.y + 1; y < minCameraPos.y + maxCameraValue.y - 1; y++)
        {
            Vector3Int leftLeftPos = new Vector3Int(minCameraPos.x, y, 0);
            Vector3Int leftRightPos = new Vector3Int(minCameraPos.x + 1, y, 0);

            Vector3Int rightLeftPos = new Vector3Int(minCameraPos.x + maxCameraValue.x - 2, y, 0);
            Vector3Int rightRightPos = new Vector3Int(minCameraPos.x + maxCameraValue.x - 1, y, 0);

            collisionGroundTilemap.SetTile(leftLeftPos, barrierTiles[0]);
            collisionGroundTilemap.SetTile(leftRightPos, barrierTiles[1]);

            collisionGroundTilemap.SetTile(rightLeftPos, barrierTiles[0]);
            collisionGroundTilemap.SetTile(rightRightPos, barrierTiles[1]);
        }
    }

    void SetEdges() 
    {
        Vector3Int leftUp = new Vector3Int(minCameraPos.x, minCameraPos.y + maxCameraValue.y - 1, 0);
        Vector3Int leftUpHelp = new Vector3Int(minCameraPos.x + 1, minCameraPos.y + maxCameraValue.y - 2, 0);
        Vector3Int leftDown = new Vector3Int(minCameraPos.x, minCameraPos.y, 0);
        Vector3Int leftDownHelp = new Vector3Int(minCameraPos.x + 1, minCameraPos.y + 1, 0);

        Vector3Int rightUp = new Vector3Int(minCameraPos.x + maxCameraValue.x - 1, minCameraPos.y + maxCameraValue.y - 1, 0);
        Vector3Int rightUpHelp = new Vector3Int(minCameraPos.x + maxCameraValue.x - 2, minCameraPos.y + maxCameraValue.y - 2, 0);
        Vector3Int rightDown = new Vector3Int(minCameraPos.x + maxCameraValue.x - 1, minCameraPos.y, 0);
        Vector3Int rightDownHelp = new Vector3Int(minCameraPos.x - 1 + maxCameraValue.x - 1, minCameraPos.y + 1, 0);

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