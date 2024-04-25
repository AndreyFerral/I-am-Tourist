using DataNamespace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionChoicePanel : MonoBehaviour
{
    private TMP_Text questionTMP;
    private TMP_Text[] answersTMPs;
    private Toggle[] answerToggles;
    private Button button;
    private int length = 4;

    public void SetQuestionChoicePanel(Collider2D collider)
    {
        questionTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        answersTMPs = new TMP_Text[length];
        answerToggles = new Toggle[length];

        string name = collider.name;
        var qcd = DataLoader.GetQuestionChoiceData(name);

        if (qcd != null)
        {
            questionTMP.text = qcd.nameQuestion;

            for (int i = 0; i < length; i++)
            {
                answerToggles[i] = transform.GetChild(1).GetChild(i).GetComponent<Toggle>();
                answersTMPs[i] = transform.GetChild(1).GetChild(i).GetChild(1).GetComponent<TMP_Text>();

                answersTMPs[i].text = qcd.answerResult[i].answer;
            }
        }
        else
        {
            Debug.Log("Данные НЕ найдены");
        }

        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(() => CheckAnswer(collider, qcd));
    }

    private void CheckAnswer(Collider2D collider, QuestionChoiceData qcd)
    {
        bool result = true;
        DialogBox dialogBox = FindObjectOfType<DialogBox>();

        // Проверяем результат
        for (int i = 0; i < length; i++)
        {
            if (qcd.answerResult[i].isOn != answerToggles[i].isOn) result = false;
            answerToggles[i].isOn = false;
        }

        if (result)
        {
            StaminaBar.ChangeStamina(qcd.valueStamina);
            dialogBox.StartDialogBox("Я правильно ответил на вопрос!");
        }
        else
        {
            dialogBox.StartDialogBox("Я неправильно ответил на вопрос");
        }

        // Выключаем коллайдер
        collider.enabled = false;
    }

    public void Initialize(QuestionChoiceData questionChoiceData, List<QuestionChoiceData> loadedQuestionChoiceData)
    {
        questionTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        answersTMPs = new TMP_Text[length];
        answerToggles = new Toggle[length];

        for (int i = 0; i < length; i++)
        {
            answersTMPs[i] = transform.GetChild(i + 1).GetChild(0).GetComponent<TMP_Text>();
            answerToggles[i] = transform.GetChild(i + 1).GetComponent<Toggle>();

            answersTMPs[i].text = questionChoiceData.answerResult[i].answer;
            answerToggles[i].isOn = questionChoiceData.answerResult[i].isOn;
        }

        questionTMP.text = questionChoiceData.nameQuestion;
        button = GetComponentInChildren<Button>();
        ResizeText(questionTMP);

        if (SceneManager.GetActiveScene().name == "Constructor")
        {
            TMP_Text button_text = button.GetComponentInChildren<TMP_Text>();
            button_text.text = "Выбрать";
            button.onClick.AddListener(() => Choice(questionChoiceData));
        }
        else
        {
            button.onClick.AddListener(() => Delete(questionChoiceData, loadedQuestionChoiceData));
        }
    }

    private void Delete(QuestionChoiceData questionChoiceData, List<QuestionChoiceData> loadedQuestionChoiceData)
    {
        loadedQuestionChoiceData.Remove(questionChoiceData);
        JsonSaveLoadSystem.ReplaceListData(loadedQuestionChoiceData);
        DestroyImmediate(gameObject);
    }

    private void Choice(QuestionChoiceData questionChoiceData)
    {
        // Закрываем окно с вопросами
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        infoPanel.CloseQuestion();

        LevelConstructor levelConstructor = FindObjectOfType<LevelConstructor>();
        levelConstructor.SetQuestion(questionChoiceData.nameQuestion);
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