using TMPro;
using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour
{
    // ������� ������, �� ������� ������ DialogBox
    private GameObject player;
    private string playerTag = "Player";

    // ������� ������� DialogBox
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

    // ����� ��� ��������� ��������� ������
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

    // ����� ��� ������� ��������� SetDialogBox
    public void StartDialogBox(string text)
    {
        activeObject = true;
        StartCoroutine(SetDialogBox(text));
    }

    // ����� ��������� DialogBox
    private IEnumerator SetDialogBox(string text)
    {
        // ����������� DialogBox
        dbSprite.SetActive(true);
        dbText.text = text;
        activeObject = false;

        // ������� ������������ ���������� �������
        const int awaitTime = 2;
        yield return new WaitForSeconds(awaitTime);

        // ��������� DialogBox
        if (!activeObject) dbSprite.SetActive(false);
    }
}