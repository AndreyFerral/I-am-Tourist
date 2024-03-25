using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Linq;

public class LevelConstructor : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap decorGroundTilemap;
    [SerializeField] Tilemap collisionGroundTilemap;

    [SerializeField] Tilemap waterTilemap;
    [SerializeField] Tilemap decorWaterTilemap;
    [SerializeField] Tilemap collisionWaterTilemap;

    // todo убрать [SerializeField], делать через бд возможно

    [SerializeField] Tile[] roadTiles; // Тайлы дороги
    [SerializeField] Tile[] whiteTiles; // Тайлы белой поляны
    [SerializeField] Tile[] greenTiles; // Тайлы темной травы
    [SerializeField] Tile[] waterOnStandartTiles; // Тайлы воды на стандартной траве
    [SerializeField] Tile[] waterOnWhiteTiles; // Тайлы воды на белой поляне

    [SerializeField] Tile[] fenceTiles; // Тайлы забора
    [SerializeField] Tile[] bushTiles; // Тайлы куста

    [SerializeField] Tile[] treeTiles; // Тайлы дерева
    [SerializeField] Tile[] stumpTiles; // Тайлы пня дерева
    [SerializeField] Tile[] rockOnGrassTiles1; // Тайлы камня на траве 1
    [SerializeField] Tile[] rockOnGrassTiles2; // Тайлы камня на траве 2 
    [SerializeField] Tile[] rockOnWaterTiles1; // Тайлы камня на воде 1 
    [SerializeField] Tile[] rockOnWaterTiles2; // Тайлы камня на воде 2

    [SerializeField] Tile[] stonesOnGrassTiles; // Камушек на траве несколько вариантов
    [SerializeField] Tile[] stonesOnWaterTiles; // Камушек на воде несколько вариантов
    [SerializeField] Tile[] flowersOnGrassTiles; // Цветы на траве несколько вариантов
    [SerializeField] Tile[] mushroomsOnGrassTiles; // Грибы на траве несколько вариантов
    [SerializeField] Tile[] flowersOnWaterTiles; // Кувшинка на воде несколько вариантов

    // Текущий выбранный слой карты, тайлы карты, главный тайл
    private Tilemap curTilemap;
    private Tile curMainTile;
    private Tile[] curTiles;

    // Текущие и предыдущие координаты тайлов
    private TileBase lastTile;
    private Vector3Int currentPos;
    private Vector3Int prevPos;

    private void SetButon(Tile[] tiles, Tilemap tilemap)
    {
        if (tiles.Length > 4) curMainTile = tiles[4];
        curTiles = tiles;
        curTilemap = tilemap;
    }

    public void SetRoad() => SetButon(roadTiles, groundTilemap);
    public void SetWhiteGrass() => SetButon(whiteTiles, groundTilemap);
    public void SetBlackGrass() => SetButon(greenTiles, groundTilemap);
    public void SetStandartWater() => SetButon(waterOnStandartTiles, waterTilemap);
    public void SetWhiteWater() => SetButon(waterOnWhiteTiles, waterTilemap);
    public void SetFence() => SetButon(fenceTiles, collisionGroundTilemap);
    public void SetBush() => SetButon(bushTiles, collisionGroundTilemap);

    public void SetTree() => SetButon(treeTiles, collisionGroundTilemap);
    public void SetStump() => SetButon(stumpTiles, collisionGroundTilemap);
    public void SetRockOnGrass1() => SetButon(rockOnGrassTiles1, collisionGroundTilemap);
    public void SetRockOnGrass2() => SetButon(rockOnGrassTiles2, collisionGroundTilemap);
    public void SetRockOnWater1() => SetButon(rockOnWaterTiles1, collisionWaterTilemap);
    public void SetRockOnWater2() => SetButon(rockOnWaterTiles2, collisionWaterTilemap);

    public void SetStonesOnWater() => SetButon(stonesOnWaterTiles, collisionWaterTilemap);
    public void SetStonesOnGrass() => SetButon(stonesOnGrassTiles, collisionGroundTilemap);
    public void SetFlowersOnWater() => SetButon(flowersOnWaterTiles, decorWaterTilemap);
    public void SetFlowersOnGrass() => SetButon(flowersOnGrassTiles, decorGroundTilemap);
    public void SetMushroomsOnGrass() => SetButon(mushroomsOnGrassTiles, decorGroundTilemap);

    void FixedUpdate()
    {
        // Прекращаем работу, если не было выбрано
        if (curTiles == null) return;

        // Не ставим объект при передвижении джойстика
        if (joystick.Horizontal != 0 || joystick.Vertical != 0) return;

        // Не ставим объект при перекрытии UI 
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Не ставим объет, если нет нажатия на экран
        if (!Input.GetMouseButton(0)) return;
        
        // Получить текущую позицию мыши или касания
        prevPos = currentPos;
        currentPos = GetMouseOrTouchPosition();

        if (curTiles.Length == 13)
        {
            SetTile(currentPos);
        }
        else if (curTiles.Length == 10)
        {
            // Два нажатия должны быть рядом с другом другом
            if (prevPos == currentPos || !IsAdjacentCell(prevPos, currentPos)) return;
            // Можно ставить на ячейку, если слой Collision не занят другим
            if (!IsTileSet(currentPos, curTilemap) || !IsTileSet(prevPos, curTilemap)) return;
            // Можно ставить, если нет воды
            if (IsTileSet(currentPos, waterTilemap) || IsTileSet(prevPos, waterTilemap)) return;

            CheckMouseMovement(prevPos, currentPos);         
        }
        else if (curTiles.Length == 4)
        {
            SetObject2x2(currentPos);
        }
        else if (curTiles.Length == 5)
        {
            SetObject1x1(currentPos);
        }
    }

    // Получить позицию мыши или касания и преобразовать ее в координаты Tilemap
    Vector3Int GetMouseOrTouchPosition()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = curTilemap.WorldToCell(worldPos);
        return gridPos;
    }

    bool IsTileSetArea(Vector3Int tilePosition, Tilemap tilemap = null, Tile[] tiles = null, bool isSpecial = false)
    {
        TileBase tempTile;
        Tile[] tempTiles;
        if (tiles == null) tempTiles = curTiles;
        else tempTiles = tiles;

        // Проходим по всем позициям
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Получаем и проверяем текущий тайл из ячейки
                Vector3Int checkPosition = tilePosition + new Vector3Int(x, y, 0);
                if (tilemap == null) tempTile = curTilemap.GetTile(checkPosition);
                else tempTile = tilemap.GetTile(checkPosition);

                if (tiles == null && tilemap != null)
                {
                    if (tempTile != null) return false;
                }
                else if (tiles == null && tilemap == null || isSpecial)
                {
                    // Проверяем совпадение текущего тайла с любым тайлом из массива
                    if (!tempTiles.Contains(tempTile) && tempTile != null)
                        return false;
                }
                else
                {
                    if (tempTile != tempTiles[4]) return false;
                }

            }
        }
        return true;
    }

    bool IsTileSet(Vector3Int tilePosition, Tilemap tilemap = null)
    {
        TileBase currentTile;
        if (tilemap == null) currentTile = curTilemap.GetTile(tilePosition);
        else currentTile = tilemap.GetTile(tilePosition);

        if (tilemap == curTilemap)
        {
            // Если тайл пустой или из этой же группы
            return (curTiles.Contains(currentTile) || !currentTile);
        }
        else if (!tilemap) 
        {
            // Сравниваем текущий тайл с главным тайлом
            return (currentTile == curMainTile);
        }
        else
        {
            // Если находится тайл на слое 
            return (currentTile);
        }
    }

    bool SetTile(Vector3Int tilePosition)
    {
        // Ставим тайл, если только соседний тайл его
        if (!IsTileSetArea(tilePosition) || IsTileSet(tilePosition)) return false;

        // Проверка тайла на поверхности земли
        if (curTilemap == groundTilemap)
        {
            if (curTiles == whiteTiles)
            {
                if (!IsTileSetArea(tilePosition, waterTilemap, waterOnWhiteTiles, true)) return false;
            }
            // Если есть в области тайл воды, прекращаем работу
            else if (!IsTileSetArea(tilePosition, waterTilemap)) return false;
        }
        // Проверка тайла на поверхности воды
        else if (curTilemap == waterTilemap)
        {
            // Если есть в области тайл коллизии, прекращаем работу
            if (!IsTileSetArea(tilePosition, collisionGroundTilemap)) return false;
            // Если 
            else if (curTiles == waterOnWhiteTiles)
            {
                if (!IsTileSetArea(tilePosition, groundTilemap, whiteTiles)) return false;
            }
            // Если есть в области тайл земли, прекращаем работу
            else if (!IsTileSetArea(tilePosition, groundTilemap)) return false;
        }

        // Позиции слева, справа, сверху и снизу тайла
        Vector3Int upPosition = tilePosition + Vector3Int.up;
        Vector3Int downPosition = tilePosition + Vector3Int.down;
        Vector3Int leftPosition = tilePosition + Vector3Int.left;
        Vector3Int rightPosition = tilePosition + Vector3Int.right;

        // Диагональные позиции
        Vector3Int leftUp = leftPosition + Vector3Int.up;
        Vector3Int leftDown = leftPosition + Vector3Int.down;
        Vector3Int rightUp = rightPosition + Vector3Int.up;
        Vector3Int rightDown = rightPosition + Vector3Int.down;

        // Диагональные позиции x2
        Vector3Int left2up2 = tilePosition + Vector3Int.left * 2 + Vector3Int.up * 2;
        Vector3Int left2down2 = tilePosition + Vector3Int.left * 2 + Vector3Int.down * 2;
        Vector3Int right2up2 = tilePosition + Vector3Int.right * 2 + Vector3Int.up * 2;
        Vector3Int right2down2 = tilePosition + Vector3Int.right * 2 + Vector3Int.down * 2;

        // Позиции слева, справа, сверху и снизу тайла x2
        Vector3Int up2 = tilePosition + Vector3Int.up * 2;
        Vector3Int down2 = tilePosition + Vector3Int.down * 2;
        Vector3Int left2 = tilePosition + Vector3Int.left * 2;
        Vector3Int right2 = tilePosition + Vector3Int.right * 2;

        // Позиции шахматного коня по вертикали
        Vector3Int up2left = up2 + Vector3Int.left;
        Vector3Int up2right = up2 + Vector3Int.right;
        Vector3Int down2left = down2 + Vector3Int.left;
        Vector3Int down2right = down2 + Vector3Int.right;

        // Позиции шахматного коня по горизонтали
        Vector3Int left2up = left2 + Vector3Int.up;
        Vector3Int left2down = left2 + Vector3Int.down;
        Vector3Int right2up = right2 + Vector3Int.up;
        Vector3Int right2down = right2 + Vector3Int.down;

        // Запоминаем прошлый тайл на месте главного
        lastTile = curTilemap.GetTile(tilePosition);

        // Ставим главный тайл дороги
        curTilemap.SetTile(tilePosition, curMainTile);

        // Проверяем позиции РЕКУРСИВНО слева, справа, сверху и снизу на 1 клетку
        if (IsTileSet(up2) && !IsTileSet(upPosition)) SetTile(upPosition);
        if (IsTileSet(down2) && !IsTileSet(downPosition)) SetTile(downPosition);
        if (IsTileSet(left2) && !IsTileSet(leftPosition)) SetTile(leftPosition);
        if (IsTileSet(right2) && !IsTileSet(rightPosition)) SetTile(rightPosition);

        // Проверяем позиции РЕКУРСИВНО по ходу шахматного коня по горизонтали
        if (IsTileSet(left2up) && !IsTileSet(leftPosition) && !IsTileSet(upPosition) && !IsTileSet(leftUp))
        {
            if (!SetTile(leftUp)/* && !SetTile(leftPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(left2down) && !IsTileSet(leftPosition) && !IsTileSet(downPosition) && !IsTileSet(leftDown))
        {
            if (!SetTile(leftDown) /*&& !SetTile(leftPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(right2up) && !IsTileSet(rightPosition) && !IsTileSet(upPosition) && !IsTileSet(rightUp))
        {
            if (!SetTile(rightUp) /*&& !SetTile(rightPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(right2down) && !IsTileSet(rightPosition) && !IsTileSet(downPosition) && !IsTileSet(rightDown))
        {
            if (!SetTile(rightDown) /*&& !SetTile(rightPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        // Проверяем позиции РЕКУРСИВНО по ходу шахматного коня по вертикали
        if (IsTileSet(up2left) && !IsTileSet(leftUp) && !IsTileSet(upPosition) && !IsTileSet(leftPosition))
        {
            if (!SetTile(leftPosition) /*&& !SetTile(upPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(up2right) && !IsTileSet(rightUp) && !IsTileSet(upPosition) && !IsTileSet(rightPosition))
        {
            if (!SetTile(rightPosition) /*&& !SetTile(upPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(down2left) && !IsTileSet(leftDown) && !IsTileSet(downPosition) && !IsTileSet(leftPosition))
        {
            if (!SetTile(leftPosition) /*&& !SetTile(downPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(down2right) && !IsTileSet(rightDown) && !IsTileSet(downPosition) && !IsTileSet(rightPosition))
        {
            if (!SetTile(rightPosition) /*&& !SetTile(downPosition)*/)
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        // Проверяем позиции РЕКУРСИВНО по диагональной x2
        if (IsTileSet(left2up2) && !IsTileSet(leftUp) && !IsTileSet(upPosition) && !IsTileSet(leftPosition))
        {
            if (!SetTile(leftUp))
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(right2up2) && !IsTileSet(rightUp) && !IsTileSet(upPosition) && !IsTileSet(rightPosition))
        {
            if (!SetTile(rightUp))
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(left2down2) && !IsTileSet(leftDown) && !IsTileSet(downPosition) && !IsTileSet(leftPosition))
        {
            if (!SetTile(leftDown))
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }
        if (IsTileSet(right2down2) && !IsTileSet(rightDown) && !IsTileSet(downPosition) && !IsTileSet(rightPosition))
        {
            if (!SetTile(rightDown))
            {
                curTilemap.SetTile(tilePosition, lastTile);
                return false;
            }
        }

        // Ставим изображение слева, справа, сверху и снизу дороги (если дорога вертикальная или горизонтальная)
        if (!IsTileSet(upPosition)) curTilemap.SetTile(upPosition, curTiles[1]);
        if (!IsTileSet(downPosition)) curTilemap.SetTile(downPosition, curTiles[7]);
        if (!IsTileSet(leftPosition)) curTilemap.SetTile(leftPosition, curTiles[3]);
        if (!IsTileSet(rightPosition)) curTilemap.SetTile(rightPosition, curTiles[5]);

        // Ставим изображение слева, справа, сверху и снизу дороги (если дорога идет по диагонали)
        if (IsTileSet(leftDown))
        {
            if (!IsTileSet(downPosition))
                curTilemap.SetTile(downPosition, curTiles[9]);
            if (!IsTileSet(leftPosition))
                curTilemap.SetTile(leftPosition, curTiles[12]);
        }
        if (IsTileSet(rightDown))
        {
            if (!IsTileSet(downPosition))
                curTilemap.SetTile(downPosition, curTiles[10]);
            if (!IsTileSet(rightPosition))
                curTilemap.SetTile(rightPosition, curTiles[11]);
        }
        if (IsTileSet(leftUp))
        {
            if (!IsTileSet(upPosition))
                curTilemap.SetTile(upPosition, curTiles[11]);
            if (!IsTileSet(leftPosition))
                curTilemap.SetTile(leftPosition, curTiles[10]);
        }
        if (IsTileSet(rightUp))
        {
            if (!IsTileSet(upPosition))
                curTilemap.SetTile(upPosition, curTiles[12]);
            if (!IsTileSet(rightPosition))
                curTilemap.SetTile(rightPosition, curTiles[9]);
        }

        // Ставим декорации на одну клетку по диагонали от главного тайла
        if (!IsTileSet(leftUp) && curTilemap.GetTile(leftUp) == null)
            curTilemap.SetTile(leftUp, curTiles[0]);
        if (!IsTileSet(leftDown) && curTilemap.GetTile(leftDown) == null)
            curTilemap.SetTile(leftDown, curTiles[6]);
        if (!IsTileSet(rightUp) && curTilemap.GetTile(rightUp) == null)
            curTilemap.SetTile(rightUp, curTiles[2]);
        if (!IsTileSet(rightDown) && curTilemap.GetTile(rightDown) == null)
            curTilemap.SetTile(rightDown, curTiles[8]);

        return true;
    }

    void SetObject1x1(Vector3Int tilePosition)
    {
        // Не ставим, если позиция уже занята
        if (curTilemap.GetTile(tilePosition)) return;
        // Для камней на воде: Не ставим, если нет воды и место занято на слое коллизии
        else if (curTiles == stonesOnWaterTiles || curTiles == flowersOnWaterTiles)
        {
            if (waterTilemap.GetTile(tilePosition) != waterOnStandartTiles[4] || collisionWaterTilemap.GetTile(tilePosition)) return;
            else decorWaterTilemap.SetTile(tilePosition, null);
        }
        // Для остальных: Не ставим, если есть вода и место занято на слое коллизии
        else
        {
            if (IsTileSet(tilePosition, waterTilemap) || collisionGroundTilemap.GetTile(tilePosition)) return;
            else decorGroundTilemap.SetTile(tilePosition, null);
        }

        // Генерируем число от 0 до 4, ставим объект с индексом
        int number = UnityEngine.Random.Range(0, 5);
        curTilemap.SetTile(tilePosition, curTiles[number]);
    }

    void SetObject2x2(Vector3Int tilePosition)
    {
        Vector3Int leftPosition = tilePosition;
        Vector3Int rightPosition = leftPosition + new Vector3Int(1, 0, 0);
        Vector3Int upPosition = leftPosition + new Vector3Int(0, 1, 0);
        Vector3Int upRightPosition = leftPosition + new Vector3Int(1, 1, 0);

        // Не ставим, если позиция уже занята
        if (curTilemap.GetTile(upPosition) || curTilemap.GetTile(upRightPosition) || 
            curTilemap.GetTile(leftPosition) || curTilemap.GetTile(rightPosition)) return;
        // Для камней на воде: Не ставим, если нет воды
        else if (curTiles == rockOnWaterTiles1 || curTiles == rockOnWaterTiles2)
        {
            if (waterTilemap.GetTile(upPosition) != waterOnStandartTiles[4] ||
                waterTilemap.GetTile(upRightPosition) != waterOnStandartTiles[4] ||
                waterTilemap.GetTile(leftPosition) != waterOnStandartTiles[4] ||
                waterTilemap.GetTile(rightPosition) != waterOnStandartTiles[4]) return;
            else
            {
                // Очищаем от декоративных элементов
                decorWaterTilemap.SetTile(upPosition, null);
                decorWaterTilemap.SetTile(upRightPosition, null);
                decorWaterTilemap.SetTile(leftPosition, null);
                decorWaterTilemap.SetTile(rightPosition, null);
            }
        }
        // Для остальных: Не ставим, если есть вода
        else
        {
            if (IsTileSet(upPosition, waterTilemap) || IsTileSet(upRightPosition, waterTilemap) ||
                IsTileSet(leftPosition, waterTilemap) || IsTileSet(rightPosition, waterTilemap)) return;
            else
            {
                // Очищаем от декоративных элементов
                decorGroundTilemap.SetTile(upPosition, null);
                decorGroundTilemap.SetTile(upRightPosition, null);
                decorGroundTilemap.SetTile(leftPosition, null);
                decorGroundTilemap.SetTile(rightPosition, null);
            }
        }

        curTilemap.SetTile(upPosition, curTiles[0]);
        curTilemap.SetTile(upRightPosition, curTiles[1]);
        curTilemap.SetTile(leftPosition, curTiles[2]);
        curTilemap.SetTile(rightPosition, curTiles[3]);
    }

    void CheckMouseMovement(Vector3Int prevPos, Vector3Int curPos)
    {
        int diffX = curPos.x - prevPos.x;
        int diffY = curPos.y - prevPos.y;

        TileBase prevTile = curTilemap.GetTile(prevPos);
        TileBase curTile = curTilemap.GetTile(curPos);

        if (diffX == 1 && diffY == 0)
        {
            // Проверка для ячейки справа
            Debug.Log("->");

            if (curTile == curTiles[0] || curTile == curTiles[1] || curTile == curTiles[2]) return;
            
            if (prevTile != curTiles[9] && prevTile != curTiles[5] && prevTile != curTiles[7] && curTile != curTiles[9] && curTile != curTiles[4] && curTile != curTiles[6])
            {
                if (curTile == null) curTilemap.SetTile(curPos, curTiles[3]);

                if (prevTile != null)
                {
                    if (prevTile == curTiles[0])
                    {
                        curTilemap.SetTile(prevPos, curTiles[4]);
                    }
                    else if (prevTile == curTiles[1])
                    {
                        curTilemap.SetTile(prevPos, curTiles[6]);
                    }
                    else if (prevTile == curTiles[3]) curTilemap.SetTile(prevPos, curTiles[8]);
                }
                else curTilemap.SetTile(prevPos, curTiles[2]);
            }
        }
        else if (diffX == -1 && diffY == 0)
        {
            // Проверка для ячейки слева
            Debug.Log("<-");

            if (curTile == curTiles[0] || curTile == curTiles[1] || curTile == curTiles[3]) return;
            
            if (prevTile != curTiles[9] && prevTile != curTiles[4] && prevTile != curTiles[6] && curTile != curTiles[9] && curTile != curTiles[5] && curTile != curTiles[7])
            {
                if (curTile == null) curTilemap.SetTile(curPos, curTiles[2]);

                if (prevTile != null)
                {
                    if (prevTile == curTiles[0])
                    {
                        curTilemap.SetTile(prevPos, curTiles[5]);
                    }
                    else if (prevTile == curTiles[1])
                    {
                        curTilemap.SetTile(prevPos, curTiles[7]);
                    }
                    else if (prevTile == curTiles[2]) curTilemap.SetTile(prevPos, curTiles[8]);
                }
                else curTilemap.SetTile(prevPos, curTiles[3]);
            }
        }
        else if (diffX == 0 && diffY == 1)
        {
            // Проверка для верхней ячейки
            Debug.Log("up");

            if (curTile == curTiles[1] || curTile == curTiles[2] || curTile == curTiles[3]) return;

            if (prevTile != curTiles[8] && prevTile != curTiles[4] && prevTile != curTiles[5] && curTile != curTiles[8] && curTile != curTiles[6] && curTile != curTiles[7])
            {
                if (curTile == null) curTilemap.SetTile(curPos, curTiles[0]);

                if (prevTile != null)
                {
                    if (prevTile == curTiles[2])
                    {
                        curTilemap.SetTile(prevPos, curTiles[6]);
                    }
                    else if (prevTile == curTiles[3])
                    {
                        curTilemap.SetTile(prevPos, curTiles[7]);
                    }
                    else if (prevTile == curTiles[0]) curTilemap.SetTile(prevPos, curTiles[9]);
                }
                else curTilemap.SetTile(prevPos, curTiles[1]);
            }
        }
        else if (diffX == 0 && diffY == -1)
        {
            // Проверка для нижней ячейки
            Debug.Log("down");

            if (curTile == curTiles[0] || curTile == curTiles[2] || curTile == curTiles[3]) return;

            if (prevTile != curTiles[8] && prevTile != curTiles[6] && prevTile != curTiles[7] && curTile != curTiles[8] && curTile != curTiles[4] && curTile != curTiles[5])
            {
                if (curTile == null) curTilemap.SetTile(curPos, curTiles[1]);

                if (prevTile != null)
                {
                    if (prevTile == curTiles[2])
                    {
                        curTilemap.SetTile(prevPos, curTiles[4]);
                    }
                    else if (prevTile == curTiles[3])
                    {
                        curTilemap.SetTile(prevPos, curTiles[5]);
                    }
                    else if (prevTile == curTiles[1]) curTilemap.SetTile(prevPos, curTiles[9]);       
                }
                else curTilemap.SetTile(prevPos, curTiles[0]);
            }
        }

        /*
        if (decorGroundTilemap.GetTile(prevPos)) decorGroundTilemap.SetTile(prevPos, null);
        if (decorGroundTilemap.GetTile(curPos)) decorGroundTilemap.SetTile(curPos, null);

        if (decorWaterTilemap.GetTile(prevPos)) decorWaterTilemap.SetTile(prevPos, null);
        if (decorWaterTilemap.GetTile(curPos)) decorWaterTilemap.SetTile(curPos, null);
        */
    }

    TileBase GetTile(Vector3Int position) => curTilemap.GetTile(position);
    TileBase GetLeftTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(-1, 0, 0));
    TileBase GetRightTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(1, 0, 0));
    TileBase GetUpTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(0, 1, 0));
    TileBase GetDownTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(0, -1, 0));
    TileBase GetUpLeftTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(-1, 1, 0));
    TileBase GetUpRightTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(1, 1, 0));
    TileBase GetDownLeftTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(-1, -1, 0));
    TileBase GetDownRightTile(Vector3Int position) => curTilemap.GetTile(position + new Vector3Int(1, -1, 0));

    bool IsAdjacentCell(Vector3Int prevPos, Vector3Int currentPos)
    {
        return Mathf.Abs(prevPos.x - currentPos.x) <= 1 && Mathf.Abs(prevPos.y - currentPos.y) <= 1;
    }

    void SetColor(Vector3Int position, Color color)
    {
        curTilemap.SetTileFlags(position, TileFlags.None);
        curTilemap.SetColor(position, color);
    }
}
