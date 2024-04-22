using UnityEngine;
using UnityEngine.UI;

public class ButtonScaler : MonoBehaviour
{
    private Button[] buttons;
    private Button selectedButton;
    private float selectedScale = 1.25f; // Желаемый увеличенный масштаб кнопки в состоянии Selected

    void Start()
    {
        buttons = GetComponentsInChildren<Button>(); // Находим все компоненты кнопок в дочерних элементах

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ToggleButtonSelection(button));
        }
    }

    void ToggleButtonSelection(Button clickedButton)
    {
        if (selectedButton == clickedButton)
        {
            return; // Не переключать состояние, если кнопка уже была выбрана
        }

        if (selectedButton != null)
        {
            selectedButton.transform.localScale = Vector3.one; // Возвращаем масштаб предыдущей выбранной кнопки к обычному значению
            Animator buttonAnimator = selectedButton.GetComponent<Animator>();

            if (buttonAnimator != null)
            {
                buttonAnimator.SetTrigger("Normal"); // Устанавливаем состояние "Normal" для предыдущей выбранной кнопки
            }
        }

        selectedButton = clickedButton;

        // Увеличиваем масштаб выбранной кнопки
        selectedButton.transform.localScale = new Vector3(selectedScale, selectedScale, selectedScale);

        // Проигрываем анимацию Scale у выбранной кнопки
        Animator selectedButtonAnimator = selectedButton.GetComponent<Animator>();
        if (selectedButtonAnimator != null)
        {
            selectedButtonAnimator.SetTrigger("Selected"); // Устанавливаем состояние "Selected" для выбранной кнопки
        }
    }
}
