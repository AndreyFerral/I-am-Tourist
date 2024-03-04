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
        
        // Получаем название объекта
        string objectName = other.name;
        string[] tokens = objectName.Split(' ');
        objectName = tokens[0];

        // Устанавливаем название объекта
        interactHeader.text = objectName;

        // Устанавливаем названия панели
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);

        interactButton.onClick.RemoveAllListeners();

        // Если есть пустые слоты в нижних ячейках
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
        // Уничтожаем вещь
        Destroy(other.gameObject);

        // Создание объекта в нижних слотах
        string namePrefab = "Prefabs/" + nameItem;
        GameObject prefab = Resources.Load(namePrefab) as GameObject;
        var item = Instantiate(prefab, GetEmptySlot(), false);
        item.gameObject.name = nameItem;
    }

    public void SetTrashCan(Collider2D other)
    {
        const int textSize = 18;
        ChangeTextSize(textSize);

        // Устанавливаем названия панели
        TrashCan trashCan = other.GetComponent<TrashCan>();
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);
        DialogBoxData dialog = DataLoader.GetDialogBoxData(tag);

        interactHeader.text = interact.VisibleName;
        scriptDB.StartDialogBox(dialog.TextBefore);

        interactButton.onClick.RemoveAllListeners();

        var eventItems = DataLoader.GetListEventsItemsData(tag);

        // Если есть мусор в нижних ячейках
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

        // Устанавливаем названия панели в зависимости пройден ли уровень
        bool isAfterRoute = DataHolder.IsAfterRoute;
        if (!isAfterRoute) interactHeader.text = interact.TextPositive;
        else interactHeader.text = interact.TextNegative;
        buttonText.text = interact.VisibleName;

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // Выключаем коллайдер и панель взаимодействия
            notifyCollider = other.GetComponent<BoxCollider2D>();
            notifyCollider.enabled = false;
            interactPanel.SetActive(false);

            // Помечаем о сообщении в начале/конце маршрута
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

        // Устанавливаем названия и текст для финиша
        string tag = other.gameObject.tag;
        InteractPanelData interact = DataLoader.GetInteractPanelData(tag);
        DialogBoxData dialog = DataLoader.GetDialogBoxData(tag);

        interactHeader.text = interact.VisibleName;
        buttonText.text = interact.TextPositive;

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // Устанавливаем значения после завершения маршрута
            interactPanel.SetActive(false);
            notifyCollider.enabled = true;
            DataHolder.IsAfterRoute = true;

            // Перемещаем игрока домой
            GameObject player = GameObject.FindWithTag("Player");
            houseOut.transform.position =
                player.transform.position;

            // Выводим на экран DialogBox текст после нажатия на кнопку
            scriptDB.StartDialogBox(dialog.TextAfter);
        });
    }

    public void SetEatPanels(Collider2D other, int idPanel)
    {
        const int textSize = 20;
        ChangeTextSize(textSize);

        // Устанавливаем названия панели пикника/костра
        string name = other.gameObject.name;
        InteractPanelData interact = DataLoader.GetInteractPanelData(name);
        interactHeader.text = interact.VisibleName;
        buttonText.text = interact.TextPositive;

        interactButton.onClick.RemoveAllListeners();
        interactButton.onClick.AddListener(delegate
        {
            // Открываем панель
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
        // Закрываем/Открываем панель взаимодействия
        interactPanel.SetActive(!enable);

        // Открываем/закрываем панель
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

        // Отключаем/Включаем кнопку вызова рюкзака
        backpackButton.interactable = !enable;
    }

    // Метод, возвращающий пустой слот с нижней панели
    private Transform GetEmptySlot()
    {
        // Проходимся по всем ячейкам кроме последней
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // Если был найден проверяемый объект
            if (item != null) continue;

            // Возвращаем пустой слот
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