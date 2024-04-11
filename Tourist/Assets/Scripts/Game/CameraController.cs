using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Скорость передвижения камеры
    [SerializeField] float movingSpeed;

    // Игровой объект, за которым следит камера
    private GameObject player;

    // Значения границы камеры
    private Vector2 minPos;
    private Vector2 maxPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MovePlayer(Vector2[] bounds, Vector3 playerCoord = default)
    {
        if (playerCoord != default)
        {
            // Перемещаем игрока, устанавливаем камере позицию игрока
            player.transform.position = playerCoord;
            transform.position = GetPlayerCoord();
        }

        minPos = bounds[0];
        maxPos = bounds[1];
    }

    void FixedUpdate()
    {
        if (player.transform)
        {
            // Получаем координаты игрока
            Vector3 playerPosition = GetPlayerCoord();

            // Вычисляем размеры камеры
            float cameraHalfHeight = Camera.main.orthographicSize;
            float cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

            // Ограничиваем координаты камеры вокруг игрока с учетом границ
            float clampedX = Mathf.Clamp(playerPosition.x, minPos.x + cameraHalfWidth, maxPos.x - cameraHalfWidth);
            float clampedY = Mathf.Clamp(playerPosition.y, minPos.y + cameraHalfHeight, maxPos.y - cameraHalfHeight);

            // Устанавливаем новую позицию камеры посередине игрока с учетом границ
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
    
    Vector3 GetPlayerCoord()
    {
        Vector3 playerCoord = new Vector3()
        {
            x = player.transform.position.x,
            y = player.transform.position.y,
            z = player.transform.position.z - 10
        };
        return playerCoord;
    }
}