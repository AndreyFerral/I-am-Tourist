using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Метод для перемещения в сцену меню
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // Метод для перемещения в сцену настроек маршрута
    public void CustomRoute()
    {
        // Устанавливаем первоначальные значения
        DataHolder.IdBackpack = 0;
        DataHolder.IdLocation = 0;
        DataHolder.IdSeason = 0;

        SceneManager.LoadScene(1);
    }

    // Метод для перемещения в сцену выбора и расположения вещей
    public void SelectItems()
    {
        DataHolder.Items = null;
        Inventory.IsOpen = false;

        SceneManager.LoadScene(2);
    }

    // Метод для перемещения в сцену игры
    public void PlayGame()
    {
        // Устанавливаем первоначальные значения
        Inventory.IsOpen = false;
        HouseTransfer.IsHome = true;

        DataHolder.IsAfterRoute = false;
        DataHolder.IsNotifyStart = false;
        DataHolder.IsNotifyEnd = false;

        SceneManager.LoadScene(3);
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        Application.Quit();
    }

    // Метод для перемещения в сцену уровней
    public void LevelGame()
    {
        SceneManager.LoadScene(4);
    }
}