using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public static class JsonSaveLoadSystem
{
    // ���� � ����� ��� ���������� ������
    private static string saveFolderPath = Application.persistentDataPath;

    // ����� ��� ���������� ������ ������ � JSON ����
    public static void SaveListData<T>(List<T> dataList)
    {
        Debug.Log("������ ����� SaveListData");

        // ������� ��� ����� �� ������ ����� ����
        string jsonFileName = typeof(T).Name + "List.json";
        // ������� ������ ���� � �����
        string jsonFilePath = Path.Combine(saveFolderPath, jsonFileName);
        // ����������� ������ � JSON
        string jsonData = JsonConvert.SerializeObject(dataList);

        // ���������� JSON � ����
        File.WriteAllText(jsonFilePath, jsonData);
    }

    // ����� ��� ���������� ������ � ����� ������ � ���������� ������������ ������
    public static void AddDataToList<T>(T newData)
    {
        Debug.Log("������ ����� AddDataToList");

        // ������� ��� ����� �� ������ ����� ����
        string jsonFileName = typeof(T).Name + "List.json";
        // ������� ������ ���� � �����
        string jsonFilePath = Path.Combine(saveFolderPath, jsonFileName);

        List<T> dataList;

        if (File.Exists(jsonFilePath))
        {
            // ������ JSON �� �����
            string jsonData = File.ReadAllText(jsonFilePath);
            // ������������� JSON � ������ ��������
            dataList = JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            // ���� ���� �� ����������, ������� ����� ������
            dataList = new List<T>();
        }

        // ��������� ����� ������ � ������
        dataList.Add(newData);

        // ����������� ����������� ������ � JSON
        string updatedJsonData = JsonConvert.SerializeObject(dataList);
        // ���������� JSON ������� � ����
        File.WriteAllText(jsonFilePath, updatedJsonData);
    }

    // ����� ��� �������� ������ ������ �� JSON �����
    public static List<T> LoadListData<T>()
    {
        Debug.Log("������ ����� LoadListData");

        // ������� ��� ����� �� ������ ����� ����
        string jsonFileName = typeof(T).Name + "List.json";
        // ������� ������ ���� � �����
        string jsonFilePath = Path.Combine(saveFolderPath, jsonFileName);

        if (File.Exists(jsonFilePath))
        {
            // ������ JSON �� �����
            string jsonData = File.ReadAllText(jsonFilePath);
            // ������������� JSON � ������ ��������
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            Debug.LogError("���� �� ������: " + jsonFileName);
            // ���������� ������ ������ � ������ ������
            return new List<T>();
        }
    }
}