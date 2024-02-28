using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CampfirePanel : MonoBehaviour
{
    [Header("Craft Info")]
    [SerializeField] CraftInfo[] campfireCraft;
    [SerializeField] CraftInfo[] cookCraft;
    [SerializeField] CraftInfo[] eatCraft;

    [Header("Slots")]
    [SerializeField] Transform useSlots;
    [SerializeField] Transform quickSlots;

    [Header("Campfire Panel")]
    [SerializeField] Button useButton;
    [SerializeField] TMP_Text textButton;

    [Header("Other")]
    [SerializeField] EventInfo eventInfo;
    [SerializeField] InteractPanel scriptIP;

    public EventInfo EventInfo => eventInfo;

    private ItemsInfo craftedItem;
    private bool isFire = false;

    private string[] textButtons = {
        "Разжечь", "Разожжён", "Приготовить", "Съесть"
    };

    void Start()
    {
        CampfireButton();
    }

    public void CampfireButton()
    {
        if (!isFire) textButton.text = textButtons[0];
        else textButton.text = textButtons[1];

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Campfire);
    }

    public void CookButton()
    {
        textButton.text = textButtons[2];
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Cook);
    }

    public void UseButton()
    {
        textButton.text = textButtons[3];
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(Use);
    }

    private void Campfire()
    {
        Debug.Log("Пошла проверка на костер");
        if (!isFire && CanCraftItem(campfireCraft))
        {
            isFire = true;
            textButton.text = textButtons[1];
        }
        else if (!isFire) textButton.text = textButtons[0];
        ReturnItems();
    }

    private void Cook()
    {
        if (isFire && CanCraftItem(cookCraft))
        {
            Craft(craftedItem);
            isFire = false;
            Debug.Log("Приготовлен");
        }
        else Debug.Log("Не приготовлен");
        ReturnItems();
    }

    private void Use()
    {
        if (CanCraftItem(eatCraft))
        {
            float staminaPlus = eventInfo.PositiveEffect;
            StaminaBar.ChangeStamina(staminaPlus);
            Debug.Log("Скушан");
        }
        else Debug.Log("Не скушан");
        ReturnItems();
    }

    // Метод для получения игровых объектов на панели события
    private List<GameObject> GetItems()
    {
        List<GameObject> items = new List<GameObject>();
        // Проходимся по всем ячейкам
        foreach (Transform slot in useSlots)
        {
            GameObject item = slot.GetComponent<Slot>().Item;
            // Если был найден проверяемый объект
            if (item == null) continue;
            items.Add(item);
        }
        return items;
    }

    // Метод для крафта объектов
    private bool CanCraftItem(CraftInfo[] craftInfos)
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
                            Destroy(item);
                            break;
                        }
                    }
                }
                craftedItem = craftInfo.CraftedItem;
                return true;
            }
        }
        return false;
    }

    // Метод для крафта пищи
    private void Craft(ItemsInfo itemsInfo)
    {
        // Создание объекта в нижних слотах
        string itemName = itemsInfo.name;
        string namePrefab = "Prefabs/" + itemName;
        GameObject prefab =
            Resources.Load(namePrefab) as GameObject;
        Instantiate(prefab, GetEmptySlot(), false);
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
    }

    public void CloseCampfirePanel()
    {
        ReturnItems();
        // Закрываем окно еды
        scriptIP.ShowEatPanel(false, false);
    }
}