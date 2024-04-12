using DataNamespace;
using UnityEngine;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    private DialogBox dialogBox;
    private Transform quickSlots;
    private bool isTrashDrop = false;

    public bool IsTrashDrop => isTrashDrop;

    public static bool IsBrook = false;
    public static bool IsRain = false;

    //private bool isRainOld;
    //private bool isBrookOld;

    void Start()
    {
        // ������� ���, ����� �������� �� �������
        dialogBox = FindObjectOfType<DialogBox>();
        GameObject quickPanel = GameObject.Find("QuickPanel");
        if (quickPanel != null) quickSlots = quickPanel.transform;
    }

    public void Brook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Brook");
        var eventItems = DataLoader.GetListEventsItemsData("Brook");

        if (!CheckQuick(eventItems))
        {
            // ���� ������������ �� ����� ��� �����
            IsBrook = true;
            dialogBox.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // ���� ������������ �� ����� � �������
            IsBrook = false;
            dialogBox.StartDialogBox(dialog.TextAfter);
        }
        //isBrookOld = IsBrook;
    }

    public void Rain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Rain");
        var eventItems = DataLoader.GetListEventsItemsData("Rain");

        if (!CheckQuick(eventItems))
        {
            // ���� ������������ ��� ������
            IsRain = true;
            dialogBox.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // ���� ������������ ��� ������ � ����� ��� � ������
            IsRain = false;
            dialogBox.StartDialogBox(dialog.TextAfter);
        }
        //isRainOld = IsRain;
    }

    /*
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
            if (!CheckQuick(eventItems)) dialogBox.StartDialogBox(dialog.TextBefore);
            else dialogBox.StartDialogBox(dialog.TextAfter);
        }
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
            if (!CheckQuick(eventItems)) dialogBox.StartDialogBox(dialog.TextBefore);
            else dialogBox.StartDialogBox(dialog.TextAfter);
        }
    }
    */

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