using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameObject panels;
    private GameObject panelInfo;
    private TMP_Text infoText;

    private GameObject panelQuestions;

    void Start()
    {
        panelInfo = panels.transform.GetChild(0).gameObject;
        infoText = panelInfo.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        if (panels.transform.childCount >= 3)
            panelQuestions = panels.transform.GetChild(2).gameObject;    
    }

    public void DisplayText(string text)
    {
        panels.SetActive(true);
        panelInfo.SetActive(true);
        infoText.text = text;
    }

    public void ConstructorText()
    {
        string text = "Для получения дополнительных плиток переключите вкладки";
        panels.SetActive(true);
        panelInfo.SetActive(true);
        infoText.text = text;
    }

    public void OpenQuestions()
    {
        panels.SetActive(true);
        panelQuestions.SetActive(true);
    }

    public void CloseQuestion()
    {
        panels.SetActive(false);
        panelQuestions.SetActive(false);
    }
}