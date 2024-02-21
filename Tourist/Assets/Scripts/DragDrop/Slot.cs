using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    private static GameObject itemBeginDrag;

    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Запрещаем OnDrop, если он не соответствует правилам функции
        if (Inventory.IsOpen && !ItemsController.CanDrop(eventData.pointerEnter))
        {
            Debug.Log("OnDrop запрещен");
            return;
        }

        // Если на слоте нет предметов
        if (!Item)
        {
            // Записываем информацию по содержимому инвентарю
            if (Inventory.IsOpen)
            {
                Debug.Log("OnDrop WriteInformation");
                ItemsController.WriteInformation();
            }

            itemBeginDrag = DragHandeler.itemBeginDragged;
            itemBeginDrag.transform.SetParent(transform);
        }
    }
}