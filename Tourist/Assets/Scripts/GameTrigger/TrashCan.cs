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
        // Обновляем значение 
        if (!CheckQuick()) isEvent = true;
        else isEvent = false;

        // Если значение изменилось выводим сообщение
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
            // Если пользователь на речке без сапог
            isEvent = true;
            scriptDB.StartDialogBox(message[0]);
        }
        else
        {
            // Если пользователь на речке в сапогах
            isEvent = false;
            scriptDB.StartDialogBox(message[1]);
        }
        isEventOld = isEvent;
    }


    public void CheckBrook()
    {
        // Обновляем значение 
        if (!CheckQuick()) IsBrook = true;
        else IsBrook = false;

        // Если значение изменилось выводим сообщение
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
            // Если пользователь на речке без сапог
            IsBrook = true;
            scriptDB.StartDialogBox(scriptDB.BrookDB[0]);
        }
        else
        {
            // Если пользователь на речке в сапогах
            IsBrook = false;
            scriptDB.StartDialogBox(scriptDB.BrookDB[1]);
        }
        isBrookOld = IsBrook;
    }

    public void CheckRain()
    {
        // Обновляем значение 
        if (!CheckQuick()) IsRain = true;
        else IsRain = false;

        // Если значение изменилось выводим сообщение
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
            // Если пользователь на речке без сапог
            IsRain = true;
            scriptDB.StartDialogBox(scriptDB.RainDB[0]);
        }
        else
        {
            // Если пользователь на речке в сапогах
            IsRain = false;
            scriptDB.StartDialogBox(scriptDB.RainDB[1]);
        }
        isRainOld = IsRain;
    }

    // Метод для использования предметов
    public void UseItems()
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
            if (CanUseItem(item)) Destroy(item);
        }
    }

    // Метод, проверяющий нижнию панель на соответствие событию
    private bool CanUseItem(GameObject item)
    {
        DragHandeler dragHandel =
            item.GetComponent<DragHandeler>();

        // Проходимся по всем доступным вещам в событии
        foreach (ItemsInfo itemInfo in eventInfo.Items)
        {
            // Если вещь доступно событию
            if (dragHandel.ItemInfo == itemInfo) return true;
        }
        return false;
    }

    // Метод, проверяющий нижнию панель на наличие мусора
    public bool CheckQuick()
    {
        // Проходимся по всем ячейкам кроме последней
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // Если был найден проверяемый объект
            if (item == null) continue;

            // Если объект подходит событию
            if (CanUseItem(item)) return true;
        }
        return false;
    }

    public bool CheckBackpack()
    {
        Transform[] slots = ItemsController.Slots;

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
                if (CanUseItem(item)) return true;
            }
        }
        return false;
    }
}