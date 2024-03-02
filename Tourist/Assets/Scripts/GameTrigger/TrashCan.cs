using DataNamespace;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] EventInfo eventInfo;
    [SerializeField] Transform quickSlots;
    [SerializeField] DialogBox scriptDB;
    private bool isTrashDrop = false;

    public bool IsTrashDrop => isTrashDrop;
    //public EventInfo EventInfo => eventInfo;

    public static bool IsBrook;
    private bool isBrookOld;
    public static bool IsRain;
    private bool isRainOld;

    void Start()
    {
        IsBrook = false;
        IsRain = false;
    }

    public void CheckBrook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        // Обновляем значение 
        if (!CheckQuick()) IsBrook = true;
        else IsBrook = false;

        // Если значение изменилось выводим сообщение
        if (isBrookOld != IsBrook)
        {
            isBrookOld = IsBrook;
            if (!CheckQuick()) scriptDB.StartDialogBox(dialog.TextBefore);
            else scriptDB.StartDialogBox(dialog.TextAfter);
        }
    }

    public void StartBrook()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        if (!CheckQuick())
        {
            // Если пользователь на речке без сапог
            IsBrook = true;
            scriptDB.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // Если пользователь на речке в сапогах
            IsBrook = false;
            scriptDB.StartDialogBox(dialog.TextAfter);
        }
        isBrookOld = IsBrook;
    }

    public void CheckRain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        // Обновляем значение 
        if (!CheckQuick()) IsRain = true;
        else IsRain = false;

        // Если значение изменилось выводим сообщение
        if (isRainOld != IsRain)
        {
            isRainOld = IsRain;
            if (!CheckQuick()) scriptDB.StartDialogBox(dialog.TextBefore);
            else scriptDB.StartDialogBox(dialog.TextAfter);
        }
    }

    public void StartRain()
    {
        DialogBoxData dialog = DataLoader.GetDialogBoxData(gameObject.tag);

        if (!CheckQuick())
        {
            // Если пользователь на речке без сапог
            IsRain = true;
            scriptDB.StartDialogBox(dialog.TextBefore);
        }
        else
        {
            // Если пользователь на речке в сапогах
            IsRain = false;
            scriptDB.StartDialogBox(dialog.TextAfter);
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
        DragHandeler dragHandel = item.GetComponent<DragHandeler>();

        DataLoader.GetItemData(item.name);

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
                if (CanUseItem(item)) return true;
            }
        }
        return false;
    }
}