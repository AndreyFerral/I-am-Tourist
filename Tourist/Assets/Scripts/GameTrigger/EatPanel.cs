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

    // Метод для использования предметов
    public void UseItems()
    {
        float staminaPlus = eventInfo.PositiveEffect;

        // Проходимся по всем ячейкам
        foreach (Transform slotTransform in useSlots)
        {
            GameObject item =
                slotTransform.GetComponent<Slot>().Item;

            // Если был найден проверяемый объект
            if (item == null) continue;

            // Если объект готов к употреблению
            if (CanUseItem(item, itemsEvent))
            {
                // Уничтожаем вещь
                Destroy(item);
                // Восстанавливаем выносливость
                staminaBar.PlusStamina(staminaPlus);

                if (CanUseItem(item, itemsPack))
                {
                    Debug.Log("Подходит для пакетика");
                    // Создание объекта вместо вещи
                    string namePrefab = "Prefabs/Мусор";
                    GameObject prefab =
                        Resources.Load(namePrefab) as GameObject;
                    Instantiate(prefab, GetEmptySlot(), false);
                }
            }
            else
            {
                // Перемещаем объект на нижние слоты
                item.transform.SetParent(GetEmptySlot());
            }
        }
        // Закрываем окна еды
        scriptIP.ShowEatPanel(false, true);
    }

    // Метод, проверяющий объект на соответствие событию
    private bool CanUseItem(GameObject item, ItemsInfo[] items)
    {
        DragHandeler dragHandel =
            item.GetComponent<DragHandeler>();

        // Проходимся по всем доступным вещам в событии
        foreach (ItemsInfo itemInfo in items)
        {
            // Если вещь доступно событию
            if (dragHandel.ItemInfo == itemInfo) return true;
        }
        return false;
    }

    // Метод, перемещающий вещи в нижние слоты при закрытии окна
    public void ReturnItems()
    {
        // Проходимся по всем ячейкам
        foreach (Transform slotTransform in useSlots)
        {
            GameObject item =
                slotTransform.GetComponent<Slot>().Item;

            // Если был найден проверяемый объект
            if (item == null) continue;

            // Перемещаем объект на нижние слоты
            item.transform.SetParent(GetEmptySlot());
        }
        // Закрываем окна еды
        scriptIP.ShowEatPanel(false, true);
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
}