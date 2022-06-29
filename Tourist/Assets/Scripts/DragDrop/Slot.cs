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
        if (Inventory.IsOpen)
        {
            if (!ItemsController.CanDrop(eventData.pointerEnter))
            {
                Debug.Log("OnDrop запрещен");
                return;
            }
        }

        // Если на слоте нет предметов
        if (!Item)
        {
            itemBeginDrag = DragHandeler.itemBeginDragged;
            itemBeginDrag.transform.SetParent(transform);
        }
    }
}