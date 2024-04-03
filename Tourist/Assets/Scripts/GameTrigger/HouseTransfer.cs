using UnityEngine;
using DataNamespace;

public class HouseTransfer : MonoBehaviour
{
    [SerializeField] DialogBox scriptDB;
    [SerializeField] GameObject gridHouse;
    [SerializeField] GameObject[] gridMaps;

    private Vector2 houseMin;
    private Vector2 houseMax;
    private Vector2 minPosGrid;
    private Vector2[] maxPosGrid;
    private Vector3 playerIn;
    private Vector3 playerOut;

    private CameraController cam;

    private string playerTag = "Player";
    private int id = DataHolder.IdLocation;

    public static bool IsHome { get; set; }

    private void Start()
    {
        // ������������� �������������� ��������
        IsHome = true;

        houseMin = new Vector2(-31.8f, -1.2f);
        houseMax = new Vector2(-28.4f, 1.1f);

        cam = Camera.main.GetComponent<CameraController>();

        minPosGrid = new Vector2(-6, -3);

        maxPosGrid = new Vector2[4];
        maxPosGrid[0] = new Vector2(10.5f, 3);
        maxPosGrid[1] = new Vector2(50, 3);
        maxPosGrid[2] = new Vector2(50, 3);
        maxPosGrid[3] = new Vector2(0, 50);

        playerIn = new Vector3(-30.5f, -2, 0);
        playerOut = new Vector3(-3, -1, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

            // ���� ����� ����
            if (IsHome)
            {
                bool isNotifyStart = DataHolder.IsNotifyStart;
                bool isAfterRoute = DataHolder.IsAfterRoute;

                // ���� ����� �� ������� � ������ ��������
                if (!isNotifyStart)
                {
                    scriptDB.StartDialogBox(dialog.TextBefore);
                    return;
                }
                // ���� ����� ������ �������
                else if (isAfterRoute)
                {
                    scriptDB.StartDialogBox(dialog.TextAfter);
                    return;
                }
            }

            ChangeGrid(other);
        }
    }

    // ����� ��� ���������/���������� �������
    private void ChangeGrid(Collider2D player)
    {
        // ���� ����� ������� �� ����
        if (IsHome)
        {
            cam.MovePlayer(minPosGrid, maxPosGrid[id], playerOut);

            IsHome = false;
            gridHouse.SetActive(false);
            gridMaps[id].SetActive(true);
        }
        // ���� ����� ������ � ���
        else
        {
            cam.MovePlayer(houseMin, houseMax, playerIn);

            IsHome = true;
            gridHouse.SetActive(true);
            gridMaps[id].SetActive(false);
        }
    }
}