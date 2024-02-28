using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private static Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public static void ChangeStamina(float amount)
    {
        slider.value += amount;
    }

    public static void SetMaxStamina()
    {
        slider.value = 100;
    }

    public static float GetStamina()
    {
        return slider.value;
    }
}