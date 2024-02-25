using UnityEngine;
using System.Collections;
using DataNamespace;
using System.Collections.Generic;

public class HouseTransfer : MonoBehaviour
{
    private Vector2 houseMin;
    private Vector2 houseMax;
    private Vector2[] newMinPos;
    private Vector2[] newMaxPos;
    private Vector3 playerIn;
    private Vector3 playerOut;

    [SerializeField] DialogBox scriptDB;
    [SerializeField] GameObject gridHouse;
    [SerializeField] GameObject[] gridsMap;

    private CameraController cam;
    private Transform camTransform;

    private string playerTag = "Player";
    private int idGrid = DataHolder.IdLocation;
    private static bool isHome = true;

    private bool isNotifyStart;
    private bool isAfterRoute;

    public static bool IsHome
    {
        get { return isHome; }
        set { isHome = value; }
    }

    private void Start()
    {
        houseMin = new Vector2(-31.8f, -1.2f);
        houseMax = new Vector2(-28.4f, 1.1f);

        cam = Camera.main.GetComponent<CameraController>();
        camTransform = cam.GetComponent<Transform>();

        newMinPos = new Vector2[4];
        Vector2 verticalMin = new Vector2(-6, -3);
        Vector2 horizontalMin = new Vector2(-6, -3);
        newMinPos[0] = horizontalMin;
        newMinPos[1] = horizontalMin;
        newMinPos[2] = horizontalMin;
        newMinPos[3] = verticalMin;

        newMaxPos = new Vector2[4];
        Vector2 verticalMax = new Vector2(0, 50);
        Vector2 horizontalMax = new Vector2(50, 3);
        newMaxPos[0] = new Vector2(10.5f, 3);
        newMaxPos[1] = horizontalMax;
        newMaxPos[2] = horizontalMax;
        newMaxPos[3] = verticalMax;

        playerIn = new Vector3(-30.5f, -2, 0);
        playerOut = new Vector3(-3, -1, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

            // Если игрок дома
            if (isHome)
            {
                isNotifyStart = DataHolder.IsNotifyStart;
                isAfterRoute = DataHolder.IsAfterRoute;

                // Если игрок не сообщил о начале маршрута
                if (!isNotifyStart)
                {
                    scriptDB.StartDialogBox(dialog.TextBefore);
                    return;
                }
                // Если игрок прошел маршрут
                else if (isAfterRoute)
                {
                    scriptDB.StartDialogBox(dialog.TextAfter);
                    return;
                }
            }

            ChangeGrid(other);
        }
    }

    // Метод для включения/выключения локации
    private void ChangeGrid(Collider2D player)
    {
        // Если игрок выходит из дома
        if (isHome)
        {
            player.transform.position = playerOut;
            camTransform.position = playerOut;
            cam.minPos = newMinPos[idGrid];
            cam.maxPos = newMaxPos[idGrid];

            isHome = false;
            gridHouse.SetActive(false);
            gridsMap[idGrid].SetActive(true);
        }
        // Если игрок входит в дом
        else
        {
            player.transform.position = playerIn;
            camTransform.position = playerIn;
            cam.minPos = houseMin;
            cam.maxPos = houseMax;

            isHome = true;
            gridHouse.SetActive(true);
            gridsMap[idGrid].SetActive(false);
        }
    }
}