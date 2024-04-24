using DataNamespace;
using UnityEngine;
using System.Collections.Generic;

public class TrashCan : MonoBehaviour
{
    private DialogBox dialogBox;
    private Transform quickSlots;
    private bool isTrashDrop = false;

    public bool IsTrashDrop => isTrashDrop;

    public static bool IsBrook = false;
    public static bool IsRain = false;

    void Start()
    {
        // Сделано так, чтобы работало из префаба
        dialogBox = FindObjectOfType<DialogBox>();
        GameObject quickPanel = GameObject.Find("QuickPanel");
        if (quickPanel != null) quickSlots = quickPanel.transform;
    }

    public void Brook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Brook");
        var eventItems = DataLoader.GetListEventsItemsData("Brook");

        if (!CheckQuick(eventItems))
        {
            // Если пользователь на речке без сапог
            IsBrook = true;
            dialogBox.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // Если пользователь на речке в сапогах
            IsBrook = false;
            dialogBox.StartDialogBox(dialog.TextAfter);
        }
    }

    public void Rain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData("Rain");
        var eventItems = DataLoader.GetListEventsItemsData("Rain");

        if (!CheckQuick(eventItems))
        {
            // Если пользователь под дождем
            IsRain = true;
            dialogBox.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // Если пользователь под дождем в плаще или с зонтом
            IsRain = false;
            dialogBox.StartDialogBox(dialog.TextAfter);
        }
    }

    // Метод для использования предметов
    public void UseItems(List<EventsItemsData> eventItems)
    {
        // Указываем, что часть мусора была выброшена
        isTrashDrop = true;

        // Проходимся по всем ячейкам кроме последней
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // Если был найден проверяемый объект
            if (item == null) continue;

            // Если объект соответствует событию
            if (CanUseItem(eventItems, item)) Destroy(item);
        }
    }

    // Метод, проверяющий нижнию панель на соответствие событию
    private bool CanUseItem(List<EventsItemsData> eventItems, GameObject item)
    {
        foreach (var _event in eventItems)
        {
            if (_event.ItemsToUse.Contains(item.name))
            {
                return true;
            }
        }
        return false;
    }

    // Метод, проверяющий нижнию панель на наличие мусора
    public bool CheckQuick(List<EventsItemsData> eventItems)
    {
        // Проходимся по всем ячейкам кроме последней
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // Если был найден проверяемый объект
            if (item == null) continue;

            // Если объект подходит событию
            if (CanUseItem(eventItems, item)) return true;
        }
        return false;
    }

    public bool CheckBackpack(List<EventsItemsData> eventItems)
    {
        List<Transform> slots = ItemsController.Slots;

        // Проходимся по всем дочерним объектам
        foreach (Transform children in slots)
        {
            // Проходимся по всем ячеек инвентаря
            foreach (Transform slotTransform in children)
            {
                // Если объект находится в ячейке
                GameObject item =
                    slotTransform.GetComponent<Slot>().Item;

                // Если был найден проверяемый объект
                if (item == null) continue;

                // Если объект соответствует событию
                if (CanUseItem(eventItems, item)) return true;
            }
        }
        return false;
    }
}