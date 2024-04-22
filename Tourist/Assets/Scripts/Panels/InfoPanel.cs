using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject panels;
    private GameObject panelInfo;
    private TMP_Text infoText;

    void Start()
    {
        panelInfo = panels.transform.GetChild(0).gameObject;
        infoText = panelInfo.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
    }

    public void DisplayText(string text)
    {
        panels.SetActive(true);
        panelInfo.SetActive(true);
        infoText.text = text;
    }

    public void ConstructorText()
    {
        string text = "Это стандартный текст для отображения, не более";
        panels.SetActive(true);
        panelInfo.SetActive(true);
        infoText.text = text;
    }
}