using UnityEngine;

public class TabContentManager : MonoBehaviour
{
    // Массив объектов с контентом вкладок
    private GameObject[] tabContents;

    // Функция для переключения между вкладками
    public void SwitchTab(int index)
    {
        GetTabContents();

        // Скрываем все объекты с контентом
        for (int i = 0; i < tabContents.Length; i++)
        {
            tabContents[i].SetActive(false);
        }

        // Показывает объект с контентом, соответствующий выбранной вкладке
        // Условия для сцены "Конструктор меню" из-за кнопки "Назад"
        if (tabContents.Length > index) tabContents[index].SetActive(true);
    }

    // Новая функция для получения ID открытой вкладки
    public int GetActiveTabIndex()
    {
        GetTabContents();

        for (int i = 0; i < tabContents.Length; i++)
        {
            if (tabContents[i].activeInHierarchy)
            {
                return i; // Возвращаем индекс активной вкладки
            }
        }

        return -1; // Возвращаем -1, если ни одна вкладка не активна
    }

    // Функция для автоматического заполнения массива tabContents
    private void GetTabContents()
    {
        // Находим все дочерние объекты текущего объекта (группы контента)
        tabContents = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tabContents[i] = transform.GetChild(i).gameObject;
        }
    }
}
