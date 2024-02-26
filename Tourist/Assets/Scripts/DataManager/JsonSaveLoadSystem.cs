using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public static class JsonSaveLoadSystem
{
    // Получаем путь к папкам StreamingAssets и persistentDataPath
    private static string sourceDir = Path.Combine(Application.streamingAssetsPath, "JSON");
    private static string saveFolder = Path.Combine(Application.persistentDataPath, "JSON");

    // Метод для переноса файлов JSON на устройство пользователя
    public static void CreateJSON()
    {
        Debug.Log("Вызван метод CreateJSON");

        // Проверяем, существует ли папка и создаем, если не существует
        if (!Directory.Exists(saveFolder)) Directory.CreateDirectory(saveFolder);

        // Получаем все JSON-файлы из папки StreamingAssets
        string[] jsonFiles = Directory.GetFiles(sourceDir, "*.json");

        // Переносим каждый файл в папку persistentDataPath
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

    // Метод для сохранения списка данных в JSON файл
    public static void SaveListData<T>(List<T> dataList)
    {
        Debug.Log("Вызван метод SaveListData");

        // Проверяем, существует ли папка и создаем, если не существует
        if (!Directory.Exists(saveFolder)) Directory.CreateDirectory(saveFolder);

        // Создаем имя файла на основе имени типа
        string jsonFileName = typeof(T).Name + "List.json";
        // Создаем полный путь к файлу
        string jsonFilePath = Path.Combine(saveFolder, jsonFileName);

        // Сериализуем список в JSON
        string jsonData = JsonConvert.SerializeObject(dataList);

        // Записываем JSON в файл
        File.WriteAllText(jsonFilePath, jsonData);
    }

    // Метод для добавления данных в конец списка и сохранения обновленного списка
    public static void AddDataToList<T>(T newData)
    {
        Debug.Log("Вызван метод AddDataToList");

        // Создаем имя файла на основе имени типа
        string jsonFileName = typeof(T).Name + "List.json";
        // Создаем полный путь к файлу
        string jsonFilePath = Path.Combine(saveFolder, jsonFileName);

        List<T> dataList;

        if (File.Exists(jsonFilePath))
        {
            // Читаем JSON из файла
            string jsonData = File.ReadAllText(jsonFilePath);
            // Десериализуем JSON в список объектов
            dataList = JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            // Если файл не существует, создаем новый список
            dataList = new List<T>();
        }

        // Добавляем новые данные в список
        dataList.Add(newData);

        // Сериализуем обновленный список в JSON
        string updatedJsonData = JsonConvert.SerializeObject(dataList);
        // Записываем JSON обратно в файл
        File.WriteAllText(jsonFilePath, updatedJsonData);
    }

    // Метод для загрузки списка данных из JSON файла
    public static List<T> LoadListData<T>()
    {
        Debug.Log("Вызван метод LoadListData");

        // Создаем имя файла на основе имени типа
        string jsonFileName = typeof(T).Name + "List.json";
        // Создаем полный путь к файлу
        string jsonFilePath = Path.Combine(saveFolder, jsonFileName);

        if (File.Exists(jsonFilePath))
        {
            // Читаем JSON из файла
            string jsonData = File.ReadAllText(jsonFilePath);
            // Десериализуем JSON в список объектов
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            Debug.LogError("Файл не найден: " + jsonFileName);
            // Возвращаем пустой список в случае ошибки
            return new List<T>();
        }
    }
}