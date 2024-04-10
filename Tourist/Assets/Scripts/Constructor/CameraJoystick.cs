using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // �������� ������������ ������
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    // �������, � ������� ������ ����� ���������
    private static Vector2 minCameraPos = new Vector2(-16.5f, -8);
    private static Vector2 maxCameraValue;

    // �������������� ���������� ��� ��������� ������
    float extensionPercent = 2.5f;

    // �������� �������� ������ ���� � ������� PrepareLevel.cs
    public static Vector2Int MaxCameraValue { set => maxCameraValue = value; }

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
        float minX = minCameraPos.x + cameraHalfWidth;
        float maxX = minCameraPos.x + maxCameraValue.x - cameraHalfWidth + extensionX;
        float minY = minCameraPos.y + cameraHalfHeight;
        float maxY = minCameraPos.y + maxCameraValue.y - cameraHalfHeight;

        // ���������, ��������� �� ����� ������� � �������� �������� �������, �������� ������� ������ � offsetValue
        float clampedX = Mathf.Clamp(newPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(newPos.y, minY, maxY);

        // ����������� ����� �������, ���� ��� ��������� � �������� �������
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}