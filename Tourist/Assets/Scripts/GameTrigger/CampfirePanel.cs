using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CampfirePanel : MonoBehaviour
{
    [Header("Craft Info")]
    [SerializeField] CraftInfo[] campfireCraft;
    [SerializeField] CraftInfo[] cookCraft;
    [SerializeField] CraftInfo[] eatCraft;

    [Header("Slots")]
    [SerializeField] Transform useSlots;
    [SerializeField] Transform quickSlots;

    [Header("Campfire Panel")]
    [SerializeField] Button useButton;
    [SerializeField] TMP_Text textButton;

    [Header("Other")]
    [SerializeField] EventInfo eventInfo;
    [SerializeField] InteractPanel scriptIP;

    public EventInfo EventInfo => eventInfo;

    private ItemsInfo craftedItem;
    private bool isFire = false;

    private string[] textButtons = {
        "�������", "�������", "�����������", "������"
    };

    void Start()
    {
        CampfireButton();
    }

    public void CampfireButton()
    {
        if (!isFire) textButton.text = textButtons[0];
        else textButton.text = textButtons[1];

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Campfire);
    }

    public void CookButton()
    {
        textButton.text = textButtons[2];
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Cook);
    }

    public void UseButton()
    {
        textButton.text = textButtons[3];
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Use);
    }

    private void Campfire()
    {
        Debug.Log("����� �������� �� ������");
        if (!isFire && CanCraftItem(campfireCraft))
        {
            isFire = true;
            textButton.text = textButtons[1];
        }
        else if (!isFire) textButton.text = textButtons[0];
        ReturnItems();
    }

    private void Cook()
    {
        if (isFire && CanCraftItem(cookCraft))
        {
            Craft(craftedItem);
            isFire = false;
            Debug.Log("�����������");
        }
        else Debug.Log("�� �����������");
        ReturnItems();
    }

    private void Use()
    {
        if (CanCraftItem(eatCraft))
        {
            float staminaPlus = eventInfo.PositiveEffect;
            StaminaBar.ChangeStamina(staminaPlus);
            Debug.Log("������");
        }
        else Debug.Log("�� ������");
        ReturnItems();
    }

    // ����� ��� ��������� ������� �������� �� ������ �������
    private List<GameObject> GetItems()
    {
        List<GameObject> items = new List<GameObject>();
        // ���������� �� ���� �������
        foreach (Transform slot in useSlots)
        {
            GameObject item = slot.GetComponent<Slot>().Item;
            // ���� ��� ������ ����������� ������
            if (item == null) continue;
            items.Add(item);
        }
        return items;
    }

    // ����� ��� ������ ��������
    private bool CanCraftItem(CraftInfo[] craftInfos)
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
                            Destroy(item);
                            break;
                        }
                    }
                }
                craftedItem = craftInfo.CraftedItem;
                return true;
            }
        }
        return false;
    }

    // ����� ��� ������ ����
    private void Craft(ItemsInfo itemsInfo)
    {
        // �������� ������� � ������ ������
        string itemName = itemsInfo.name;
        string namePrefab = "Prefabs/" + itemName;
        GameObject prefab =
            Resources.Load(namePrefab) as GameObject;
        Instantiate(prefab, GetEmptySlot(), false);
    }

    // �����, ������������ ������ ���� � ������ ������
    private Transform GetEmptySlot()
    {
        // ���������� �� ���� ������� ����� ���������
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // ���� ��� ������ ����������� ������
            if (item != null) continue;

            // ���������� ������ ����
            return slot;
        }
        return null;
    }

    // �����, ������������ ���� � ������ ����� ��� �������� ����
    public void ReturnItems()
    {
        // ���������� �� ���� �������
        foreach (Transform slotTransform in useSlots)
        {
            GameObject item =
                slotTransform.GetComponent<Slot>().Item;

            // ���� ��� ������ ����������� ������
            if (item == null) continue;

            // ���������� ������ �� ������ �����
            item.transform.SetParent(GetEmptySlot());
        }
    }

    public void CloseCampfirePanel()
    {
        ReturnItems();
        // ��������� ���� ���
        scriptIP.ShowEatPanel(false, false);
    }
}