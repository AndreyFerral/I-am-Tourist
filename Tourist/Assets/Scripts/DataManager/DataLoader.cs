using DataNamespace;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private static List<InteractPanelData> loadedInteractList;
    private static List<DialogBoxData> loadedDialogList;
    private static List<BackpackData> loadedBackpackList;
    private static List<ItemData> loadedItemList;

    private static List<EventsData> loadedEventList;
    private static List<EventsInfoData> loadedEventInfoList;
    private static List<EventsItemsData> loadedEventItemList;

    void Start()
    {
        // «агружаем данные из базы данных при запуске похода
        loadedInteractList = JsonSaveLoadSystem.LoadListData<InteractPanelData>();
        loadedDialogList = JsonSaveLoadSystem.LoadListData<DialogBoxData>();
        loadedBackpackList = JsonSaveLoadSystem.LoadListData<BackpackData>();
        loadedItemList = JsonSaveLoadSystem.LoadListData<ItemData>();

        loadedEventList = JsonSaveLoadSystem.LoadListData<EventsData>();
        loadedEventInfoList = JsonSaveLoadSystem.LoadListData<EventsInfoData>();
        loadedEventItemList = JsonSaveLoadSystem.LoadListData<EventsItemsData>();

        Debug.Log("«агрузка данных произошла успешно");
    }

    public static EventsItemsData GetEventsItemsData(string name, int id=0)
    {
        foreach (EventsItemsData item in loadedEventItemList)
        {
            if (item.EventName == name && item.EventInfoId == id) return item;
        }
        return null;
    }

    public static EventsInfoData GetEventsInfoData(string name, int id=0)
    {
        foreach (EventsInfoData item in loadedEventInfoList)
        {
            if (item.EventName == name && item.EventInfoId == id) return item;
        }
        return null;
    }

    public static EventsData GetEventsData(string name)
    {
        foreach (EventsData item in loadedEventList)
        {
            if (item.EventName == name) return item;
        }
        return null;
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
            if (item.VisibleName == name) return item;
        }
        return null;
    }

    public static List<EventsItemsData> GetListEventsItemsData(string name, int id=0)
    {
        List<EventsItemsData> returnList = new List<EventsItemsData>();
        foreach (EventsItemsData item in loadedEventItemList)
        {
            if (item.EventName == name && item.EventInfoId == id) returnList.Add(item);
        }
        return returnList;
    }
}