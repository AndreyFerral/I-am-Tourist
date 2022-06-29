using UnityEngine;

public class EatPanel : MonoBehaviour
{
    [SerializeField] StaminaBar staminaBar;
    [SerializeField] Transform useSlots;
    [SerializeField] Transform quickSlots;
    [SerializeField] EventInfo eventInfo;
    [SerializeField] EventInfo packInfo;
    [SerializeField] InteractPanel scriptIP;

    private ItemsInfo[] itemsEvent;
    private ItemsInfo[] itemsPack;

    public EventInfo EventInfo => eventInfo;

    void Start()
    {
        itemsEvent = eventInfo.Items;
        itemsPack = packInfo.Items;
    }

    // ����� ��� ������������� ���������
    public void UseItems()
    {
        float staminaPlus = eventInfo.PositiveEffect;

        // ���������� �� ���� �������
        foreach (Transform slotTransform in useSlots)
        {
            GameObject item =
                slotTransform.GetComponent<Slot>().Item;

            // ���� ��� ������ ����������� ������
            if (item == null) continue;

            // ���� ������ ����� � ������������
            if (CanUseItem(item, itemsEvent))
            {
                // ���������� ����
                Destroy(item);
                // ��������������� ������������
                staminaBar.PlusStamina(staminaPlus);

                if (CanUseItem(item, itemsPack))
                {
                    Debug.Log("�������� ��� ��������");
                    // �������� ������� ������ ����
                    string namePrefab = "Prefabs/�����";
                    GameObject prefab =
                        Resources.Load(namePrefab) as GameObject;
                    Instantiate(prefab, GetEmptySlot(), false);
                }
            }
            else
            {
                // ���������� ������ �� ������ �����
                item.transform.SetParent(GetEmptySlot());
            }
        }
        // ��������� ���� ���
        scriptIP.ShowEatPanel(false, true);
    }

    // �����, ����������� ������ �� ������������ �������
    private bool CanUseItem(GameObject item, ItemsInfo[] items)
    {
        DragHandeler dragHandel =
            item.GetComponent<DragHandeler>();

        // ���������� �� ���� ��������� ����� � �������
        foreach (ItemsInfo itemInfo in items)
        {
            // ���� ���� �������� �������
            if (dragHandel.ItemInfo == itemInfo) return true;
        }
        return false;
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
        // ��������� ���� ���
        scriptIP.ShowEatPanel(false, true);
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
}