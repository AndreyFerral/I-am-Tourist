using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    [Header("Interact Panel")]
    [SerializeField] GameObject interactPanel;
    [SerializeField] TMP_Text interactHeader;
    [SerializeField] Button interactButton;
    [SerializeField] TMP_Text buttonText;

    [Header("Finish")]
    [SerializeField] GameObject houseOut;
    [SerializeField] StaminaBar scriptSB;
    [SerializeField] DialogBox scriptDB;
    [SerializeField] FinalMenu scriptFM;

    [Header("Other settings")]
    [SerializeField] GameObject eatPanel;
    [SerializeField] GameObject picnicPanel;
    [SerializeField] GameObject campfirePanel;
    [SerializeField] Transform quickSlots;
    [SerializeField] Button backpackButton;

    private Collider2D notifyCollider;
    private string openName = "�������";

    private string[] pickupNames = {
        "���������", "��� �����"
    };

    private string[] trashNames = {
        "��������� �����", "��� ������"
    };

    private string[] notifyNames = {
        "�������� � ������ ������", "�������� � ����� ������",
        "������������� � ���"
    };

    private string[] finishNames = {
        "������� �������", "�����"
    };

    public void SetItemPick(Collider2D other)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);

        // ������������� �������� ������
        DragHandeler dragHandel =
            other.GetComponent<DragHandeler>();
        string itemName = dragHandel.ItemInfo.NameItem;

        interactHeader.text = itemName;
        interactButton.onClick.RemoveAllListeners();

        // ���� ���� ������ ����� � ������ �������
        if (GetEmptySlot() != null)
        {
            buttonText.text = pickupNames[0];
            interactButton.onClick.AddListener(delegate
            {
                ItemPick(other, itemName);
            });
        }
        else
        {
            buttonText.text = pickupNames[1];
            interactButton.onClick.AddListener(delegate
            {
                interactPanel.SetActive(false);
            });
        }
    }

    private void ItemPick(Collider2D other, string itemName)
    {
        // ���������� ����
        Destroy(other.gameObject);

        // �������� ������� � ������ ������
        string namePrefab = "Prefabs/" + itemName;
        GameObject prefab =
            Resources.Load(namePrefab) as GameObject;
        Instantiate(prefab, GetEmptySlot(), false);
    }

    public void SetTrashCan(Collider2D other)
    {
        const int textSize = 18;
        ChangeTextSize(textSize);

        // ������������� �������� ������
        TrashCan trashCan = other.GetComponent<TrashCan>();
        string eventName = trashCan.EventInfo.NameEvent;

        interactHeader.text = eventName;
        interactButton.onClick.RemoveAllListeners();
        scriptDB.StartDialogBox(scriptDB.TrashDB[0]);

        // ���� ���� ����� � ������ �������
        if (trashCan.CheckQuick() != false)
        {
            buttonText.text = trashNames[0];
            interactButton.onClick.AddListener(delegate
            {
                trashCan.UseItems();
                interactPanel.SetActive(false);
            });
        }
        else
        {
            buttonText.text = trashNames[1];
            interactButton.onClick.AddListener(delegate
            {
                interactPanel.SetActive(false);
            });
        }
    }

    public void SetNotify(Collider2D other)
    {
        const int textSize = 14;
        ChangeTextSize(textSize);

        // ������������� �������� ������
        bool isAfterRoute = DataHolder.IsAfterRoute;
        int �fterRoute = isAfterRoute ? 1 : 0;
        interactHeader.text = notifyNames[�fterRoute];
        buttonText.text = notifyNames[2];

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // ��������� ��������� � ������ ��������������
            notifyCollider = other.GetComponent<BoxCollider2D>();
            notifyCollider.enabled = false;
            interactPanel.SetActive(false);

            // �������� � ��������� � ������/����� ��������
            if (!isAfterRoute)
            {
                DataHolder.IsNotifyStart = true;
                scriptDB.StartDialogBox(scriptDB.NotifyDB[2]);
            }
            else
            {
                DataHolder.IsNotifyEnd = true;
                scriptFM.SetWinMenu();
            }
        });
    }

    public void SetFinish()
    {
        const int textSize = 16;
        ChangeTextSize(textSize);

        // ������������� �������� ������
        interactHeader.text = finishNames[0];
        buttonText.text = finishNames[1];

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // ������������� �������� ����� ���������� ��������
            interactPanel.SetActive(false);
            scriptSB.SetMaxStamina();
            notifyCollider.enabled = true;
            DataHolder.IsAfterRoute = true;

            // ���������� ������ �����
            GameObject player = GameObject.FindWithTag("Player");
            houseOut.transform.position =
                player.transform.position;

            // ������� �� ����� DialogBox
            scriptDB.StartDialogBox(scriptDB.NotifyDB[1]);
        });
    }

    public void SetEatPanels(bool isPicnic)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);

        // ������������� �������� ������
        EventInfo eventInfo;
        if (isPicnic) eventInfo = picnicPanel.
                GetComponent<EatPanel>().EventInfo;
        else eventInfo = campfirePanel.
                GetComponent<CampfirePanel>().EventInfo;
        string eventName = eventInfo.NameEvent;

        interactHeader.text = eventName;
        buttonText.text = openName;

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // ��������� ������
            ShowEatPanel(true, isPicnic);
        });
    }

    public void ShowEatPanel(bool show, bool isPicnic)
    {
        // true - ��������� ������ ��������������
        // false - ��������� ������ ��������������
        interactPanel.SetActive(!show);

        // true - ��������� ������
        // false - ��������� ������
        eatPanel.SetActive(show);
        if (isPicnic) picnicPanel.SetActive(show);
        else campfirePanel.SetActive(show);

        // true - ��������� ������ ������ �������
        // false - �������� ������ ������ �������
        backpackButton.interactable = !show;
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

    private void ChangeTextSize(int textSize)
    {
        interactHeader.fontSize = textSize + 2;
        buttonText.fontSize = textSize;
    }
}