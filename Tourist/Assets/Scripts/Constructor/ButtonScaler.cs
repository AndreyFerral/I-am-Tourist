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

    private static ButtonScaler clickedButton; // ���������� ����������� ���������� ��� ������������ ������� ������

    private Outline outline; // ���������� ��� ���������� Outline

    private void Start()
    {
        transform.localScale = normalScale;

        // ��������� ��������� Outline, ���� ���  ���
        outline = gameObject.GetComponent<Outline>();
        if (outline == null) outline = gameObject.AddComponent<Outline>();

        outline.effectColor = new Color(0, 0, 0, 0.3f);
        outline.effectDistance = new Vector2(1, 1);
        outline.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���� ������� ������ �� �������� ��������� ������� ��� �� ���� ������, ����� ��������� ��������
        if (clickedButton != this)
        {
            transform.DOScale(hoveredScale, duration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���������� ������ � ����������� ������� ������, ���� ��� �� ���� ��������� �������
        if (clickedButton != this)
        {
            transform.DOScale(normalScale, duration);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickedButton != null && clickedButton != this)
        {
            // ���������� ���������� ������� ������ � ����������� �������� � ��������� � �������
            clickedButton.transform.DOScale(normalScale, duration);
            clickedButton.outline.enabled = false;
        }

        transform.DOScale(clickedScale, duration); // �������� ���������� �������� ��� �������
        outline.enabled = true; // �������� �������
        clickedButton = this; // ���������� ������� ������ ��� ��������� �������
    }
}
