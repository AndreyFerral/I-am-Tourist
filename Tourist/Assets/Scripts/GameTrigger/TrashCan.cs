using System.Collections;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] EventInfo eventInfo;
    [SerializeField] Transform quickSlots;
    [SerializeField] DialogBox scriptDB;
    private bool isTrashDrop = false;

    public bool IsTrashDrop => isTrashDrop;
    public EventInfo EventInfo => eventInfo;

    public static bool IsBrook;
    private bool isBrookOld;
    public static bool IsRain;
    private bool isRainOld;

    private void CheckEvent(
        bool isEvent, bool isEventOld, string[] message)
    {
        // ��������� �������� 
        if (!CheckQuick()) isEvent = true;
        else isEvent = false;

        // ���� �������� ���������� ������� ���������
        if (isEventOld != isEvent)
        {
            isEventOld = isEvent;
            if (!CheckQuick())
                scriptDB.StartDialogBox(message[0]);
            else scriptDB.StartDialogBox(message[1]);
        }
    }

    private void StartEvent(
        bool isEvent, bool isEventOld, string[] message)
    {
        if (!CheckQuick())
        {
            // ���� ������������ �� ����� ��� �����
            isEvent = true;
            scriptDB.StartDialogBox(message[0]);
        }
        else
        {
            // ���� ������������ �� ����� � �������
            isEvent = false;
            scriptDB.StartDialogBox(message[1]);
        }
        isEventOld = isEvent;
    }


    public void CheckBrook()
    {
        // ��������� �������� 
        if (!CheckQuick()) IsBrook = true;
        else IsBrook = false;

        // ���� �������� ���������� ������� ���������
        if (isBrookOld != IsBrook)
        {
            isBrookOld = IsBrook;
            if (!CheckQuick())
                scriptDB.StartDialogBox(scriptDB.BrookDB[0]);
            else scriptDB.StartDialogBox(scriptDB.BrookDB[1]);
        }
    }

    public void StartBrook()
    {
        if (!CheckQuick())
        {
            // ���� ������������ �� ����� ��� �����
            IsBrook = true;
            scriptDB.StartDialogBox(scriptDB.BrookDB[0]);
        }
        else
        {
            // ���� ������������ �� ����� � �������
            IsBrook = false;
            scriptDB.StartDialogBox(scriptDB.BrookDB[1]);
        }
        isBrookOld = IsBrook;
    }

    public void CheckRain()
    {
        // ��������� �������� 
        if (!CheckQuick()) IsRain = true;
        else IsRain = false;

        // ���� �������� ���������� ������� ���������
        if (isRainOld != IsRain)
        {
            isRainOld = IsRain;
            if (!CheckQuick())
                scriptDB.StartDialogBox(scriptDB.RainDB[0]);
            else scriptDB.StartDialogBox(scriptDB.RainDB[1]);
        }
    }

    public void StartRain()
    {
        if (!CheckQuick())
        {
            // ���� ������������ �� ����� ��� �����
            IsRain = true;
            scriptDB.StartDialogBox(scriptDB.RainDB[0]);
        }
        else
        {
            // ���� ������������ �� ����� � �������
            IsRain = false;
            scriptDB.StartDialogBox(scriptDB.RainDB[1]);
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
        DragHandeler dragHandel =
            item.GetComponent<DragHandeler>();

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
        Transform[] slots = ItemsController.Slots;

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