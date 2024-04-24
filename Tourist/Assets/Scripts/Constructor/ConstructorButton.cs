using DataNamespace;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructorButton : MonoBehaviour
{
    [Header("�������� ������")]
    [SerializeField] TMP_InputField input_name;
    [SerializeField] TMP_InputField input_description;
    [SerializeField] Slider slider_height;
    [SerializeField] Slider slider_width;
    [SerializeField] ToggleGroup toggle_group;

    [Header("�������� �������")]
    [SerializeField] TMP_InputField input_question;
    [SerializeField] GameObject[] answers;

    public void CreateQuestion()
    {
        string question = input_question.text;

        int length = answers.Length;
        TMP_InputField[] answerInputs = new TMP_InputField[length];
        Toggle[] answerToggles = new Toggle[length];

        for (int i = 0; i < length; i++)
        {
            answerInputs[i] = answers[i].transform.GetChild(0).GetComponent<TMP_InputField>();
            answerToggles[i] = answers[i].GetComponent<Toggle>();
        }

        int trueToggleCount = answerToggles.Count(toggle => toggle.isOn);

        if (question == "")
        {
            InfoPanel("���������� ������� ������ � ������ �����������");
            return;
        }
        else if (CheckQuestion(question))
        {
            InfoPanel("������ � ������ ��������� ��� ����������");
            return;
        }
        else if (trueToggleCount == 0 || trueToggleCount == 4)
        {
            InfoPanel("���� ������� �� 1 �� 3 �������");
            return;
        }
        foreach (TMP_InputField answer in answerInputs)
        {
            if (answer.text == "")
            {
                InfoPanel("��� �������� ������� ������ ��������� �����");
                return;
            }
        }

        Answers[] questionAnswers = new Answers[length];
        for (int i = 0; i < length; i++)
        {
            questionAnswers[i] = new Answers { 
                answer = answerInputs[i].text,
                isOn = answerToggles[i].isOn 
            };
        }

        JsonSaveLoadSystem.AddDataToList(new QuestionChoiceData(question, questionAnswers, 10));
        InfoPanel("������ ��� �������� � ���������");

        // ���������� ������ � ���������
        MainMenu mainMenu = FindObjectOfType<MainMenu>();
        mainMenu.UpdateQuestions();

        ClearPanelQuestion(input_question, answerInputs, answerToggles);
    }


    private void ClearPanelQuestion(TMP_InputField question, TMP_InputField[] answers, Toggle[] toggles)
    {
        question.text = "";
        for (int i = 0; i < 4; i++)
        {
            answers[i].text = "";
            toggles[i].isOn = false;
        }
    }

    public void CreateLevel()
    {
        string level_name = input_name.text;
        string level_description = input_description.text;
        int level_height = (int)slider_height.value;
        int level_width = (int)slider_width.value;

        if (level_name == "" || level_description == "")
        {
            InfoPanel("���������� ������� ������� � ������ ��������� ��� ���������");
            return;
        }
        else if (CheckName(level_name))
        {
            InfoPanel("������� � ������ ��������� ��� ����������");
            return;
        }

        Toggle[] toggles = toggle_group.GetComponentsInChildren<Toggle>();
        int toggle_index = toggles.ToList().FindIndex(toggle => toggle.isOn);

        DataHolder.levelData = new LevelData(level_name, level_description, level_height, level_width, toggle_index);
        MainMenu.Constructor();
    }

    private void InfoPanel(string text)
    {
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        infoPanel.DisplayText(text);
    }

    private bool CheckName(string name)
    {
        var loadedLevelDataList = JsonSaveLoadSystem.LoadListData<LevelData>();

        foreach (LevelData item in loadedLevelDataList)
        {
            if (item.nameMap == name) return true;
        }
        return false;
    }

    private bool CheckQuestion(string name)
    {
        var loadedQuestionChoiceDataList = JsonSaveLoadSystem.LoadListData<QuestionChoiceData>();

        foreach (QuestionChoiceData item in loadedQuestionChoiceDataList)
        {
            if (item.nameQuestion == name) return true;
        }
        return false;
    }
}