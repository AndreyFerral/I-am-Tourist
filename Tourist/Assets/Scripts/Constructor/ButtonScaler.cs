using UnityEngine;
using UnityEngine.UI;

public class ButtonScaler : MonoBehaviour
{
    private Button[] buttons;
    private Button selectedButton;
    private float selectedScale = 1.25f; // �������� ����������� ������� ������ � ��������� Selected

    void Start()
    {
        buttons = GetComponentsInChildren<Button>(); // ������� ��� ���������� ������ � �������� ���������

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ToggleButtonSelection(button));
        }
    }

    void ToggleButtonSelection(Button clickedButton)
    {
        if (selectedButton == clickedButton)
        {
            return; // �� ����������� ���������, ���� ������ ��� ���� �������
        }

        if (selectedButton != null)
        {
            selectedButton.transform.localScale = Vector3.one; // ���������� ������� ���������� ��������� ������ � �������� ��������
            Animator buttonAnimator = selectedButton.GetComponent<Animator>();

            if (buttonAnimator != null)
            {
                buttonAnimator.SetTrigger("Normal"); // ������������� ��������� "Normal" ��� ���������� ��������� ������
            }
        }

        selectedButton = clickedButton;

        // ����������� ������� ��������� ������
        selectedButton.transform.localScale = new Vector3(selectedScale, selectedScale, selectedScale);

        // ����������� �������� Scale � ��������� ������
        Animator selectedButtonAnimator = selectedButton.GetComponent<Animator>();
        if (selectedButtonAnimator != null)
        {
            selectedButtonAnimator.SetTrigger("Selected"); // ������������� ��������� "Selected" ��� ��������� ������
        }
    }
}
