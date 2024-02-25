using TMPro;
using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour
{
    // Игровой объект, за которым следит DialogBox
    private GameObject player;
    private string playerTag = "Player";

    // Игровые объекты DialogBox
    private TMP_Text dbText;
    private GameObject dbSprite;
    private bool activeObject = false;

    void Awake()
    {
        dbSprite = transform.GetChild(0).gameObject;
        dbText = dbSprite.GetComponentInChildren<TMP_Text>();
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void FixedUpdate()
    {
        transform.position = GetPlayerCoord();
    }

    // Метод для получения координат игрока
    Vector3 GetPlayerCoord()
    {
        const float addHeight = 1.5f;
        Vector3 playerCoord = new Vector3()
        {
            x = player.transform.position.x,
            y = player.transform.position.y + addHeight,
            z = player.transform.position.z
        };
        return playerCoord;
    }

    // Метод для запуска короутины SetDialogBox
    public void StartDialogBox(string text)
    {
        activeObject = true;
        StartCoroutine(SetDialogBox(text));
    }

    // Метод настройки DialogBox
    private IEnumerator SetDialogBox(string text)
    {
        // Настраиваем DialogBox
        dbSprite.SetActive(true);
        dbText.text = text;
        activeObject = false;

        // Ожидаем определенное количество времени
        const int awaitTime = 2;
        yield return new WaitForSeconds(awaitTime);

        // Выключаем DialogBox
        if (!activeObject) dbSprite.SetActive(false);
    }
}