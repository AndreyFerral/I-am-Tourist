using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public static class JsonSaveLoadSystem
{
    // �������� ���� � ������ StreamingAssets � persistentDataPath
    private static string sourceDir = Path.Combine(Application.streamingAssetsPath, "JSON");
    private static string saveFolder = Path.Combine(Application.persistentDataPath, "JSON");

    // ����� ��� �������� ������ JSON �� ���������� ������������
    public static void CreateJSON()
    {
        Debug.Log("������ ����� CreateJSON");

        // ���������, ���������� �� ����� � �������, ���� �� ����������
        if (!Directory.Exists(saveFolder)) Directory.CreateDirectory(saveFolder);

        // �������� ��� JSON-����� �� ����� StreamingAssets
        string[] jsonFiles = Directory.GetFiles(sourceDir, "*.json");

        // ��������� ������ ���� � ����� persistentDataPath
        foreach (string file in jsonFiles)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(saveFolder, fileName);
            if (!File.Exists(destFile))
            {
                File.Copy(file, destFile);
            }
        }
    }

    // ����� ��� ���������� ������ ������ � JSON ����
    public static void SaveListData<T>(List<T> dataList)
    {
        Debug.Log("������ ����� SaveListData");

        // ���������, ���������� �� ����� � �������, ���� �� ����������
        if (!Directory.Exists(saveFolder)) Directory.CreateDirectory(saveFolder);

        // ������� ��� ����� �� ������ ����� ����
        string jsonFileName = typeof(T).Name + "List.json";
        // ������� ������ ���� � �����
        string jsonFilePath = Path.Combine(saveFolder, jsonFileName);

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
        string jsonFilePath = Path.Combine(saveFolder, jsonFileName);

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
        string jsonFilePath = Path.Combine(saveFolder, jsonFileName);

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