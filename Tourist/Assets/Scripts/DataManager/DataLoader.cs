using DataNamespace;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private static List<InteractPanelData> loadedInteractList;
    private static List<DialogBoxData> loadedDialogList;

    void Start()
    {
        // «агружаем данные из базы данных при запуске похода
        loadedInteractList = JsonSaveLoadSystem.LoadListData<InteractPanelData>();
        loadedDialogList = JsonSaveLoadSystem.LoadListData<DialogBoxData>();
        Debug.Log("«агрузка данных произошла успешно");
    }

    public static InteractPanelData GetInteractPanelData(string tag)
    {
        foreach (InteractPanelData item in loadedInteractList)
        {
            if (item.TagName == tag) return item;
        }
        return null;
    }

    public static DialogBoxData GetDialogBoxData(string tag)
    {
        foreach (DialogBoxData item in loadedDialogList)
        {
            if (item.TagName == tag) return item;
        }
        return null;
    }
}