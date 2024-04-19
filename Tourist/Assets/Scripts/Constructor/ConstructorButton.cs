using DataNamespace;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConstructorButton : MonoBehaviour
{
    private List<LevelData> loadedLevelDataList;

    [Header("—оздание уровн€")]
    [SerializeField] TMP_InputField input_name;
    [SerializeField] TMP_InputField input_description;
    [SerializeField] Slider slider_height;
    [SerializeField] Slider slider_width;
    [SerializeField] ToggleGroup toggle_group;

    void Start()
    {
        loadedLevelDataList = JsonSaveLoadSystem.LoadListData<LevelData>();
    }

    public void CreateLevel()
    {
        string level_name = input_name.text;
        string level_description = input_description.text;
        int level_height = (int)slider_height.value;
        int level_width = (int)slider_width.value;

        if (level_name == "" || level_description == "")
        {
            InfoPanel("Ќевозможно создать уровень с пустым названием или описанием");
            return;
        }
        else if (CheckName(level_name))
        {
            InfoPanel("”ровень с данным названием уже существует");
            return;
        }

        Toggle[] toggles = toggle_group.GetComponentsInChildren<Toggle>();
        int toggleIndex = toggles.ToList().FindIndex(toggle => toggle.isOn);

        DataHolder.levelData = new LevelData(level_name, level_description, level_height, level_width, toggleIndex);
        MainMenu.Constructor();
    }

    private void InfoPanel(string text)
    {
        InfoPanel infoPanel = FindObjectOfType<InfoPanel>();
        infoPanel.DisplayText(text);
    }

    private bool CheckName(string name)
    {
        foreach (LevelData item in loadedLevelDataList)
        {
            if (item.nameMap == name) return true;
        }
        return false;
    }
}