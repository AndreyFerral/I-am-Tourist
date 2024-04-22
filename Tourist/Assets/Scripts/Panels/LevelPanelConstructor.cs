using DataNamespace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelConstructor : MonoBehaviour
{
    private TMP_Text nameTMP;
    private TMP_Text descriptionTMP;
    private TMP_Text heightAndWidthTMP;
    private Button button;
    private ToggleGroup toggleGroup;

    public void Initialize(LevelData levelData, List<LevelData> loadedLevelDataList)
    {
        nameTMP = transform.GetChild(0).GetComponent<TMP_Text>();
        descriptionTMP = transform.GetChild(1).GetComponent<TMP_Text>();
        heightAndWidthTMP = transform.GetChild(2).GetComponent<TMP_Text>();
        button = GetComponentInChildren<Button>();
        toggleGroup = GetComponentInChildren<ToggleGroup>();

        nameTMP.text = levelData.nameMap;
        descriptionTMP.text = levelData.descriptionMap;
        string heightText = "Высота: " + levelData.heightMap.ToString();
        string widthText = "Ширина: " + levelData.widthMap.ToString();
        heightAndWidthTMP.text = heightText + "\n" + widthText;

        // Активируем переключатель
        Toggle[] toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        toggles[levelData.idBackpack].isOn = true;

        button.onClick.AddListener(() => Delete(levelData, loadedLevelDataList));

        ResizeText(nameTMP);
        ResizeText(descriptionTMP);
    }

    private void Delete(LevelData levelData, List<LevelData> loadedLevelDataList)
    {
        loadedLevelDataList.Remove(levelData);
        JsonSaveLoadSystem.ReplaceListData(loadedLevelDataList);
        DestroyImmediate(gameObject);
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