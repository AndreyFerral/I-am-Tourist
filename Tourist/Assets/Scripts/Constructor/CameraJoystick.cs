using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // Скорость передвижения камеры
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    // Область, в которой камера может двигаться
    [SerializeField] Vector2 minCameraPos;
    [SerializeField] Vector2 maxCameraPos;

    // Дополнительная переменная для коррекции камеры
    float offsetY = 2.0f;
    float offsetX = 1.5f;
    float extensionPercent = 2.5f;

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
        float minX = minCameraPos.x + cameraHalfWidth - offsetX;
        float maxX = minCameraPos.x + maxCameraPos.x - cameraHalfWidth - offsetX + extensionX;
        float minY = minCameraPos.y + cameraHalfHeight - offsetY;
        float maxY = minCameraPos.y + maxCameraPos.y - cameraHalfHeight - offsetY;

        // Проверяем, находится ли новая позиция в пределах заданной области, учитывая размеры камеры и offsetValue
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);

        // Присваиваем новую позицию, если она находится в пределах области
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}