using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ����� ��� ����������� � ����� ����
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // ����� ��� ����������� � ����� �������� ��������
    public void CustomRoute()
    {
        // ������������� �������������� ��������
        DataHolder.IdBackpack = 0;
        DataHolder.IdLocation = 0;
        DataHolder.IdSeason = 0;

        SceneManager.LoadScene(1);
    }

    // ����� ��� ����������� � ����� ������ � ������������ �����
    public void SelectItems()
    {
        DataHolder.Items = null;
        Inventory.IsOpen = false;

        SceneManager.LoadScene(2);
    }

    // ����� ��� ����������� � ����� ����
    public void PlayGame()
    {
        // ������������� �������������� ��������
        Inventory.IsOpen = false;
        HouseTransfer.IsHome = true;

        DataHolder.IsAfterRoute = false;
        DataHolder.IsNotifyStart = false;
        DataHolder.IsNotifyEnd = false;

        SceneManager.LoadScene(3);
    }

    // ����� ��� ������ �� ����
    public void QuitGame()
    {
        Application.Quit();
    }

    // ����� ��� ����������� � ����� �������
    public void LevelGame()
    {
        SceneManager.LoadScene(4);
    }
}