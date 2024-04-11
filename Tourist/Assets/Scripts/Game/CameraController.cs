using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �������� ������������ ������
    [SerializeField] float movingSpeed;

    // ������� ������, �� ������� ������ ������
    private GameObject player;

    // �������� ������� ������
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
            // ���������� ������, ������������� ������ ������� ������
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
            // �������� ���������� ������
            Vector3 playerPosition = GetPlayerCoord();

            // ��������� ������� ������
            float cameraHalfHeight = Camera.main.orthographicSize;
            float cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

            // ������������ ���������� ������ ������ ������ � ������ ������
            float clampedX = Mathf.Clamp(playerPosition.x, minPos.x + cameraHalfWidth, maxPos.x - cameraHalfWidth);
            float clampedY = Mathf.Clamp(playerPosition.y, minPos.y + cameraHalfHeight, maxPos.y - cameraHalfHeight);

            // ������������� ����� ������� ������ ���������� ������ � ������ ������
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