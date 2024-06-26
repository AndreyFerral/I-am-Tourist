using DataNamespace;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("��������� �������")]
    [SerializeField] GameObject levelPanelPrefab;
    [SerializeField] Transform parentTransform;

    [Header("��������� ��������")]
    [SerializeField] GameObject questionChoicePrefab;
    [SerializeField] Transform parentQuestionTransform;

    void Start()
    {
        // ������� ��� ����������� JSON �����
        JsonSaveLoadSystem.CreateJSON();

        if (questionChoicePrefab != null && parentQuestionTransform != null)
        {
            UpdateQuestions();
        }
        if (levelPanelPrefab != null && parentTransform != null)
        { 
            UpdateLevels();
        }
    }

    // ����� ��� ���������� ������ ��� ����������� ��������
    public void UpdateQuestions()
    {
        List<QuestionChoiceData> loadedQuestionChoiceData = JsonSaveLoadSystem.LoadListData<QuestionChoiceData>();

        // �������� ���� �������� �������� �� parentQuestionTransform
        foreach (Transform child in parentQuestionTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (QuestionChoiceData item in loadedQuestionChoiceData)
        {
            GameObject questionChoicePanel = Instantiate(questionChoicePrefab, parentQuestionTransform);
            QuestionChoicePanel qcp = questionChoicePanel.GetComponent<QuestionChoicePanel>();
            qcp.Initialize(item, loadedQuestionChoiceData);
        }
    }

    // ����� ��� ���������� ������ ��� ����������� �������
    public void UpdateLevels()
    {
        List<LevelData> loadedLevelDataList = JsonSaveLoadSystem.LoadListData<LevelData>();

        // �������� ���� �������� �������� �� parentTransform
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (LevelData item in loadedLevelDataList)
        {
            GameObject panelObject = Instantiate(levelPanelPrefab, parentTransform);
            LevelPanelInfo lpi = panelObject.GetComponent<LevelPanelInfo>();
            LevelPanelConstructor lpc = panelObject.GetComponent<LevelPanelConstructor>();
            if (lpi != null) lpi.Initialize(item);
            else lpc.Initialize(item, loadedLevelDataList);
        }
    }

    // ����� ��� ����������� � ����� ����
    public static void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // ����� ��� ����������� � ����� �������� ��������
    public static void CustomRoute()
    {
        // ������������� �������������� ��������
        DataHolder.IdBackpack = 0;
        DataHolder.IdLocation = 0;
        DataHolder.IdSeason = 0;

        SceneManager.LoadScene(1);
    }

    // ����� ��� ����������� � ����� ������ � ������������ �����
    public static void SelectItems()
    {
        DataHolder.Items = null;
        Inventory.IsOpen = false;

        SceneManager.LoadScene(2);
    }

    // ����� ��� ����������� � ����� ����
    public static void PlayGame()
    {
        // ������������� �������������� ��������
        Inventory.IsOpen = false;
        HouseTransfer.IsHome = true;

        DataHolder.IsAfterRoute = false;
        DataHolder.IsNotifyStart = false;
        DataHolder.IsNotifyEnd = false;

        SceneManager.LoadScene(3);
    }

    // ����� ��� ����������� � ���� ������������
    public static void MenuConstructor()
    {
        SceneManager.LoadScene(4);
    }

    // ����� ��� ����������� � ���� ������������
    public static void Constructor()
    {
        SceneManager.LoadScene(5);
    }

    // ����� ��� ������ �� ����
    public static void QuitGame()
    {
        Application.Quit();
    }

    // ����� ��� ������� JSON ������
    public static void DeleteJsonFolder()
    {
        string jsonFolderPath = Path.Combine(Application.persistentDataPath, "JSON");

        if (Directory.Exists(jsonFolderPath))
        {
            Directory.Delete(jsonFolderPath, true);
            Debug.Log("����� JSON �� ���� ���������� ���� �������");
        }
    }

    private static void CreateInteractPanelData()
    {
        List<InteractPanelData> data = new List<InteractPanelData>();
        data.Add(new InteractPanelData("TrashCan", "�������� �����", "��������� �����", "��� ������"));
        data.Add(new InteractPanelData("Notify", "������������� � ���", "�������� � ������ ������", "�������� � ����� ������"));
        data.Add(new InteractPanelData("ItemPick", "", "���������", "��� �����"));
        data.Add(new InteractPanelData("Picnic", "������", "�������", ""));
        data.Add(new InteractPanelData("Campfire", "�����", "�������", ""));
        data.Add(new InteractPanelData("Finish", "������� �������", "�����", ""));
        JsonSaveLoadSystem.ReplaceListData(data);
    }

    private static void CreateDialogBoxData()
    {
        List<DialogBoxData> data = new List<DialogBoxData>();
        data.Add(new DialogBoxData("TrashCan", "� �������� ����� ����� ��������� �������", ""));
        data.Add(new DialogBoxData("Notify", "", "������ � ���� ����������� � �����!"));
        data.Add(new DialogBoxData("Teleport", "������� ��� ����� �������� � ���, ��� � ����� � �����", "��� ����� �������� � ���, ��� � �������� �� ������"));
        data.Add(new DialogBoxData("Finish", "", "��� ����� �������� � ���, ��� � �������� �� ������"));
        data.Add(new DialogBoxData("Brook", "��� ����� ������ ������ ��� ���� � �����", "�� ��� ������ - � �� ������ ����"));
        data.Add(new DialogBoxData("Rain", "��� ����� ����� ���� ��� ������ ����", "������ � �� ��������"));
        JsonSaveLoadSystem.ReplaceListData(data);
    }

    private static void CreateItemData()
    {
        List<ItemData> data = new List<ItemData>();
        data.Add(new ItemData("Prefabs/", "boots", "�������", 0.2f));
        data.Add(new ItemData("Prefabs/", "umbrella", "����", 0.3f));
        data.Add(new ItemData("Prefabs/", "saucepan", "��������", 0.5f));
        data.Add(new ItemData("Prefabs/", "raincoat", "����", 0.5f));
        data.Add(new ItemData("Prefabs/", "branch", "�����", 0.1f));
        data.Add(new ItemData("Prefabs/", "cutlery", "�������", 0.2f));
        data.Add(new ItemData("Prefabs/", "energy-bar", "��������", 0.1f));
        data.Add(new ItemData("Prefabs/", "lighter", "���������", 0.1f));
        data.Add(new ItemData("Prefabs/", "matches", "������", 0.1f));
        data.Add(new ItemData("Prefabs/", "packaging", "�����", 0.15f));
        data.Add(new ItemData("Prefabs/", "plastic-cup", "����", 0.2f));
        data.Add(new ItemData("Prefabs/", "raspberry", "�����", 0.1f));
        data.Add(new ItemData("Prefabs/", "rice-bowl", "��������", 0.3f));
        data.Add(new ItemData("Prefabs/", "rice", "���", 0.25f));
        data.Add(new ItemData("Prefabs/", "sandwich", "���������", 0.4f));
        data.Add(new ItemData("Prefabs/", "thermos", "������", 0.6f));
        data.Add(new ItemData("Prefabs/", "zip-bag", "��������", 0.1f));
        JsonSaveLoadSystem.ReplaceListData(data);
    }

    private static void CreateEventsItemsData()
    {
        List<EventsItemsData> data = new List<EventsItemsData>();
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "����" }, new List<string> { "����" }, new List<string> { "" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "������" }, new List<string> { "������" }, new List<string> { "" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "�����" }, new List<string> { "�����" }, new List<string> { "" }, 10));

        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "��������" }, new List<string> { "��������" }, new List<string> { "�����" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "���������" }, new List<string> { "���������" }, new List<string> { "�����" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "��������" }, new List<string> { "��������" }, new List<string> { "�����" }, 10));

        data.Add(new EventsItemsData("TrashCan", 0, new List<string> { "�����" }, new List<string> { "�����" }, new List<string> { "" }, 0));
        data.Add(new EventsItemsData("Brook", 0, new List<string> { "�������" }, new List<string> { "" }, new List<string> { "" }, -2));
        data.Add(new EventsItemsData("Rain", 0, new List<string> { "����" }, new List<string> { "" }, new List<string> { "" }, -2));
        data.Add(new EventsItemsData("Rain", 0, new List<string> { "����" }, new List<string> { "" }, new List<string> { "" }, -2));

        data.Add(new EventsItemsData("Campfire", 0, new List<string> { "�����", "���������" }, new List<string> { "�����" }, new List<string> { "" }, 0));
        data.Add(new EventsItemsData("Campfire", 0, new List<string> { "�����", "������" }, new List<string> { "�����" }, new List<string> { "" }, 0));
        data.Add(new EventsItemsData("Campfire", 1, new List<string> { "��������", "��������", "����" }, new List<string> { "��������", "����" }, new List<string> { "���" }, 0));
        data.Add(new EventsItemsData("Campfire", 1, new List<string> { "��������", "��������", "������" }, new List<string> { "��������", "������" }, new List<string> { "���" }, 0));
        data.Add(new EventsItemsData("Campfire", 2, new List<string> { "���", "�������" }, new List<string> { "���" }, new List<string> { "" }, 20));

        JsonSaveLoadSystem.ReplaceListData(data);
    }

    private static void CreateEventsInfoData()
    {
        List<EventsInfoData> data = new List<EventsInfoData>();
        data.Add(new EventsInfoData("Picnic", 0, "", "����������","" ,""));

        data.Add(new EventsInfoData("Campfire", 0, "", "�������", "�������", ""));
        data.Add(new EventsInfoData("Campfire", 1, "", "�����������", "", ""));
        data.Add(new EventsInfoData("Campfire", 2, "", "������", "", ""));
        
        JsonSaveLoadSystem.ReplaceListData(data);
    }

    private static void CreateEventsData()
    {
        List<EventsData> data = new List<EventsData>();
        data.Add(new EventsData("Picnic", "������", ""));
        data.Add(new EventsData("Campfire", "�����", ""));

        JsonSaveLoadSystem.ReplaceListData(data);
    }
}