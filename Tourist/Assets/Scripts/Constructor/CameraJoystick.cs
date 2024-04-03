using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // Скорость передвижения камеры
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    // Область, в которой камера может двигаться
    [SerializeField] Vector2 minCameraXY;
    [SerializeField] Vector2 maxCameraValue;

    // Дополнительная переменная для коррекции камеры
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
        float minX = minCameraXY.x + cameraHalfWidth;
        float maxX = minCameraXY.x + maxCameraValue.x - cameraHalfWidth + extensionX;
        float minY = minCameraXY.y + cameraHalfHeight;
        float maxY = minCameraXY.y + maxCameraValue.y - cameraHalfHeight;

        // Проверяем, находится ли новая позиция в пределах заданной области, учитывая размеры камеры и offsetValue
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);

        // Присваиваем новую позицию, если она находится в пределах области
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}