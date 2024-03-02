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

        // ������������� �������� 
        headerText.text = curEvent.VisibleName;
        buttonText.text = eventInfo.TextButtonBefore;

        // �������� � ������ ������������ � ������ �������� �������
        Events.SetSlots(useSlots, quickSlots);

        // ������� ����������
        useButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        // ������������� ��������� �� ������
        useButton.onClick.AddListener(() => Events.Begin(eventItems));
        closeButton.onClick.AddListener(ClosePanel);
    }

    public void SetParam(string name)
    {
        Debug.Log("OneSlotPanel SetParam");

        // ������������� ������� ������� ��� ��������� ��������
        curEvent = DataLoader.GetEventsData(name);

        // ��������� ������ ������� ��� �������� ������������ ������
        eventInfo = DataLoader.GetEventsInfoData(name);

        // ��������� ������ ��������� ������� � �������
        eventItems = DataLoader.GetListEventsItemsData(name);
    }

    private void ClosePanel()
    {
        // ���������� ������� �� ����� �������
        Events.ReturnItems();
        // ��������� ������ �������
        scriptIP.ShowEatPanel(false, 0);
    }
}