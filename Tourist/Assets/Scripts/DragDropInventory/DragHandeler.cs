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

    // �������� ������ ��������������
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ������������� �������� ���������������� �������
        itemBeginDragged = gameObject;
        itemStartParent = transform.parent.gameObject;
        startPosition = transform.position;

        // �������� �������� �����������
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
                Debug.Log("OnDrag ��������");

                // ���������� ���������� � ����������� ���������
                OnEndDrag(eventData);

                // ������� ��������������� ������
                eventData.pointerDrag = null;
            }
        }
    }

    // ��������� ��������������
    public void OnDrag(PointerEventData eventData)
    {
        // �������� ��������� �������
        Vector3 vec =
            Camera.main.WorldToScreenPoint(rectTrans.position);
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        rectTrans.position = Camera.main.ScreenToWorldPoint(vec);
    }

    // ��������� ���������� ��������������
    public void OnEndDrag(PointerEventData eventData)
    {
        // ������������� �������� ���������������� �������
        itemBeginDragged = null;

        // �������� �������� �����������
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        canvasComp.overrideSorting = false;

        // ���������� ������, ���� �� ��� ������� ��� �����
        transform.position = startPosition;
    }
}