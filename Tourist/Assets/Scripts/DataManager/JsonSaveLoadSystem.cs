using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public static class JsonSaveLoadSystem
{
    // Путь к папке для сохранения файлов
    private static string saveFolderPath = Application.persistentDataPath;

    // Метод для сохранения списка данных в JSON файл
    public static void SaveListData<T>(List<T> dataList)
    {
        Debug.Log("Вызван метод SaveListData");

        // Создаем имя файла на основе имени типа
        string jsonFileName = typeof(T).Name + "List.json";
        // Создаем полный путь к файлу
        string jsonFilePath = Path.Combine(saveFolderPath, jsonFileName);
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
        string jsonFilePath = Path.Combine(saveFolderPath, jsonFileName);

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
        string jsonFilePath = Path.Combine(saveFolderPath, jsonFileName);

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