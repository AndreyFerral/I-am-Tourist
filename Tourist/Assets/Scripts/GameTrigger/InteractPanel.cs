using DataNamespace;
using System.Collections.Generic;
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
    [SerializeField] DialogBox scriptDB;
    [SerializeField] FinalMenu scriptFM;

    [Header("Other settings")]
    [SerializeField] GameObject eatPanel;
    [SerializeField] GameObject picnicPanel;
    [SerializeField] GameObject campfirePanel;
    [SerializeField] Transform quickSlots;
    [SerializeField] Button backpackButton;

    private Collider2D notifyCollider;

    public void SetItemPick(Collider2D other)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);

        // ������������� �������� ������
        

        // TODO �������� scriptable �� json
        DragHandeler dragHandel = other.GetComponent<DragHandeler>();
        string itemName = dragHandel.ItemInfo.NameItem;
        interactHeader.text = itemName;

        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);

        interactButton.onClick.RemoveAllListeners();

        // ���� ���� ������ ����� � ������ �������
        if (GetEmptySlot() != null)
        {
            buttonText.text = interact.TextPositive;
            interactButton.onClick.AddListener(delegate
            {
                ItemPick(other, itemName);
            });
        }
        else
        {
            buttonText.text = interact.TextNegative;
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
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);
        DialogBoxData dialog = DataLoader.GetDialogBoxData(tag);

        interactHeader.text = interact.TextName;
        scriptDB.StartDialogBox(dialog.TextBefore);

        interactButton.onClick.RemoveAllListeners();

        // ���� ���� ����� � ������ �������
        if (trashCan.CheckQuick() != false)
        {
            buttonText.text = interact.TextPositive;
            interactButton.onClick.AddListener(delegate
            {
                trashCan.UseItems();
                interactPanel.SetActive(false);
            });
        }
        else
        {
            buttonText.text = interact.TextNegative;
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

        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);
        DialogBoxData dialog = DataLoader.GetDialogBoxData(tag);

        // ������������� �������� ������ � ����������� ������� �� �������
        bool isAfterRoute = DataHolder.IsAfterRoute;
        if (!isAfterRoute) interactHeader.text = interact.TextPositive;
        else interactHeader.text = interact.TextNegative;
        buttonText.text = interact.TextName;

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
                scriptDB.StartDialogBox(dialog.TextAfter);
            }
            else
            {
                DataHolder.IsNotifyEnd = true;
                scriptFM.SetWinMenu();
            }
        });
    }

    public void SetFinish(Collider2D other)
    {
        const int textSize = 16;
        ChangeTextSize(textSize);

        // ������������� �������� � ����� ��� ������
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);
        DialogBoxData dialog = DataLoader.GetDialogBoxData(tag);

        interactHeader.text = interact.TextName;
        buttonText.text = interact.TextPositive;

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // ������������� �������� ����� ���������� ��������
            interactPanel.SetActive(false);
            notifyCollider.enabled = true;
            DataHolder.IsAfterRoute = true;

            // ���������� ������ �����
            GameObject player = GameObject.FindWithTag("Player");
            houseOut.transform.position =
                player.transform.position;

            // ������� �� ����� DialogBox ����� ����� ������� �� ������
            scriptDB.StartDialogBox(dialog.TextAfter);
        });
    }

    public void SetEatPanels(Collider2D other, bool isPicnic)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);

        // ������������� �������� ������ �������/������
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);
        interactHeader.text = interact.TextName;
        buttonText.text = interact.TextPositive;

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