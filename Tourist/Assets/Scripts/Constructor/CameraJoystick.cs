using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // �������� ������������ ������
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    // �������, � ������� ������ ����� ���������
    [SerializeField] Vector2 minCameraXY;
    [SerializeField] Vector2 maxCameraValue;

    // �������������� ���������� ��� ��������� ������
    float extensionPercent = 2.5f;

    void FixedUpdate()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        // ������������ �������� ��� ��������� ���� ��������
        Vector3 moveDirection = new Vector2(moveX, moveY).normalized;
        Vector3 newPos = transform.position + moveDirection * movingSpeed * Time.deltaTime;

        // ��������� ������� ������
        float cameraHalfHeight = Camera.main.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        // ��������� ������ ���������� ����� ������, ������ �� �������� �� aspect ratio
        float extensionX = Camera.main.aspect * extensionPercent;

        // ��������� min � max ��� X � Y, �������� ������� ������ � offsetValue
        float minX = minCameraXY.x + cameraHalfWidth;
        float maxX = minCameraXY.x + maxCameraValue.x - cameraHalfWidth + extensionX;
        float minY = minCameraXY.y + cameraHalfHeight;
        float maxY = minCameraXY.y + maxCameraValue.y - cameraHalfHeight;

        // ���������, ��������� �� ����� ������� � �������� �������� �������, �������� ������� ������ � offsetValue
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);

        // ����������� ����� �������, ���� ��� ��������� � �������� �������
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}