using DataNamespace;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalMenu : MonoBehaviour
{
    [SerializeField] TrashCan trashCan;
    [SerializeField] TMP_Text headerText, mainText;
    private Button button;

    private string[] lose = {
        "Вы проиграли", "Выносливость персонажа закончилась"
    };
    private string[] win = {
        "Маршрут пройден", "- Было сообщено о походе\n",
        "- В рюкзаке нет мусора", "- Мусор не был выброшен",
        "- Не весь мусор выброшен"
    };

    private void PrepareFinalMenu(TextAlignmentOptions alignment, bool isWin)
    {
        string[] text = isWin ? win : lose;

        gameObject.SetActive(true);
        mainText.alignment = alignment;
        headerText.text = text[0];
        mainText.text = text[1];

        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(() => LevelComplete(isWin));
    }

    public void SetLoseMenu() => PrepareFinalMenu(TextAlignmentOptions.Center, false);

    public void SetWinMenu()
    {
        PrepareFinalMenu(TextAlignmentOptions.Left, true);

        var eventItems = DataLoader.GetListEventsItemsData("TrashCan");

        // Проверяем ячейки на наличие мусора
        if (trashCan.CheckQuick(eventItems) || trashCan.CheckBackpack(eventItems))
        {
            // Если была выброшена часть мусора
            if (trashCan.IsTrashDrop) mainText.text += win[4];
            // Если мусор не был выброшен
            else mainText.text += win[3];
        }
        // Если мусор был выброшен
        else mainText.text += win[2];
    }

    public void LevelComplete(bool isWin)
    {
        if (DataHolder.levelData != null && isWin)
        {
            List<LevelData> loadedLevelDataList = JsonSaveLoadSystem.LoadListData<LevelData>();
            LevelData levelDataToUpdate = loadedLevelDataList.FirstOrDefault(data => data.nameMap == DataHolder.levelData.nameMap);

            levelDataToUpdate.isPassed = true;
            JsonSaveLoadSystem.ReplaceListData(loadedLevelDataList);
            Debug.Log("JSON: Записано прохождение уровня");
        }

        // Выходим после финального меню при выигрыше или поражении
        MainMenu.Menu();    
    }
}