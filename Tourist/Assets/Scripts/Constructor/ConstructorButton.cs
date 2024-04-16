using DataNamespace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConstructorButton : MonoBehaviour
{
    [SerializeField] GameObject info_panel;
    private GameObject info;
    private TMP_Text info_text;
    private List<LevelData> loadedLevelDataList;

    [SerializeField] TMP_InputField input_name;
    [SerializeField] TMP_InputField input_description;
    [SerializeField] Slider slider_height;
    [SerializeField] Slider slider_width;

    private string level_name;
    private string level_description;
    private int level_height;
    private int level_width;

    void Start()
    {
        info = info_panel.transform.GetChild(0).gameObject;
        info_text = info.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        loadedLevelDataList = JsonSaveLoadSystem.LoadListData<LevelData>();
    }

    public void CreateLevel()
    {
        level_name = input_name.text;
        level_description = input_description.text;
        level_height = (int)slider_height.value;
        level_width = (int)slider_width.value;

        if (level_name == "" || level_description == "")
        {
            InfoPanel("Невозможно создать уровень с пустым названием или описанием");
            return;
        }
        else if (CheckName(level_name))
        {
            InfoPanel("Уровень с данным названием уже существует");
            return;
        }

        //Debug.Log(level_name + " " + level_description + " " + level_height + " " + level_width);
        DataHolder.levelData = new LevelData(level_name, level_description, level_height, level_width);
        SceneManager.LoadScene(6);
    }

    private void InfoPanel(string text)
    {
        info_panel.SetActive(true);
        info_text.text = text;
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