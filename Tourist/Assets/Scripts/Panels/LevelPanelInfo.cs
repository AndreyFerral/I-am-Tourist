using DataNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelInfo : MonoBehaviour
{
    private TMP_Text nameTMP;
    private TMP_Text descriptionTMP;
    private TMP_Text infoTMP;
    private Button button;
    private ToggleGroup toggleGroup;

    public void Initialize(LevelData levelData)
    {
        nameTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        descriptionTMP = transform.GetChild(1).GetComponent<TMP_Text>();
        infoTMP = transform.GetChild(2).GetComponent<TMP_Text>();
        button = GetComponentInChildren<Button>();
        toggleGroup = GetComponentInChildren<ToggleGroup>();

        nameTMP.text = levelData.nameMap;
        descriptionTMP.text = levelData.descriptionMap;

        int minValue = 35;
        int maxValue = 198;
        int step = (maxValue - minValue) / 3;

        int value = levelData.heightMap + levelData.widthMap;
        int level = (value - minValue) / step + 1;
        level = Mathf.Clamp(level, 1, 3);

        string levelText = "Сложность: " + level.ToString();
        string resultText = levelData.isPassed ? "Пройден" : "Не пройден";
        infoTMP.text = levelText + "\n" + resultText;

        // Активируем переключатель
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        toggles[levelData.idBackpack].isOn = true;

        button.onClick.AddListener(() => Play(levelData));

        ResizeText(nameTMP);
        ResizeText(descriptionTMP);
    }

    private void Play(LevelData levelData)
    {
        DataHolder.levelData = levelData;
        DataHolder.IdLocation = 4;
        DataHolder.IdBackpack = levelData.idBackpack;
        MainMenu.SelectItems();
    }

    void ResizeText(TMP_Text textMeshPro)
    {
        float textHeight = textMeshPro.GetPreferredValues().y; // Получение предпочтительной высоты текста 
        float currentHeight = textMeshPro.rectTransform.sizeDelta.y; // Текущая высота текстового поля

        if (textHeight > currentHeight)
        {
            textMeshPro.rectTransform.sizeDelta = new Vector2(textMeshPro.rectTransform.sizeDelta.x, textHeight); // Изменение высоты текстового поля
        }
    }
}