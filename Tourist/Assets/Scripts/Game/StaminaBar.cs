using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    private float maxStamina = 100;

    public void MinusStamina(float number)
    {
        slider.value = slider.value - number;
    }

    public void PlusStamina(float number)
    {
        slider.value = slider.value + number;
    }

    public void SetMaxStamina()
    {
        slider.value = maxStamina;
    }

    public void SetStamina(float number)
    {
        slider.value = number;
    }

    public float GetStamina()
    {
        return slider.value;
    }
}