using DataNamespace;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ����� ��� ����������� � ����� ����
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // ����� ��� ����������� � ����� �������� ��������
    public void CustomRoute()
    {
        // ������������� �������������� ��������
        DataHolder.IdBackpack = 0;
        DataHolder.IdLocation = 0;
        DataHolder.IdSeason = 0;

        // ������� ��� ����������� JSON �����
        JsonSaveLoadSystem.CreateJSON();

        SceneManager.LoadScene(1);
    }

    // ����� ��� ����������� � ����� ������ � ������������ �����
    public void SelectItems()
    {
        DataHolder.Items = null;
        Inventory.IsOpen = false;

        SceneManager.LoadScene(2);
    }

    // ����� ��� ����������� � ����� ����
    public void PlayGame()
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
    public void MenuConstructor()
    {
        SceneManager.LoadScene(5);
    }

    // ����� ��� ����������� � ���� ������������
    public void Constructor()
    {
        SceneManager.LoadScene(6);
    }

    // ����� ��� ����������� � ����� �������
    public void LevelGame()
    {
        SceneManager.LoadScene(4);
    }

    // ����� ��� ������ �� ����
    public void QuitGame()
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
        JsonSaveLoadSystem.SaveListData(data);
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
        JsonSaveLoadSystem.SaveListData(data);
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
        JsonSaveLoadSystem.SaveListData(data);
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

        JsonSaveLoadSystem.SaveListData(data);
    }

    private static void CreateEventsInfoData()
    {
        List<EventsInfoData> data = new List<EventsInfoData>();
        data.Add(new EventsInfoData("Picnic", 0, "", "����������","" ,""));

        data.Add(new EventsInfoData("Campfire", 0, "", "�������", "�������", ""));
        data.Add(new EventsInfoData("Campfire", 1, "", "�����������", "", ""));
        data.Add(new EventsInfoData("Campfire", 2, "", "������", "", ""));
        
        JsonSaveLoadSystem.SaveListData(data);
    }

    private static void CreateEventsData()
    {
        List<EventsData> data = new List<EventsData>();
        data.Add(new EventsData("Picnic", "������", ""));
        data.Add(new EventsData("Campfire", "�����", ""));

        JsonSaveLoadSystem.SaveListData(data);
    }

    // ����� ��� ���������� ������ � JSON
    public static void AddDataToJson()
    {
        /*
        CreateEventsItemsData();
        CreateEventsInfoData();
        CreateEventsData();
        */
    }
}