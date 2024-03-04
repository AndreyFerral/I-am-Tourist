using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTrans;
    private Canvas canvasComp;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    public static GameObject itemBeginDragged;
    public static GameObject itemStartParent;

    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasComp = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Обработа начала перетаскивания
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Устанавливаем значения перетаксиваемого объекта
        itemBeginDragged = gameObject;
        itemStartParent = transform.parent.gameObject;
        startPosition = transform.position;

        // Изменяем свойства компонентов
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
        canvasComp.overrideSorting = true;
        canvasComp.sortingOrder = 11;

        if (Inventory.IsOpen)
        {
            Debug.Log("OnBeginDrag WriteInformation");
            ItemsController.WriteInformation();

            if (!ItemsController.CanDrag(itemBeginDragged))
            {
                Debug.Log("OnDrag запрещен");

                // Возвращаем переменные к изначальным значениям
                OnEndDrag(eventData);

                // Убираем перетаскиваемый объект
                eventData.pointerDrag = null;
            }
        }
    }

    // Обработка перетаскивания
    public void OnDrag(PointerEventData eventData)
    {
        // Изменяем положение объекта
        Vector3 vec =
            Camera.main.WorldToScreenPoint(rectTrans.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        rectTrans.position = Camera.main.ScreenToWorldPoint(vec);
    }

    // Обработка завершения перетаскивания
    public void OnEndDrag(PointerEventData eventData)
    {
        // Устанавливаем значения перетаксиваемого объекта
        itemBeginDragged = null;

        // Изменяем свойства компонентов
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        canvasComp.overrideSorting = false;

        // Возвращаем объект, если он был отпущен вне слота
        transform.position = startPosition;
    }
}