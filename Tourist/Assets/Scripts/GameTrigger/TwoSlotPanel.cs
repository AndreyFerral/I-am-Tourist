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

        // ������������� �������� 
        headerText.text = curEvent.VisibleName;
        buttonText.text = first.TextButtonBefore;

        // �������� � ������ ������������ � ������ �������� �������
        Events.SetSlots(useSlots, quickSlots);

        // ������� ����������
        useButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
        // ������������� ��������� �� ������
        useButton.onClick.AddListener(First);
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void SetParam(string name)
    {
        Debug.Log("TwoSlotPanel SetParam");

        // TODO �������� ��������, �.�. �� �� ��� ���������
        name = "Campfire";

        // ������������� ������� ������� ��� ��������� ��������
        curEvent = DataLoader.GetEventsData(name);

        // ��������� ������ ������� ��� �������� ������
        first = DataLoader.GetEventsInfoData(name, 0);
        second = DataLoader.GetEventsInfoData(name, 1);

        // ��������� ������ ��������� ������� � �������
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
        Debug.Log("������ ������ ������ " + curEvent.EventName);
        if (!isFirstActive && Events.Begin(firstItems))
        {
            isFirstActive = true;
            buttonText.text = first.TextButtonAfter;
        }
        else if (!isFirstActive) buttonText.text = first.TextButtonBefore;
    }

    private void Second()
    {
        Debug.Log("������ ������ ������ " + curEvent.EventName);
        if (isFirstActive && Events.Begin(secondItems))
        {
            isFirstActive = false;
            Debug.Log("�����������");
        }
        else Debug.Log("�� �����������");
    }

    private void SetButtonText(string text) 
    {
        // ���� �������� �� ������, �� �� ��� �������������
        if (text != null || text != "")
        {
            buttonText.text = text;
        }
    }

    private void ClosePanel()
    {
        // ���������� ������� �� ����� �������
        Events.ReturnItems();
        // ��������� ������ �������
        scriptIP.ShowEatPanel(false, 1);
    }
}