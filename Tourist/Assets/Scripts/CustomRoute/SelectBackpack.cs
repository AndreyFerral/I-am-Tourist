using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectBackpack : MonoBehaviour,
    IPointerEnterHandler
{
    // ��������� ������
    [SerializeField] GameObject selectedItem;

    // ��� ��������� � �������� � �� ��������
    [SerializeField] GameObject[] items;
    private static int countItems;
    private Vector3[] sizeItems;
    private Vector3 scale;

    // ����������������� ��������, ������������
    private static float duration = 0.6f;
    private static float alphaOld = 1f;
    private static float alphaNew = 0.6f;

    void Start()
    {
        // ����������� �������� ����������
        countItems = items.Length;
        sizeItems = new Vector3[countItems];
        scale = new Vector3(0.2f, 0.2f, 0.2f);

        // ����������� ����������� ������� ��������
        RectTransform[] rectTr;
        rectTr = new RectTransform[countItems];
        for (int i = 0; i < countItems; i++)
        {
            rectTr[i] = items[i].GetComponent<RectTransform>();
            sizeItems[i] = rectTr[i].localScale;
        }

        // ������������� ������ ������ ��� ���������
        selectedItem = items[0];
        DataHolder.IdBackpack = 0;

        // ����������� ������ ������� �������
        RectTransform rt = items[0].GetComponent<RectTransform>();
        Vector3 updateScale = rt.localScale + scale;
        rt.DOScale(updateScale, duration).
            SetEase(Ease.InOutBack);

        // �������� ����������� � ���������
        CanvasGroup bp1 =
            items[1].GetComponent<CanvasGroup>();
        CanvasGroup bp2 =
            items[2].GetComponent<CanvasGroup>();

        bp1.DOFade(alphaNew, duration);
        bp2.DOFade(alphaNew, duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���� ������ ������ ��� ������,
        // �� ��� ������������� ��������� ���
        if (eventData.pointerEnter == selectedItem)
        {
            Debug.Log("������ ������ ��� ������ " +
                eventData.pointerEnter);
            return;
        }

        // ������������� ������, �� ������� ������, ��� ���������
        selectedItem = eventData.pointerEnter;

        // ���������� ����������
        Debug.Log("��������� �� " + eventData.pointerEnter);

        // ���������� �� ���� ��������
        for (int i = 0; i < countItems; i++)
        {
            // ����������� �������� ����������� ��������
            RectTransform rectTrans =
                items[i].GetComponent<RectTransform>();
            CanvasGroup canvasGroup =
                items[i].GetComponent<CanvasGroup>();

            // ��������� ������
            if (eventData.pointerEnter == items[i])
            {
                // ���������� ������������� �������
                DataHolder.IdBackpack = i;

                // ������������� ����������� ������
                Vector3 updateScale = sizeItems[i] + scale;
                rectTrans.DOScale(updateScale, duration).
                    SetEase(Ease.InOutBack);

                // ������������� ����������� ������������
                canvasGroup.DOFade(alphaOld, duration);
            }
            // ��������� �������
            else
            {
                // ������������� ����������� ������
                rectTrans.DOScale(
                    sizeItems[i], duration).
                    SetEase(Ease.InOutBack);

                // ������������� ����������� ������������
                canvasGroup.DOFade(alphaNew, duration);
            }
        }
    }
}