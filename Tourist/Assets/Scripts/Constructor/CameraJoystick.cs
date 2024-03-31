using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // �������� ������������ ������
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    // �������, � ������� ������ ����� ���������
    [SerializeField] Vector2 minCameraPos;
    [SerializeField] Vector2 maxCameraPos;

    // �������������� ���������� ��� ��������� ������
    float offsetY = 2.0f;
    float offsetX = 1.5f;
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
        float minX = minCameraPos.x + cameraHalfWidth - offsetX;
        float maxX = minCameraPos.x + maxCameraPos.x - cameraHalfWidth - offsetX + extensionX;
        float minY = minCameraPos.y + cameraHalfHeight - offsetY;
        float maxY = minCameraPos.y + maxCameraPos.y - cameraHalfHeight - offsetY;

        // ���������, ��������� �� ����� ������� � �������� �������� �������, �������� ������� ������ � offsetValue
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);

        // ����������� ����� �������, ���� ��� ��������� � �������� �������
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}