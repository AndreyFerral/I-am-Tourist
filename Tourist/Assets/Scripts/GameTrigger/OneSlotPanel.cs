using DataNamespace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneSlotPanel : MonoBehaviour
{
    [Header("Buttons & Text")]
    [SerializeField] TMP_Text headerText;
    [SerializeField] Button useButton;
    [SerializeField] Button closeButton;

    [Header("Slots")]
    [SerializeField] Transform useSlots;
    [SerializeField] Transform quickSlots;

    [Header("Other")]
    [SerializeField] InteractPanel scriptIP;

    private static EventsData curEvent;
    private static EventsInfoData eventInfo;
    private static List<EventsItemsData> eventItems;

    private void Start()
    {
        Debug.Log("OneSlotPanel Start");
        TMP_Text buttonText = useButton.GetComponentInChildren<TMP_Text>();

        // Устанавливаем названия 
        headerText.text = curEvent.VisibleName;
        buttonText.text = eventInfo.TextButtonBefore;

        // Передаем в скрипт используемые и ячейки быстрого доступа
        Events.SetSlots(useSlots, quickSlots);

        // Удаляем слушателей
        useButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        // Устанавливаем слушателя на кнопки
        useButton.onClick.AddListener(() => Events.Begin(eventItems));
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void SetParam(string name)
    {
        Debug.Log("OneSlotPanel SetParam");

        // Устанавливаем текущее события для получения названия
        curEvent = DataLoader.GetEventsData(name);

        // Формируем список событий для названия используемой кнопки
        eventInfo = DataLoader.GetEventsInfoData(name);

        // Формируем список возможных исходов у события
        eventItems = DataLoader.GetListEventsItemsData(name);
    }

    private void ClosePanel()
    {
        // Возвращаем объекты из ячеек события
        Events.ReturnItems();
        // Закрываем панель события
        scriptIP.ShowEatPanel(false, 0);
    }
}