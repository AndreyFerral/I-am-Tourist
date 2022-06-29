using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTrans;
    [SerializeField] Color bgActiveColor;
    [SerializeField] Color handleActiveColor;

    private Image bgImage, handleImage;
    private Color bgDefaultColor, handleDefaultColor;
    private Toggle toggle;
    private Vector2 handlePosition;

    void Awake()
    {
        // Определяем значения переменным
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTrans.anchoredPosition;

        bgImage =
            uiHandleRectTrans.parent.GetComponent<Image>();
        handleImage =
            uiHandleRectTrans.GetComponent<Image>();

        bgDefaultColor = bgImage.color;
        handleDefaultColor = handleImage.color;

        // Присоединяем слушатель
        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    // Метод для перемещения флажка
    void OnSwitch(bool on)
    {
        // Изменяем положение флажка
        uiHandleRectTrans.DOAnchorPos(
            on ? handlePosition * -1 : handlePosition, .4f).
            SetEase(Ease.InOutBack);

        // Изменяем цвет переключателя
        bgImage.DOColor(
            on ? bgActiveColor : bgDefaultColor, .6f);
        handleImage.DOColor(
            on ? handleActiveColor : handleDefaultColor, .4f);
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}