using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DataNamespace;

public class TwoSlotPanel : MonoBehaviour
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

    private TMP_Text buttonText;
    private static  EventsData curEvent;
    private static EventsInfoData first, second;
    private static List<EventsItemsData> firstItems, secondItems;

    private bool isFirstActive = false;

    void Start()
    {
        buttonText = useButton.GetComponentInChildren<TMP_Text>();

        // Устанавливаем названия 
        headerText.text = curEvent.VisibleName;
        buttonText.text = first.TextButtonBefore;

        // Передаем в скрипт используемые и ячейки быстрого доступа
        Events.SetSlots(useSlots, quickSlots);

        // Удаляем слушателей
        useButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        // Устанавливаем слушателя на кнопки
        useButton.onClick.AddListener(First);
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void SetParam(string name)
    {
        Debug.Log("TwoSlotPanel SetParam");

        // TODO Поставил заглушку, т.к. не на чем проверить
        name = "Campfire";

        // Устанавливаем текущее события для получения названия
        curEvent = DataLoader.GetEventsData(name);

        // Формируем список событий для названий кнопок
        first = DataLoader.GetEventsInfoData(name, 0);
        second = DataLoader.GetEventsInfoData(name, 1);

        // Формируем список возможных исходов у события
        firstItems = DataLoader.GetListEventsItemsData(name, 0);
        secondItems = DataLoader.GetListEventsItemsData(name, 1);
    }

    public void FirstButton()
    {
        if (!isFirstActive) buttonText.text = first.TextButtonBefore;
        else buttonText.text = first.TextButtonAfter;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(First);
    }

    public void SecondButton()
    {
        buttonText.text = second.TextButtonBefore;
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Second);
    }

    private void First()
    {
        Debug.Log("Нажата первая кнопка " + curEvent.EventName);
        if (!isFirstActive && Events.Begin(firstItems))
        {
            isFirstActive = true;
            buttonText.text = first.TextButtonAfter;
        }
        else if (!isFirstActive) buttonText.text = first.TextButtonBefore;
    }

    private void Second()
    {
        Debug.Log("Нажата вторая кнопка " + curEvent.EventName);
        if (isFirstActive && Events.Begin(secondItems))
        {
            isFirstActive = false;
            Debug.Log("Приготовлен");
        }
        else Debug.Log("Не приготовлен");
    }

    private void SetButtonText(string text) 
    {
        // Если значение не пустое, то мы его устанавливаем
        if (text != null || text != "")
        {
            buttonText.text = text;
        }
    }

    private void ClosePanel()
    {
        // Возвращаем объекты из ячеек события
        Events.ReturnItems();
        // Закрываем панель события
        scriptIP.ShowEatPanel(false, 1);
    }
}