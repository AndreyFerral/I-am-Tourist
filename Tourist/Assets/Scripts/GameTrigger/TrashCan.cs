using DataNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] Transform quickSlots;
    [SerializeField] DialogBox scriptDB;
    private bool isTrashDrop = false;

    public bool IsTrashDrop => isTrashDrop;

    public static bool IsBrook;
    private bool isBrookOld;
    public static bool IsRain;
    private bool isRainOld;


    void Start()
    {
        IsBrook = false;
        IsRain = false;
    }

    public void CheckBrook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Brook");
        var eventItems = DataLoader.GetListEventsItemsData("Brook");

        // ��������� �������� 
        if (!CheckQuick(eventItems)) IsBrook = true;
        else IsBrook = false;

        // ���� �������� ���������� ������� ���������
        if (isBrookOld != IsBrook)
        {
            isBrookOld = IsBrook;
            if (!CheckQuick(eventItems)) scriptDB.StartDialogBox(dialog.TextBefore);
            else scriptDB.StartDialogBox(dialog.TextAfter);
        }
    }

    public void StartBrook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Brook");
        var eventItems = DataLoader.GetListEventsItemsData("Brook");

        if (!CheckQuick(eventItems))
        {
            // ���� ������������ �� ����� ��� �����
            IsBrook = true;
            scriptDB.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // ���� ������������ �� ����� � �������
            IsBrook = false;
            scriptDB.StartDialogBox(dialog.TextAfter);
        }
        isBrookOld = IsBrook;
    }

    public void CheckRain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Rain");
        var eventItems = DataLoader.GetListEventsItemsData("Rain");

        // ��������� �������� 
        if (!CheckQuick(eventItems)) IsRain = true;
        else IsRain = false;

        // ���� �������� ���������� ������� ���������
        if (isRainOld != IsRain)
        {
            isRainOld = IsRain;
            if (!CheckQuick(eventItems)) scriptDB.StartDialogBox(dialog.TextBefore);
            else scriptDB.StartDialogBox(dialog.TextAfter);
        }
    }

    public void StartRain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Rain");
        var eventItems = DataLoader.GetListEventsItemsData("Rain");

        if (!CheckQuick(eventItems))
        {
            // ���� ������������ �� ����� ��� �����
            IsRain = true;
            scriptDB.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // ���� ������������ �� ����� � �������
            IsRain = false;
            scriptDB.StartDialogBox(dialog.TextAfter);
        }
        isRainOld = IsRain;
    }

    // ����� ��� ������������� ���������
    public void UseItems(List<EventsItemsData> eventItems)
    {
        // ���������, ��� ����� ������ ���� ���������
        isTrashDrop = true;

        // ���������� �� ���� ������� ����� ���������
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // ���� ��� ������ ����������� ������
            if (item == null) continue;

            // ���� ������ ������������� �������
            if (CanUseItem(eventItems, item)) Destroy(item);
        }
    }

    // �����, ����������� ������ ������ �� ������������ �������
    private bool CanUseItem(List<EventsItemsData> eventItems, GameObject item)
    {
        foreach (var _event in eventItems)
        {
            if (_event.ItemsToUse.Contains(item.name))
            {
                return true;
            }
        }
        return false;
    }

    // �����, ����������� ������ ������ �� ������� ������
    public bool CheckQuick(List<EventsItemsData> eventItems)
    {
        // ���������� �� ���� ������� ����� ���������
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // ���� ��� ������ ����������� ������
            if (item == null) continue;

            // ���� ������ �������� �������
            if (CanUseItem(eventItems, item)) return true;
        }
        return false;
    }

    public bool CheckBackpack(List<EventsItemsData> eventItems)
    {
        List<Transform> slots = ItemsController.Slots;

        // ���������� �� ���� �������� ��������
        foreach (Transform children in slots)
        {
            // ���������� �� ���� ����� ���������
            foreach (Transform slotTransform in children)
            {
                // ���� ������ ��������� � ������
                GameObject item =
                    slotTransform.GetComponent<Slot>().Item;

                // ���� ��� ������ ����������� ������
                if (item == null) continue;

                // ���� ������ ������������� �������
                if (CanUseItem(eventItems, item)) return true;
            }
        }
        return false;
    }
}