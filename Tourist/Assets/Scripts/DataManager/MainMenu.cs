using DataNamespace;
using System.Collections.Generic;
using System.IO;
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

        // Создаем все необходимые JSON файлы
        JsonSaveLoadSystem.CreateJSON();

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

    // Метод для очистки JSON файлов
    public static void DeleteJsonFolder()
    {
        string jsonFolderPath = Path.Combine(Application.persistentDataPath, "JSON");

        if (Directory.Exists(jsonFolderPath))
        {
            Directory.Delete(jsonFolderPath, true);
            Debug.Log("Папка JSON со всем содержимым была удалена");
        }
    }

    // Метод для добавления данных в JSON
    public static void AddDataToJson()
    {
        /*
        List<InteractPanelData> interactPanelDatas = new List<InteractPanelData>();
        interactPanelDatas.Add(new InteractPanelData("TrashCan", "Мусорное ведро", "Выбросить мусор", "Нет мусора"));
        interactPanelDatas.Add(new InteractPanelData("Notify", "Родственникам и МЧС", "Сообщить о начале похода", "Сообщить о конце похода"));
        interactPanelDatas.Add(new InteractPanelData("ItemPick", "", "Подобрать", "Нет места"));
        interactPanelDatas.Add(new InteractPanelData("Picnic", "Пикник", "Открыть", ""));
        interactPanelDatas.Add(new InteractPanelData("Campfire", "Костёр", "Открыть", ""));
        interactPanelDatas.Add(new InteractPanelData("Finish", "Маршрут пройден", "Домой", ""));
        JsonSaveLoadSystem.SaveListData(interactPanelDatas);
        */

        /*
        List<DialogBoxData> data = new List<DialogBoxData>();
        data.Add(new DialogBoxData("TrashCan", "В мусорное ведро можно выбросить фантики", ""));
        data.Add(new DialogBoxData("Notify", "", "Теперь я могу отправиться в поход!"));
        data.Add(new DialogBoxData("Teleport", "Сначала мне нужно сообщить о том, что я пошел в поход", "Мне нужно сообщить о том, что я вернулся из похода"));
        data.Add(new DialogBoxData("Finish", "", "Мне нужно сообщить о том, что я вернулся из похода"));
        data.Add(new DialogBoxData("Brook", "Мне нужно надеть сапоги или уйти с ручья", "На мне сапоги - я не намочу ноги"));
        data.Add(new DialogBoxData("Rain", "Мне нужно взять зонт или надеть плащ", "Теперь я не промокну"));
        JsonSaveLoadSystem.SaveListData(data);
        */

        
        List<ItemData> data = new List<ItemData>();
        data.Add(new ItemData("Prefabs/", "boots", "Ботинки", 0.2f));
        data.Add(new ItemData("Prefabs/", "umbrella", "Зонт", 0.3f));
        data.Add(new ItemData("Prefabs/", "saucepan", "Кастрюля", 0.5f));
        data.Add(new ItemData("Prefabs/", "raincoat", "Плащ", 0.5f));

        data.Add(new ItemData("Prefabs/", "branch", "Ветка", 0.1f));
        data.Add(new ItemData("Prefabs/", "cutlery", "Приборы", 0.2f));
        data.Add(new ItemData("Prefabs/", "energy-bar", "Батончик", 0.1f));
        data.Add(new ItemData("Prefabs/", "lighter", "Зажигалка", 0.1f));
        data.Add(new ItemData("Prefabs/", "matches", "Спички", 0.1f));
        data.Add(new ItemData("Prefabs/", "packaging", "Мусор", 0.15f));
        data.Add(new ItemData("Prefabs/", "plastic-cup", "Вода", 0.2f));
        data.Add(new ItemData("Prefabs/", "raspberry", "Ягода", 0.1f));
        data.Add(new ItemData("Prefabs/", "rice-bowl", "РисСырой", 0.3f));
        data.Add(new ItemData("Prefabs/", "rice", "Рис", 0.25f));
        data.Add(new ItemData("Prefabs/", "sandwich", "Бутерброд", 0.4f));
        data.Add(new ItemData("Prefabs/", "thermos", "Термос", 0.6f));
        data.Add(new ItemData("Prefabs/", "zip-bag", "Печеньки", 0.1f));

        JsonSaveLoadSystem.SaveListData(data);
        
    }
}