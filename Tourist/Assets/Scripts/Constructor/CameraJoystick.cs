using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // Скорость передвижения камеры
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    // Область, в которой камера может двигаться
    private static Vector2 minCameraPos = new Vector2(-16.5f, -8);
    private static Vector2 maxCameraValue;

    // Дополнительная переменная для коррекции камеры
    float extensionPercent = 2.5f;

    // Получаем значения границ мира у скрипта PrepareLevel.cs
    public static Vector2Int MaxCameraValue { set => maxCameraValue = value; }

    void FixedUpdate()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        // Нормализация значения при получении двух значений
        Vector3 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector3 newPos = transform.position + moveDirection * movingSpeed * Time.deltaTime;

        // Вычисляем размеры камеры
        float cameraHalfHeight = Camera.main.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        // Вычисляем размер расширения карты вправо, исходя из процента от aspect ratio
        float extensionX = Camera.main.aspect * extensionPercent;

        // Вычисляем min и max для X и Y, учитывая размеры камеры и offsetValue
        float minX = minCameraPos.x + cameraHalfWidth;
        float maxX = minCameraPos.x + maxCameraValue.x - cameraHalfWidth + extensionX;
        float minY = minCameraPos.y + cameraHalfHeight;
        float maxY = minCameraPos.y + maxCameraValue.y - cameraHalfHeight;

        // Проверяем, находится ли новая позиция в пределах заданной области, учитывая размеры камеры и offsetValue
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);

        // Присваиваем новую позицию, если она находится в пределах области
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}