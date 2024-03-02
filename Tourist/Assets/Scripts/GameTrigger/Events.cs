using UnityEngine;
using System.Collections.Generic;
using DataNamespace;
using System.Linq;

public class Events : MonoBehaviour
{
    private static Transform useSlots;
    private static Transform quickSlots;

    public static void SetSlots(Transform useSlots, Transform quickSlots) 
    {
        Events.useSlots = useSlots;
        Events.quickSlots = quickSlots;
    }

    // ����� ��� ��������� ������� �������� �� ������ �������
    private static List<GameObject> GetItems()
    {
        List<GameObject> items = new List<GameObject>();
        // ���������� �� ���� �������
        foreach (Transform slot in useSlots)
        {
            // ���� ������ �� ������ - ��������� ���
            GameObject item = slot.GetComponent<Slot>().Item;

            // ��������� ������ � ������
            if (item != null) items.Add(item);
        }
        return items;
    }

    public static bool Begin(List<EventsItemsData> listEventsItems)
    {
        // ��������� ����� ���������� 3 ���� (�.�. 3 ����� �����)
        bool isFound = false;
        for (int i = 0; i < 3; i++)
        {
            EventsItemsData combination = FoundCombinations(listEventsItems);
            // ��������� ����, ���� �� ������� ����������
            if (combination == null)
            {
                Debug.Log("���������� �� �������. �����");
                break;
            }

            // ���� ������� ����������
            isFound = true;

            // ��������� �������� ��� ��������� �����������       
            DeleteObject(combination.ItemsToDelete);
            CreateObject(combination.ItemsToCreate);

            // ��������� �������, ���� ����������
            float valueStamina = combination.ValueStamina;
            if (valueStamina != 0)
            {
                StaminaBar.ChangeStamina(valueStamina);
                Debug.Log(combination.EventName + ". ������������ +" + combination.ValueStamina);
            }
        }

        // ���������� ������� � ������ �������� �������
        ReturnItems();

        // ���������� ���� �� ������� ����������
        return isFound;
    }

    private static void DeleteObject(List<string> itemsToDelete)
    {
        if (itemsToDelete == null || itemsToDelete[0] == "") return;
        Debug.Log("����� DeleteObject");

        List<GameObject> items = GetItems();

        foreach (GameObject item in items)
        {
            if (itemsToDelete.Contains(item.name))
            {
                DestroyImmediate(item);
            }
        }

        /*
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (itemsToDelete.Contains(items[i].name))
            {
                DestroyImmediate(items[i]);
                items.RemoveAt(i);
            }
        }
        */
    }

    private static void CreateObject(List<string> itemsToCreate)
    {
        if (itemsToCreate == null || itemsToCreate[0] == "") return;
        Debug.Log("����� CreateObject");

        // �������� ������� � ������ ������
        foreach (string itemName in itemsToCreate)
        {
            string prefabName = "Prefabs/" + itemName;
            GameObject prefab = Resources.Load(prefabName) as GameObject;
            var item = Instantiate(prefab, GetEmptySlot(), false);
            item.gameObject.name = itemName;
        }
    }

    private static EventsItemsData FoundCombinations(List<EventsItemsData> listEventsItems)
    {
        Debug.Log("����� FoundCombinations");

        // ��������� ������ �������� � ������� �������
        List<string> itemNames = GetItems().Select(item => item.name).ToList();

        // ���������� ������ ����������
        foreach (var eventItem in listEventsItems)
        {
            if (eventItem.ItemsToUse.All(item => itemNames.Contains(item)))
            {
                Debug.Log("����������\n");
                Debug.Log("items " + string.Join(", ", itemNames));
                Debug.Log("events " + string.Join(", ", eventItem.ItemsToUse));
                return eventItem;
            }
        }
        return null;
    }

    // ����� ��� ������ ��������
    private static bool CanCraftItem(CraftInfo[] craftInfos)
    {
        List<GameObject> items = GetItems();

        // ���������� �� ���� ��������� �������
        foreach (CraftInfo craftInfo in craftInfos)
        {
            List<bool> matches = new List<bool>();

            // ���������� �� ���� ����� ��� ������
            for (int i = 0; i < craftInfo.CraftItems.Length; i++)
            {
                // ���������� �� ���� ��������
                foreach (GameObject item in items)
                {
                    DragHandeler dragHandel =
                        item.GetComponent<DragHandeler>();

                    Debug.Log(craftInfo.CraftItems[i]);
                    Debug.Log(dragHandel.ItemInfo.name);

                    if (craftInfo.CraftItems[i] ==
                        dragHandel.ItemInfo)
                    {
                        matches.Add(true);
                        Debug.Log("true");
                        break;
                    }

                }
            }
            // ���� ���� ����������, �� �����
            if (matches.Count == craftInfo.CraftItems.Length)
            {
                // �������� �������� ����� ������
                int length = craftInfo.DeleteItems.Length;
                for (int i = 0; i < length; i++)
                {
                    // ���������� �� ���� ��������
                    foreach (GameObject item in items)
                    {
                        DragHandeler dragHandel =
                            item.GetComponent<DragHandeler>();
                        // ���� ������ �������� � ��������
                        if (craftInfo.DeleteItems[i] ==
                            dragHandel.ItemInfo)
                        {
                            DestroyImmediate(item);
                            break;
                        }
                    }
                }
                //craftedItem = craftInfo.CraftedItem;
                return true;
            }
        }
        return false;
    }

    // ����� ��� ������ ����
    private static void CraftGameObject(ItemsInfo itemsInfo)
    {
        // �������� ������� � ������ ������
        string itemName = itemsInfo.name;
        string prefabName = "Prefabs/" + itemName;
        GameObject prefab =
            Resources.Load(prefabName) as GameObject;
        var item = Instantiate(prefab, GetEmptySlot(), false);
        item.gameObject.name = itemName;
    }

    // �����, ������������ ������ ���� � ������ ������
    private static Transform GetEmptySlot()
    {
        // ���������� �� ���� ������� ����� ���������
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // ���������� ������ ����, ���� ��� ������ ����������� ������
            if (item == null) return slot;
        }
        return null;
    }

    // �����, ������������ ���� � ������ ����� ��� �������� ����
    public static void ReturnItems()
    {
        // ���������� �� ���� �������
        foreach (Transform slotTransform in useSlots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().Item;

            // ���������� ������ �� ������ �����
            if (item != null) item.transform.SetParent(GetEmptySlot());
        }
    }
}