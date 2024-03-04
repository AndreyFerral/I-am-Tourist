using TMPro;
using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    [SerializeField] TrashCan scriptTC;
    [SerializeField] TMP_Text headerText;
    [SerializeField] TMP_Text mainText;

    private string[] lose = {
        "�� ���������", "������������ ��������� �����������"
    };
    private string[] win = {
        "������� �������", "- ���� �������� � ������\n",
        "- � ������� ��� ������", "- ����� �� ��� ��������",
        "- �� ���� ����� ��������"
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

        // ��������� ������ �� ������� ������
        if (scriptTC.CheckQuick(eventItems) || scriptTC.CheckBackpack(eventItems))
        {
            // ���� ���� ��������� ����� ������
            if (scriptTC.IsTrashDrop) mainText.text += win[4];
            // ���� ����� �� ��� ��������
            else mainText.text += win[3];
        }
        // ���� ����� ��� ��������
        else mainText.text += win[2];
    }
}