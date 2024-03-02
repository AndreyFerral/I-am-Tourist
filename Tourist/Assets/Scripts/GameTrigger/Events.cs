using UnityEngine;
using System.Collections.Generic;
using DataNamespace;
using System.Linq;

public class Events : MonoBehaviour
{
    private static Transform useSlots;
    private static Transform quickSlots;

    public static void SetSlots(Transform useSlots, Transform quickSlots) 
    {
        Events.useSlots = useSlots;
        Events.quickSlots = quickSlots;
    }

    // Метод для получения игровых объектов на панели события
    private static List<GameObject> GetItems()
    {
        List<GameObject> items = new List<GameObject>();
        // Проходимся по всем ячейкам
        foreach (Transform slot in useSlots)
        {
            // Если объект не пустой - добавляем его
            GameObject item = slot.GetComponent<Slot>().Item;

            // Добавляем объект в список
            if (item != null) items.Add(item);
        }
        return items;
    }

    public static bool Begin(List<EventsItemsData> listEventsItems)
    {
        // Выполняем поиск комбинаций 3 раза (т.к. 3 слота всего)
        bool isFound = false;
        for (int i = 0; i < 3; i++)
        {
            EventsItemsData combination = FoundCombinations(listEventsItems);
            // Завершаем цикл, если не находим комбинацию
            if (combination == null)
            {
                Debug.Log("Совпадений не найдено. Конец");
                break;
            }

            // Была найдена комбинация
            isFound = true;

            // Выполняем действие над найденной комбинацией       
            DeleteObject(combination.ItemsToDelete);
            CreateObject(combination.ItemsToCreate);

            // Добавляем стамину, если необходимо
            float valueStamina = combination.ValueStamina;
            if (valueStamina != 0)
            {
                StaminaBar.ChangeStamina(valueStamina);
                Debug.Log(combination.EventName + ". Выносливость +" + combination.ValueStamina);
            }
        }

        // Возвращаем объекты в панель быстрого доступа
        ReturnItems();

        // Возвращаем была ли найдена комбинация
        return isFound;
    }

    private static void DeleteObject(List<string> itemsToDelete)
    {
        if (itemsToDelete == null || itemsToDelete[0] == "") return;
        Debug.Log("Вызов DeleteObject");

        List<GameObject> items = GetItems();

        foreach (GameObject item in items)
        {
            if (itemsToDelete.Contains(item.name))
            {
                DestroyImmediate(item);
            }
        }

        /*
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (itemsToDelete.Contains(items[i].name))
            {
                DestroyImmediate(items[i]);
                items.RemoveAt(i);
            }
        }
        */
    }

    private static void CreateObject(List<string> itemsToCreate)
    {
        if (itemsToCreate == null || itemsToCreate[0] == "") return;
        Debug.Log("Вызов CreateObject");

        // Создание объекта в нижних слотах
        foreach (string itemName in itemsToCreate)
        {
            string prefabName = "Prefabs/" + itemName;
            GameObject prefab = Resources.Load(prefabName) as GameObject;
            var item = Instantiate(prefab, GetEmptySlot(), false);
            item.gameObject.name = itemName;
        }
    }

    private static EventsItemsData FoundCombinations(List<EventsItemsData> listEventsItems)
    {
        Debug.Log("Вызов FoundCombinations");

        // Формируем список объектов в ячейках события
        List<string> itemNames = GetItems().Select(item => item.name).ToList();

        // Возвращаем первое совпадение
        foreach (var eventItem in listEventsItems)
        {
            if (eventItem.ItemsToUse.All(item => itemNames.Contains(item)))
            {
                Debug.Log("СОВПАДЕНИЕ\n");
                Debug.Log("items " + string.Join(", ", itemNames));
                Debug.Log("events " + string.Join(", ", eventItem.ItemsToUse));
                return eventItem;
            }
        }
        return null;
    }

    // Метод для крафта объектов
    private static bool CanCraftItem(CraftInfo[] craftInfos)
    {
        List<GameObject> items = GetItems();

        // Проходимся по всем доступным крафтам
        foreach (CraftInfo craftInfo in craftInfos)
        {
            List<bool> matches = new List<bool>();

            // Проходимся по всем вещам для крафта
            for (int i = 0; i < craftInfo.CraftItems.Length; i++)
            {
                // Проходимся по всем объектам
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
            // Если есть совпадения, то успех
            if (matches.Count == craftInfo.CraftItems.Length)
            {
                // Удаление объектов после крафта
                int length = craftInfo.DeleteItems.Length;
                for (int i = 0; i < length; i++)
                {
                    // Проходимся по всем объектам
                    foreach (GameObject item in items)
                    {
                        DragHandeler dragHandel =
                            item.GetComponent<DragHandeler>();
                        // Если объект подходит к удалению
                        if (craftInfo.DeleteItems[i] ==
                            dragHandel.ItemInfo)
                        {
                            DestroyImmediate(item);
                            break;
                        }
                    }
                }
                //craftedItem = craftInfo.CraftedItem;
                return true;
            }
        }
        return false;
    }

    // Метод для крафта пищи
    private static void CraftGameObject(ItemsInfo itemsInfo)
    {
        // Создание объекта в нижних слотах
        string itemName = itemsInfo.name;
        string prefabName = "Prefabs/" + itemName;
        GameObject prefab =
            Resources.Load(prefabName) as GameObject;
        var item = Instantiate(prefab, GetEmptySlot(), false);
        item.gameObject.name = itemName;
    }

    // Метод, возвращающий пустой слот с нижней панели
    private static Transform GetEmptySlot()
    {
        // Проходимся по всем ячейкам кроме последней
        for (int i = 0; i < quickSlots.childCount - 1; i++)
        {
            Transform slot = quickSlots.GetChild(i);
            GameObject item = slot.GetComponent<Slot>().Item;

            // Возвращаем пустой слот, если был найден проверяемый объект
            if (item == null) return slot;
        }
        return null;
    }

    // Метод, перемещающий вещи в нижние слоты при закрытии окна
    public static void ReturnItems()
    {
        // Проходимся по всем ячейкам
        foreach (Transform slotTransform in useSlots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().Item;

            // Перемещаем объект на нижние слоты
            if (item != null) item.transform.SetParent(GetEmptySlot());
        }
    }
}