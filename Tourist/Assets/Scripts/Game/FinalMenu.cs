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

    private string[] lose = {
        "�� ���������", "������������ ��������� �����������"
    };
    private string[] win = {
        "������� �������", "- ���� �������� � ������\n",
        "- � ������� ��� ������", "- ����� �� ��� ��������",
        "- �� ���� ����� ��������"
    };

    private void PrepareFinalMenu(TextAlignmentOptions alignment, string[] text)
    {
        gameObject.SetActive(true);
        mainText.alignment = alignment;
        headerText.text = text[0];
        mainText.text = text[1];
    }

    public void SetLoseMenu() => PrepareFinalMenu(TextAlignmentOptions.Center, lose);

    public void SetWinMenu()
    {
        PrepareFinalMenu(TextAlignmentOptions.Left, win);

        var eventItems = DataLoader.GetListEventsItemsData("TrashCan");

        // ��������� ������ �� ������� ������
        if (trashCan.CheckQuick(eventItems) || trashCan.CheckBackpack(eventItems))
        {
            // ���� ���� ��������� ����� ������
            if (trashCan.IsTrashDrop) mainText.text += win[4];
            // ���� ����� �� ��� ��������
            else mainText.text += win[3];
        }
        // ���� ����� ��� ��������
        else mainText.text += win[2];
    }

    public void LevelComplete()
    {
        if (DataHolder.levelData != null)
        {
            List<LevelData> loadedLevelDataList = JsonSaveLoadSystem.LoadListData<LevelData>();
            LevelData levelDataToUpdate = loadedLevelDataList.FirstOrDefault(data => data.nameMap == DataHolder.levelData.nameMap);

            levelDataToUpdate.isPassed = true;
            JsonSaveLoadSystem.ReplaceListData(loadedLevelDataList);
            Debug.Log("JSON: �������� ����������� ������");
        }

        MainMenu.Menu();    
    }
}