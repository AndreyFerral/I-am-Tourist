using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject backpackPanel;
    private GameObject slots;
    private int idBackpack;

    // Отвечает за видимое отображение инвентаря
    private List<GameObject> backpackImages;
    private List<GameObject> backpackPanels;
    private List<Vector3> originalPositions;

    // Открыт ли рюкзак - Проверяется в других скриптах
    public static bool IsOpen { get; set; } = false;

    void Start()
    {
        // 0 - Small, 1 - Medium, 2 - Large Backpack Panel
        idBackpack = DataHolder.IdBackpack;
        slots = backpackPanel.transform.GetChild(idBackpack).gameObject;
        // Делаем выбранной Backpack Panel активным
        slots.SetActive(true);

        // список gameobject рюкзака (инвентаря) спереди и сзади
        backpackImages = new List<GameObject>();
        // список gameobject ячеек рюкзака (инвентаря)
        backpackPanels = new List<GameObject>();
        // список оригинальных позиций ячеек рюкзака
        originalPositions = new List<Vector3>();

        for (int i = 0; i < slots.transform.childCount; i++)
        {
            GameObject child = slots.transform.GetChild(i).gameObject;
            backpackImages.Add(child);

            for (int j = 0; j < child.transform.childCount; j++)
            {
                GameObject tempChild = child.transform.GetChild(j).gameObject;
                backpackPanels.Add(tempChild);
                originalPositions.Add(tempChild.GetComponent<RectTransform>().anchoredPosition3D);
            }
        }
    }

    private void EnableBackpack(bool enable)
    {
        // Необходимо для правильной работы в сцене "SelectItems"
        if (backpackPanel.GetComponent<Image>() is null) return;

        // Включаем/Отключаем прозрачное изображение, которое блокирует управлением персонажем
        backpackPanel.GetComponent<Image>().enabled = enable;

        // Включаем/Отключаем изображение рюкзаков
        foreach (GameObject image in backpackImages)
        {
            image.GetComponent<Image>().enabled = enable;
        }

        // Перемещаем панели в исходное положение или наверх (вне зоны видимости игрока)
        for (int i = 0; i < backpackPanels.Count; i++)
        {
            RectTransform rectTransform = backpackPanels[i].GetComponent<RectTransform>();
            if (enable)
            {
                rectTransform.anchoredPosition3D = originalPositions[i];
            }
            else
            {
                Vector3 movedPosition = originalPositions[i] + Vector3.up * 500f;
                rectTransform.anchoredPosition3D = movedPosition;
            }
        }
    }

    public void Backpack()
    {
        // Выполяем при первом запуске
        if (!backpackPanel.activeSelf)
        {
            backpackPanel.SetActive(true);
            ItemsController.RestoreInformation();
        }
        // Если рюкзак закрыт
        if (!IsOpen) {
            EnableBackpack(true);
            IsOpen = true;
        }
        // Если рюкзак открыт
        else if (IsOpen) {
            EnableBackpack(false);
            IsOpen = false;
        }
    }
}