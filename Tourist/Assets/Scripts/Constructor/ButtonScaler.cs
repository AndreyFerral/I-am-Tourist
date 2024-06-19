using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 normalScale = Vector3.one;
    private Vector3 hoveredScale = new Vector3(0.85f, 0.85f, 0.85f);
    private Vector3 clickedScale = new Vector3(1.15f, 1.15f, 1.15f);
    private float duration = 0.3f;

    private static ButtonScaler clickedButton; // Используем статическую переменную для отслеживания нажатой кнопки

    private Outline outline; // Переменная для компонента Outline

    private void Start()
    {
        transform.localScale = normalScale;

        // Добавляем компонент Outline, если его  нет
        outline = gameObject.GetComponent<Outline>();
        if (outline == null) outline = gameObject.AddComponent<Outline>();

        outline.effectColor = new Color(0, 0, 0, 0.3f);
        outline.effectDistance = new Vector2(1, 1);
        outline.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Если текущая кнопка не является последней нажатой или не была нажата, тогда применяем анимацию
        if (clickedButton != this)
        {
            transform.DOScale(hoveredScale, duration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем кнопку к нормальному размеру только, если она не была последней нажатой
        if (clickedButton != this)
        {
            transform.DOScale(normalScale, duration);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickedButton != null && clickedButton != this)
        {
            // Возвращаем предыдущую нажатую кнопку к нормальному масштабу и отключаем её обводку
            clickedButton.transform.DOScale(normalScale, duration);
            clickedButton.outline.enabled = false;
        }

        transform.DOScale(clickedScale, duration); // Анимация увеличения масштаба при нажатии
        outline.enabled = true; // Включаем обводку
        clickedButton = this; // Запоминаем текущую кнопку как последнюю нажатую
    }
}
