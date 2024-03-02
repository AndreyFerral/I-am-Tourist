using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DataNamespace;

public class ThreeSlotPanel : MonoBehaviour
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
    private static EventsInfoData first, second, third;
    private static List<EventsItemsData> firstItems, secondItems, thirdItems;

    private bool isFire = false;

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
        Debug.Log("ThreeSlotPanel SetParam");

        // Устанавливаем текущее события для получения названия
        curEvent = DataLoader.GetEventsData(name);

        // Формируем список событий для названий кнопок
        first = DataLoader.GetEventsInfoData(name, 0);
        second = DataLoader.GetEventsInfoData(name, 1);
        third = DataLoader.GetEventsInfoData(name, 2);

        // Формируем список возможных исходов у события
        firstItems = DataLoader.GetListEventsItemsData(name, 0);
        secondItems = DataLoader.GetListEventsItemsData(name, 1);
        thirdItems = DataLoader.GetListEventsItemsData(name, 2);
    }

    public void FirstButton()
    {
        if (!isFire) buttonText.text = first.TextButtonBefore;
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

    public void ThirdButton()
    {
        buttonText.text = third.TextButtonBefore;
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Third);
    }

    private void First()
    {
        Debug.Log("Пошла проверка на костер");
        if (!isFire && Events.Begin(firstItems))
        {
            isFire = true;
            buttonText.text = first.TextButtonAfter;
        }
        else if (!isFire) buttonText.text = first.TextButtonBefore;
    }

    private void Second()
    {
        if (isFire && Events.Begin(secondItems))
        {
            isFire = false;
            Debug.Log("Приготовлен");
        }
        else Debug.Log("Не приготовлен");
    }

    private void Third()
    {
        if (Events.Begin(thirdItems)) Debug.Log("Скушан");
        else Debug.Log("Не скушан");
    }

    private void ClosePanel()
    {
        // Возвращаем объекты из ячеек события
        Events.ReturnItems();
        // Закрываем панель события
        scriptIP.ShowEatPanel(false, 2);
    }
}