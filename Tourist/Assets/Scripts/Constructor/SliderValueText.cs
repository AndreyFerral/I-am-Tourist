using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    private Slider slider;
    private TMP_Text textComp;
    private string text;

    void Awake()
    {
        textComp = GetComponentInChildren<TMP_Text>();
        slider = GetComponentInChildren<Slider>();
        text = textComp.text;

        if (slider != null)
        {
            slider.onValueChanged.AddListener(UpdateText);
        }
    }

    void Start()
    {
        UpdateText(slider.value);
    }

    void UpdateText(float val)
    {
        if (textComp != null)
        {
            textComp.text = text + val.ToString();
        }
    }
}