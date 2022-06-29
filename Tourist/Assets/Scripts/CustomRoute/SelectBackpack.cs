using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectBackpack : MonoBehaviour,
    IPointerEnterHandler
{
    // Выбранный объект
    [SerializeField] GameObject selectedItem;

    // Для обращения к объектам и их размерам
    [SerializeField] GameObject[] items;
    private static int countItems;
    private Vector3[] sizeItems;
    private Vector3 scale;

    // Продолжительность анимации, прозрачность
    private static float duration = 0.6f;
    private static float alphaOld = 1f;
    private static float alphaNew = 0.6f;

    void Start()
    {
        // Присваиваем значения переменным
        countItems = items.Length;
        sizeItems = new Vector3[countItems];
        scale = new Vector3(0.2f, 0.2f, 0.2f);

        // Присваиваем стандартные размеры объектам
        RectTransform[] rectTr;
        rectTr = new RectTransform[countItems];
        for (int i = 0; i < countItems; i++)
        {
            rectTr[i] = items[i].GetComponent<RectTransform>();
            sizeItems[i] = rectTr[i].localScale;
        }

        // Устанавливаем первый объект как выбранный
        selectedItem = items[0];
        DataHolder.IdBackpack = 0;

        // Увеличиваем размер первого объекта
        RectTransform rt = items[0].GetComponent<RectTransform>();
        Vector3 updateScale = rt.localScale + scale;
        rt.DOScale(updateScale, duration).
            SetEase(Ease.InOutBack);

        // Уменьшем прорачность у остальных
        CanvasGroup bp1 =
            items[1].GetComponent<CanvasGroup>();
        CanvasGroup bp2 =
            items[2].GetComponent<CanvasGroup>();

        bp1.DOFade(alphaNew, duration);
        bp2.DOFade(alphaNew, duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Если данный объект уже выбран,
        // то нет необходимости выполнять код
        if (eventData.pointerEnter == selectedItem)
        {
            Debug.Log("Данный объект уже выбран " +
                eventData.pointerEnter);
            return;
        }

        // Устанавливаем объект, на который навели, как выбранный
        selectedItem = eventData.pointerEnter;

        // Отладочная информация
        Debug.Log("Наведение на " + eventData.pointerEnter);

        // Проходимся по всем объектам
        for (int i = 0; i < countItems; i++)
        {
            // Присваиваем значения компонентам объектов
            RectTransform rectTrans =
                items[i].GetComponent<RectTransform>();
            CanvasGroup canvasGroup =
                items[i].GetComponent<CanvasGroup>();

            // Выбранный объект
            if (eventData.pointerEnter == items[i])
            {
                // Записываем идентификатор объекта
                DataHolder.IdBackpack = i;

                // Устанавливаем увеличенный размер
                Vector3 updateScale = sizeItems[i] + scale;
                rectTrans.DOScale(updateScale, duration).
                    SetEase(Ease.InOutBack);

                // Устанавливаем стандартную прозрачность
                canvasGroup.DOFade(alphaOld, duration);
            }
            // Остальные объекты
            else
            {
                // Устанавливаем стандартный размер
                rectTrans.DOScale(
                    sizeItems[i], duration).
                    SetEase(Ease.InOutBack);

                // Устанавливаем уменьшенную прозрачность
                canvasGroup.DOFade(alphaNew, duration);
            }
        }
    }
}