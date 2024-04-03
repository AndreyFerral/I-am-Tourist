using TMPro;
using UnityEngine;
using System.Collections;

public class DialogBox : MonoBehaviour
{
    private GameObject player;
    private string playerTag = "Player";

    private TMP_Text dbText;
    private GameObject dbSprite;
    private Coroutine dialogCoroutine;

    void Awake()
    {
        dbSprite = transform.GetChild(0).gameObject;
        dbText = dbSprite.GetComponentInChildren<TMP_Text>();
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void Update()
    {
        transform.position = GetPlayerCoord();
    }

    Vector3 GetPlayerCoord()
    {
        const float addHeight = 1.5f;
        Vector3 playerCoord = player ? player.transform.position + Vector3.up * addHeight : transform.position;
        return playerCoord;
    }

    public void StartDialogBox(string text)
    {
        if (dialogCoroutine != null)
        {
            StopCoroutine(dialogCoroutine);
        }
        dialogCoroutine = StartCoroutine(SetDialogBox(text));
    }

    private IEnumerator SetDialogBox(string text)
    {
        dbSprite.SetActive(true);
        dbText.text = text;

        yield return new WaitForSeconds(2);

        dbSprite.SetActive(false);
    }
}