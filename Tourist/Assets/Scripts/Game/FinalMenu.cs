using TMPro;
using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    [SerializeField] TrashCan scriptTC;
    [SerializeField] TMP_Text headerText;
    [SerializeField] TMP_Text mainText;

    private string[] lose = {
        "Вы проиграли", "Выносливость персонажа закончилась"
    };
    private string[] win = {
        "Маршрут пройден", "- Было сообщено о походе\n",
        "- В рюкзаке нет мусора", "- Мусор не был выброшен",
        "- Не весь мусор выброшен"
    };

    public void SetLoseMenu()
    {
        gameObject.SetActive(true);
        headerText.text = lose[0];
        mainText.alignment = TextAlignmentOptions.Center;
        mainText.text = lose[1];
    }

    public void SetWinMenu()
    {
        gameObject.SetActive(true);
        headerText.text = win[0];
        mainText.alignment = TextAlignmentOptions.Left;
        mainText.text = win[1];

        var eventItems = DataLoader.GetListEventsItemsData(tag);

        // Проверяем ячейки на наличие мусора
        if (scriptTC.CheckQuick(eventItems) || scriptTC.CheckBackpack(eventItems))
        {
            // Если была выброшена часть мусора
            if (scriptTC.IsTrashDrop) mainText.text += win[4];
            // Если мусор не был выброшен
            else mainText.text += win[3];
        }
        // Если мусор был выброшен
        else mainText.text += win[2];
    }
}