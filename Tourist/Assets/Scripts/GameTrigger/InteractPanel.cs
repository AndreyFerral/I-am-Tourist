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
    [SerializeField] GameObject onePanel;
    [SerializeField] GameObject twoPanel;
    [SerializeField] GameObject threePanel;
    [SerializeField] Transform quickSlots;
    [SerializeField] Button backpackButton;

    private Collider2D notifyCollider;

    public void SetItemPick(Collider2D other)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);
        
        // �������� �������� �������
        string objectName = other.name;
        string[] tokens = objectName.Split(' ');
        objectName = tokens[0];

        // ������������� �������� �������
        interactHeader.text = objectName;

        // ������������� �������� ������
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);

        interactButton.onClick.RemoveAllListeners();

        // ���� ���� ������ ����� � ������ �������
        if (GetEmptySlot() != null)
        {
            buttonText.text = interact.TextPositive;
            interactButton.onClick.AddListener(delegate
            {
                ItemPick(other, objectName);
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

    private void ItemPick(Collider2D other, string nameItem)
    {
        // ���������� ����
        Destroy(other.gameObject);

        // �������� ������� � ������ ������
        string namePrefab = "Prefabs/" + nameItem;
        GameObject prefab = Resources.Load(namePrefab) as GameObject;
        var item = Instantiate(prefab, GetEmptySlot(), false);
        item.gameObject.name = nameItem;
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

        interactHeader.text = interact.VisibleName;
        scriptDB.StartDialogBox(dialog.TextBefore);

        interactButton.onClick.RemoveAllListeners();

        var eventItems = DataLoader.GetListEventsItemsData(tag);

        // ���� ���� ����� � ������ �������
        if (trashCan.CheckQuick(eventItems) != false)
        {
            buttonText.text = interact.TextPositive;
            interactButton.onClick.AddListener(delegate
            {
                trashCan.UseItems(eventItems);
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
        buttonText.text = interact.VisibleName;

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

        interactHeader.text = interact.VisibleName;
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

    public void SetEatPanels(Collider2D other, int idPanel)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);

        // ������������� �������� ������ �������/������
        string name = other.gameObject.name;
        InteractPanelData interact = DataLoader.GetInteractPanelData(name);
        interactHeader.text = interact.VisibleName;
        buttonText.text = interact.TextPositive;

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // ��������� ������
            ShowEatPanel(true, idPanel);

            switch (idPanel)
            {
                case 0:
                    OneSlotPanel oneSlotPanel = new OneSlotPanel();
                    oneSlotPanel.SetParam(other.gameObject.name);
                    break;
                case 1:
                    TwoSlotPanel twoSlotPanel = new TwoSlotPanel();
                    twoSlotPanel.SetParam(other.gameObject.name);
                    break;
                case 2:
                    ThreeSlotPanel threeSlotPanel = new ThreeSlotPanel();
                    threeSlotPanel.SetParam(other.gameObject.name);
                    break;
            }
        });
    }

    public void ShowEatPanel(bool enable, int idPanel)
    {
        // ���������/��������� ������ ��������������
        interactPanel.SetActive(!enable);

        // ���������/��������� ������
        eatPanel.SetActive(enable);

        switch (idPanel)
        {
            case 0:
                onePanel.SetActive(enable);
                break;
            case 1:
                twoPanel.SetActive(enable);
                break;
            case 2:
                threePanel.SetActive(enable);
                break;
        }

        // ���������/�������� ������ ������ �������
        backpackButton.interactable = !enable;
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