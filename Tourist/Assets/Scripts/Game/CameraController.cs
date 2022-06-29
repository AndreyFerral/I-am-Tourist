using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Игровой объект, за которым следит камера
    private GameObject player;
    private string playerTag = "Player";

    // Скорость передвижения камеры
    [SerializeField] float movingSpeed;

    // Значения границы камеры
    public Vector2 minPos;
    public Vector2 maxPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Устанавливаем камере позицию игрока
        transform.position = GetPlayerCoord();
    }

    void FixedUpdate()
    {
        if (player.transform)
        {
            // Получаем координаты игрока
            Vector3 target = GetPlayerCoord();

            // Ограничиваем координаты 
            target.x = Mathf.Clamp(target.x, minPos.x, maxPos.x);
            target.y = Mathf.Clamp(target.y, minPos.y, maxPos.y);

            // Обрабатываем координаты
            Vector3 pos = Vector3.Lerp(
                transform.position, target,
                movingSpeed * Time.fixedDeltaTime);

            // Устанавливаем координаты камере
            transform.position = pos;
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