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

    private static void CreateInteractPanelData()
    {
        List<InteractPanelData> data = new List<InteractPanelData>();
        data.Add(new InteractPanelData("TrashCan", "Мусорное ведро", "Выбросить мусор", "Нет мусора"));
        data.Add(new InteractPanelData("Notify", "Родственникам и МЧС", "Сообщить о начале похода", "Сообщить о конце похода"));
        data.Add(new InteractPanelData("ItemPick", "", "Подобрать", "Нет места"));
        data.Add(new InteractPanelData("Picnic", "Пикник", "Открыть", ""));
        data.Add(new InteractPanelData("Campfire", "Костёр", "Открыть", ""));
        data.Add(new InteractPanelData("Finish", "Маршрут пройден", "Домой", ""));
        JsonSaveLoadSystem.SaveListData(data);
    }

    private static void CreateDialogBoxData()
    {
        List<DialogBoxData> data = new List<DialogBoxData>();
        data.Add(new DialogBoxData("TrashCan", "В мусорное ведро можно выбросить фантики", ""));
        data.Add(new DialogBoxData("Notify", "", "Теперь я могу отправиться в поход!"));
        data.Add(new DialogBoxData("Teleport", "Сначала мне нужно сообщить о том, что я пошел в поход", "Мне нужно сообщить о том, что я вернулся из похода"));
        data.Add(new DialogBoxData("Finish", "", "Мне нужно сообщить о том, что я вернулся из похода"));
        data.Add(new DialogBoxData("Brook", "Мне нужно надеть сапоги или уйти с ручья", "На мне сапоги - я не намочу ноги"));
        data.Add(new DialogBoxData("Rain", "Мне нужно взять зонт или надеть плащ", "Теперь я не промокну"));
        JsonSaveLoadSystem.SaveListData(data);
    }

    private static void CreateItemData()
    {
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

    private static void CreateEventsItemsData()
    {
        List<EventsItemsData> data = new List<EventsItemsData>();
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "plastic-cup" }, new List<string> { "plastic-cup" }, new List<string> { "" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "thermos" }, new List<string> { "thermos" }, new List<string> { "" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "raspberry" }, new List<string> { "raspberry" }, new List<string> { "" }, 10));

        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "energy-bar" }, new List<string> { "energy-bar" }, new List<string> { "packaging" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "sandwich" }, new List<string> { "sandwich" }, new List<string> { "packaging" }, 10));
        data.Add(new EventsItemsData("Picnic", 0, new List<string> { "zip-bag" }, new List<string> { "zip-bag" }, new List<string> { "packaging" }, 10));

        data.Add(new EventsItemsData("TrashCan", 0, new List<string> { "packaging" }, new List<string> { "packaging" }, new List<string> { "" }, 0));
        data.Add(new EventsItemsData("Brook", 0, new List<string> { "boots" }, new List<string> { "" }, new List<string> { "" }, -2));
        data.Add(new EventsItemsData("Rain", 0, new List<string> { "raincoat" }, new List<string> { "" }, new List<string> { "" }, -2));
        data.Add(new EventsItemsData("Rain", 0, new List<string> { "umbrella" }, new List<string> { "" }, new List<string> { "" }, -2));

        data.Add(new EventsItemsData("Bonfire", 0, new List<string> { "branch", "lighter" }, new List<string> { "branch" }, new List<string> { "" }, 0));
        data.Add(new EventsItemsData("Bonfire", 0, new List<string> { "branch", "matches" }, new List<string> { "branch" }, new List<string> { "" }, 0));
        data.Add(new EventsItemsData("Bonfire", 1, new List<string> { "saucepan", "rice-bowl", "plastic-cup" }, new List<string> { "rice-bowl", "plastic-cup" }, new List<string> { "rice" }, 0));
        data.Add(new EventsItemsData("Bonfire", 1, new List<string> { "saucepan", "rice-bowl", "thermos" }, new List<string> { "rice-bowl", "thermos" }, new List<string> { "rice" }, 0));
        data.Add(new EventsItemsData("Bonfire", 2, new List<string> { "rice", "cutlery" }, new List<string> { "rice" }, new List<string> { "" }, 20));

        JsonSaveLoadSystem.SaveListData(data);
    }

    private static void CreateEventsInfoData()
    {
        List<EventsInfoData> data = new List<EventsInfoData>();
        data.Add(new EventsInfoData("Picnic", 0, "", "Перекусить","" ,""));

        data.Add(new EventsInfoData("Bonfire", 0, "", "Разжечь", "Разожжён", ""));
        data.Add(new EventsInfoData("Bonfire", 1, "", "Приготовить", "", ""));
        data.Add(new EventsInfoData("Bonfire", 2, "", "Съесть", "", ""));
        
        JsonSaveLoadSystem.SaveListData(data);
    }

    private static void CreateEventsData()
    {
        List<EventsData> data = new List<EventsData>();
        data.Add(new EventsData("Picnic", "Пикник", ""));
        data.Add(new EventsData("Bonfire", "Костёр", ""));

        JsonSaveLoadSystem.SaveListData(data);
    }

    // Метод для добавления данных в JSON
    public static void AddDataToJson()
    {
        CreateEventsItemsData();
        CreateEventsInfoData();
        CreateEventsData();
    }
}