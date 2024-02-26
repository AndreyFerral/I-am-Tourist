using DataNamespace;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private static List<InteractPanelData> loadedInteractList;
    private static List<DialogBoxData> loadedDialogList;
    private static List<BackpackData> loadedBackpackList;
    private static List<ItemData> loadedItemList;

    void Start()
    {
        // «агружаем данные из базы данных при запуске похода
        loadedInteractList = JsonSaveLoadSystem.LoadListData<InteractPanelData>();
        loadedDialogList = JsonSaveLoadSystem.LoadListData<DialogBoxData>();
        loadedBackpackList = JsonSaveLoadSystem.LoadListData<BackpackData>();
        loadedItemList = JsonSaveLoadSystem.LoadListData<ItemData>();

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

    public static BackpackData GetBackpackData(int id)
    {
        foreach (BackpackData item in loadedBackpackList)
        {
            if (item.IdBackpack == id) return item;
        }
        return null;
    }

    public static ItemData GetItemData(string name)
    {
        foreach (ItemData item in loadedItemList)
        {
            if (item.FileName == name) return item;
        }
        return null;
    }
}