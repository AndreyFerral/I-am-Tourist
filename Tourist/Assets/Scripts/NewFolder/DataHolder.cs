using System.Collections.Generic;

public static class DataHolder
{
    // Хранение выбранной локации
    // 0 - поле, 1 - лес, 2 - озеро, 3 - горы
    public static int IdLocation { get; set; }

    // Хранение выбранного времени года
    // 0 - весна, 1 - лето, 2 - осень, 3 - зима
    public static int IdSeason { get; set; }

    // Хранение идентификатора выбранного рюкзака
    // 0 - маленький, 1 - средний, 2 - большой
    public static int IdBackpack { get; set; }

    // Хранение вещей в рюкзаке
    public static List<string> Items { get; set; }

    // Была достигнута финальная точка маршрута
    public static bool IsAfterRoute { get; set; }

    // Было ли сообщено в начале похода
    public static bool IsNotifyStart { get; set; }

    // Было ли сообщено в конце похода
    public static bool IsNotifyEnd { get; set; }
}