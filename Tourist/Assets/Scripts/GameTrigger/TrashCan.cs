using DataNamespace;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] EventInfo eventInfo;
    [SerializeField] Transform quickSlots;
    [SerializeField] DialogBox scriptDB;
    private bool isTrashDrop = false;

    public bool IsTrashDrop => isTrashDrop;
    //public EventInfo EventInfo => eventInfo;

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
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        // ��������� �������� 
        if (!CheckQuick()) IsBrook = true;
        else IsBrook = false;

        // ���� �������� ���������� ������� ���������
        if (isBrookOld != IsBrook)
        {
            isBrookOld = IsBrook;
            if (!CheckQuick()) scriptDB.StartDialogBox(dialog.TextBefore);
            else scriptDB.StartDialogBox(dialog.TextAfter);
        }
    }

    public void StartBrook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        if (!CheckQuick())
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
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        // ��������� �������� 
        if (!CheckQuick()) IsRain = true;
        else IsRain = false;

        // ���� �������� ���������� ������� ���������
        if (isRainOld != IsRain)
        {
            isRainOld = IsRain;
            if (!CheckQuick()) scriptDB.StartDialogBox(dialog.TextBefore);
            else scriptDB.StartDialogBox(dialog.TextAfter);
        }
    }

    public void StartRain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        if (!CheckQuick())
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
    public void UseItems()
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
            if (CanUseItem(item)) Destroy(item);
        }
    }

    // �����, ����������� ������ ������ �� ������������ �������
    private bool CanUseItem(GameObject item)
    {
        DragHandeler dragHandel = item.GetComponent<DragHandeler>();

        DataLoader.GetItemData(item.name);

        // ���������� �� ���� ��������� ����� � �������
        foreach (ItemsInfo itemInfo in eventInfo.Items)
        {
            // ���� ���� �������� �������
            if (dragHandel.ItemInfo == itemInfo) return true;
        }
        return false;
    }

    // �����, ����������� ������ ������ �� ������� ������
    public bool CheckQuick()
    {
        // ���������� �� ���� ������� ����� ���������
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // ���� ��� ������ ����������� ������
            if (item == null) continue;

            // ���� ������ �������� �������
            if (CanUseItem(item)) return true;
        }
        return false;
    }

    public bool CheckBackpack()
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
                if (CanUseItem(item)) return true;
            }
        }
        return false;
    }
}