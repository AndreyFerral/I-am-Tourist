using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ������� ������, �� ������� ������ ������
    private GameObject player;
    private string playerTag = "Player";

    // �������� ������������ ������
    [SerializeField] float movingSpeed;

    // �������� ������� ������
    public Vector2 minPos;
    public Vector2 maxPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);

        // ������������� ������ ������� ������
        transform.position = GetPlayerCoord();
    }

    void FixedUpdate()
    {
        if (player.transform)
        {
            // �������� ���������� ������
            Vector3 target = GetPlayerCoord();

            // ������������ ���������� 
            target.x = Mathf.Clamp(target.x, minPos.x, maxPos.x);
            target.y = Mathf.Clamp(target.y, minPos.y, maxPos.y);

            // ������������ ����������
            Vector3 pos = Vector3.Lerp(
                transform.position, target,
                movingSpeed * Time.fixedDeltaTime);

            // ������������� ���������� ������
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