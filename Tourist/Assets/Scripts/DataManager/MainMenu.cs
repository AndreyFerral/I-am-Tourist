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

    // ����� ��� ������ �� ����
    public void QuitGame()
    {
        Application.Quit();
    }

    // ����� ��� ����������� � ����� �������
    public void LevelGame()
    {
        SceneManager.LoadScene(4);
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

    // ����� ��� ���������� ������ � JSON
    public static void AddDataToJson()
    {
        /*
        List<InteractPanelData> interactPanelDatas = new List<InteractPanelData>();
        interactPanelDatas.Add(new InteractPanelData("TrashCan", "�������� �����", "��������� �����", "��� ������"));
        interactPanelDatas.Add(new InteractPanelData("Notify", "������������� � ���", "�������� � ������ ������", "�������� � ����� ������"));
        interactPanelDatas.Add(new InteractPanelData("ItemPick", "", "���������", "��� �����"));
        interactPanelDatas.Add(new InteractPanelData("Picnic", "������", "�������", ""));
        interactPanelDatas.Add(new InteractPanelData("Campfire", "�����", "�������", ""));
        interactPanelDatas.Add(new InteractPanelData("Finish", "������� �������", "�����", ""));
        JsonSaveLoadSystem.SaveListData(interactPanelDatas);
        */

        /*
        List<DialogBoxData> data = new List<DialogBoxData>();
        data.Add(new DialogBoxData("TrashCan", "� �������� ����� ����� ��������� �������", ""));
        data.Add(new DialogBoxData("Notify", "", "������ � ���� ����������� � �����!"));
        data.Add(new DialogBoxData("Teleport", "������� ��� ����� �������� � ���, ��� � ����� � �����", "��� ����� �������� � ���, ��� � �������� �� ������"));
        data.Add(new DialogBoxData("Finish", "", "��� ����� �������� � ���, ��� � �������� �� ������"));
        data.Add(new DialogBoxData("Brook", "��� ����� ������ ������ ��� ���� � �����", "�� ��� ������ - � �� ������ ����"));
        data.Add(new DialogBoxData("Rain", "��� ����� ����� ���� ��� ������ ����", "������ � �� ��������"));
        JsonSaveLoadSystem.SaveListData(data);
        */

        
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
}