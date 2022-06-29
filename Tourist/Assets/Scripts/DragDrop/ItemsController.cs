using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    private static int idBackpack;
    private static List<string> items;
    [SerializeField] Transform[] smallPanel;
    [SerializeField] Transform[] mediumPanel;
    [SerializeField] Transform[] largePanel;
    [SerializeField] static Transform[] slots;
    private bool isRestore = false;

    public static Transform[] Slots => slots;

    private void Start()
    {
        idBackpack = DataHolder.IdBackpack;
        items = DataHolder.Items;

        if (idBackpack == 0) slots = smallPanel;
        else if (idBackpack == 1) slots = mediumPanel;
        else if (idBackpack == 2) slots = largePanel;
    }

    public void Update()
    {
        // ���� ������ ������, �� ���������� ��� ����������
        if (Inventory.IsOpen) WriteInformation();
    }

    public static void WriteInformation()
    {
        List<string> tempItems = new List<string>();

        // ���������� �� ���� �������� ��������
        foreach (Transform children in slots)
        {
            // ���������� �� ���� ����� ���������
            foreach (Transform slotTransform in children)
            {
                // ���� ������ ��������� � ������
                GameObject item =
                    slotTransform.GetComponent<Slot>().Item;

                // �������� ��� ��������
                if (item) tempItems.Add(item.name);
                // �������� �������� "0"
                else tempItems.Add("0");
            }
        }
        DataHolder.Items = tempItems;
        items = DataHolder.Items;
    }

    public void RestoreInformation()
    {
        // ���� ������ �� ���� �������������
        if (isRestore == false)
        {
            int counter = 0;

            // ���������� �� ���� �������� ��������
            foreach (Transform children in slots)
            {
                // ���������� �� ���� ����� ���������
                foreach (Transform slot in children)
                {
                    Debug.Log(counter + " " + items[counter]);

                    // ���� �������� ��������
                    if (items[counter] != "0")
                        RestoreItem(items[counter], slot);

                    counter++;
                }
            }
            Debug.Log("���������� ��������� �������������");
        }
        // ������ ���� �������������
        isRestore = true;
    }

    private void RestoreItem(string nameItem, Transform slot)
    {
        // �������������� �������
        string namePrefab = "Prefabs/" + nameItem;
        GameObject prefab =
            Resources.Load(namePrefab) as GameObject;
        Instantiate(prefab, slot, false);
    }

    private static int GetIdItem(GameObject draggedItem)
    {
        int counter = 0;

        // ���������� �� ���� �������� ��������
        foreach (Transform children in slots)
        {
            // ���������� �� ���� ����� ���������
            foreach (Transform slotTransform in children)
            {
                GameObject item =
                    slotTransform.GetComponent<Slot>().Item;

                // ���� ��� ������ ����������� ������
                if (draggedItem == item) return counter;
                counter++;
            }
        }
        return -1;
    }

    private static int GetIdSlot(GameObject slot)
    {
        int counter = 0;

        // ���������� �� ���� �������� ��������
        foreach (Transform children in slots)
        {
            // ���������� �� ���� ����� ���������
            foreach (Transform slotTransform in children)
            {
                // ���� ��� ������ ����������� ������
                if (slot == slotTransform.gameObject)
                {
                    return counter;
                }
                counter++;
            }
        }
        return -1;
    }

    public static bool CanDrag(GameObject draggedItem)
    {
        items = DataHolder.Items;
        int idItem = GetIdItem(draggedItem);

        // ���� ��� ��������� ������
        if (idBackpack == 0)
        {
            // ��������� �������� ������� �������
            if (idItem >= 2 && idItem <= 5)
            {
                // �������� �������� ��� ���������
                if (items[idItem - 2] == "0") return true;
                else return false;
            }
            // ��������� ������ ������� �������
            else if (idItem >= 8)
            {
                // �������� �������� ��� ���������
                if (items[idItem - 2] == "0") return true;
                else return false;
            }
        }
        // ���� ��� ������� ������
        else if (idBackpack == 1)
        {
            // ��������� �������� ����� �������
            if (idItem >= 2 && idItem <= 6)
            {
                // �������� ������� ����
                if (idItem <= 3)
                {
                    if (items[idItem - 2] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 4)
                {
                    if (items[2] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 5)
                {
                    if (items[2] == "0" && items[3] == "0")
                        return true;
                    else return false;
                }
                // �������� �������� �������� �������� ���� 
                else if (idItem == 6)
                {
                    if (items[3] == "0") return true;
                    else return false;
                }
            }
            // ��������� ������ ����� �������
            else if (idItem >= 9 && idItem <= 13)
            {
                // �������� ������� ����
                if (idItem <= 10)
                {
                    if (items[idItem - 2] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 11)
                {
                    if (items[9] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 12)
                {
                    if (items[9] == "0" && items[10] == "0")
                        return true;
                    else return false;
                }
                // �������� �������� �������� �������� ���� 
                else if (idItem == 13)
                {
                    if (items[10] == "0") return true;
                    else return false;
                }
            }
        }
        // ���� ��� ������� ������
        else if (idBackpack == 2)
        {
            // ��������� �������� ����� �������
            if (idItem >= 2 && idItem <= 9)
            {
                // �������� ������� ����
                if (idItem <= 3)
                {
                    if (items[idItem - 2] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 4)
                {
                    if (items[2] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 5)
                {
                    if (items[2] == "0" && items[3] == "0")
                        return true;
                    else return false;
                }
                // �������� �������� �������� �������� ���� 
                else if (idItem == 6)
                {
                    if (items[3] == "0") return true;
                    else return false;
                }
                // �������� ���������� ����
                else if (idItem <= 9)
                {
                    if (items[idItem - 3] == "0") return true;
                    else return false;
                }
            }
            // ��������� ������ ����� �������
            else if (idItem >= 12 && idItem <= 19)
            {
                // �������� ������� ����
                if (idItem <= 13)
                {
                    if (items[idItem - 2] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 14)
                {
                    if (items[12] == "0") return true;
                    else return false;
                }
                // �������� ������� �������� �������� ���� 
                else if (idItem == 15)
                {
                    if (items[12] == "0" && items[13] == "0")
                        return true;
                    else return false;
                }
                // �������� �������� �������� �������� ���� 
                else if (idItem == 16)
                {
                    if (items[13] == "0") return true;
                    else return false;
                }
                // �������� ���������� ����
                else if (idItem <= 19)
                {
                    if (items[idItem - 3] == "0") return true;
                    else return false;
                }
            }
        }
        return true;
    }

    public static bool CanDrop(GameObject slotTrigger)
    {
        items = DataHolder.Items;
        int idSlot = GetIdSlot(slotTrigger);

        // ���� ������ ��� ��������� � ������ �����
        if (idSlot == -1) return true;

        // ���������� �� ������� ����� ���� ������ ��������
        GameObject oldSlot = DragHandeler.itemStartParent;
        int idSlotOld = GetIdSlot(oldSlot);

        // ���� ������ �� ��� ��������� � ������ ������
        if (idSlotOld != -1) items[idSlotOld] = "0";

        // ���� ��� ��������� ������
        if (idBackpack == 0)
        {
            // ��������� �������� ������� �������
            if (idSlot <= 3)
            {
                // �������� �������� ��� ���������
                if (items[idSlot + 2] != "0") return true;
                else return false;
            }
            // ��������� ������ ������� �������
            else if (idSlot >= 6 && idSlot <= 9)
            {
                // �������� �������� ��� ���������
                if (items[idSlot + 2] != "0") return true;
                else return false;
            }
        }
        // ���� ��� ������� ������
        else if (idBackpack == 1)
        {
            // ��������� �������� ����� �������
            if (idSlot <= 3)
            {
                // �������� ������� ����
                if (idSlot <= 1)
                {
                    if (items[idSlot + 2] != "0") return true;
                    else return false;
                }
                // �������� ������� �������� ������� ����
                else if (idSlot == 2)
                {
                    if (items[4] != "0" && items[5] != "0")
                        return true;
                    else return false;
                }
                // �������� ������� �������� ������� ���� 
                else if (idSlot == 3)
                {
                    if (items[5] != "0" && items[6] != "0")
                        return true;
                    else return false;
                }
            }
            // ��������� ������ ����� �������
            else if (idSlot >= 7 && idSlot <= 10)
            {
                // �������� ������� ����
                if (idSlot <= 8)
                {
                    if (items[idSlot + 2] != "0") return true;
                    else return false;
                }
                // �������� ������� �������� ������� ����
                else if (idSlot == 9)
                {
                    if (items[11] != "0" && items[12] != "0")
                        return true;
                    else return false;
                }
                // �������� ������� �������� ������� ���� 
                else if (idSlot == 10)
                {
                    if (items[12] != "0" && items[13] != "0")
                        return true;
                    else return false;
                }
            }
        }
        // ���� ��� ������� ������
        else if (idBackpack == 2)
        {
            // ��������� �������� ����� �������
            if (idSlot <= 6)
            {
                // �������� ������� ����
                if (idSlot <= 1)
                {
                    if (items[idSlot + 2] != "0") return true;
                    else return false;
                }
                // �������� ������� �������� ������� ����
                else if (idSlot == 2)
                {
                    if (items[4] != "0" && items[5] != "0")
                        return true;
                    else return false;
                }
                // �������� ������� �������� ������� ���� 
                else if (idSlot == 3)
                {
                    if (items[5] != "0" && items[6] != "0")
                        return true;
                    else return false;
                }
                // �������� �������� ���� 
                else if (idSlot <= 6)
                {
                    if (items[idSlot + 3] != "0") return true;
                    else return false;
                }
            }
            // ��������� ������ ����� �������
            else if (idSlot >= 10 && idSlot <= 16)
            {
                // �������� ������� ����
                if (idSlot <= 11)
                {
                    if (items[idSlot + 2] != "0") return true;
                    else return false;
                }
                // �������� ������� �������� ������� ����
                else if (idSlot == 12)
                {
                    if (items[14] != "0" && items[15] != "0")
                        return true;
                    else return false;
                }
                // �������� ������� �������� ������� ���� 
                else if (idSlot == 13)
                {
                    if (items[15] != "0" && items[16] != "0")
                        return true;
                    else return false;
                }
                // �������� ������� ���� 
                else if (idSlot <= 16)
                {
                    if (items[idSlot + 3] != "0") return true;
                    else return false;
                }
            }
        }
        return true;
    }
}