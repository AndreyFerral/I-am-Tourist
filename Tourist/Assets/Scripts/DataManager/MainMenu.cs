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

        
        List<BackpackData> data = new List<BackpackData>();
        data.Add(new BackpackData(0, "SmallBackpack", 2));
        data.Add(new BackpackData(1, "MediumBackpack", 3));
        data.Add(new BackpackData(2, "LargeBackpack", 4));
        JsonSaveLoadSystem.SaveListData(data);
        
    }
}