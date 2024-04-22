using DataNamespace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionChoicePanel : MonoBehaviour
{
    private TMP_Text questionTMP;
    private TMP_Text[] answersTMPs;
    private Toggle[] answerToggles;
    private Button button;

    public void Initialize(QuestionChoiceData questionChoiceData, List<QuestionChoiceData> loadedQuestionChoiceData)
    {
        questionTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        int length = 4;
        TMP_Text[] answersTMPs = new TMP_Text[length];
        Toggle[] answerToggles = new Toggle[length];

        for (int i = 0; i < length; i++)
        {
            answersTMPs[i] = transform.GetChild(i + 1).GetChild(0).GetComponent<TMP_Text>();
            answerToggles[i] = transform.GetChild(i + 1).GetComponent<Toggle>();

            answersTMPs[i].text = questionChoiceData.answerResult[i].answer;
            answerToggles[i].isOn = questionChoiceData.answerResult[i].isOn;
        }

        questionTMP.text = questionChoiceData.nameQuestion;

        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(() => Delete(questionChoiceData, loadedQuestionChoiceData));

        ResizeText(questionTMP);
    }

    private void Delete(QuestionChoiceData questionChoiceData, List<QuestionChoiceData> loadedQuestionChoiceData)
    {
        loadedQuestionChoiceData.Remove(questionChoiceData);
        JsonSaveLoadSystem.ReplaceListData(loadedQuestionChoiceData);
        DestroyImmediate(gameObject);
    }

    void ResizeText(TMP_Text textMeshPro)
    {
        float textHeight = textMeshPro.GetPreferredValues().y; // Получение предпочтительной высоты текста 
        float currentHeight = textMeshPro.rectTransform.sizeDelta.y; // Текущая высота текстового поля

        if (textHeight > currentHeight)
        {
            textMeshPro.rectTransform.sizeDelta = new Vector2(textMeshPro.rectTransform.sizeDelta.x, textHeight); // Изменение высоты текстового поля
        }
    }
}