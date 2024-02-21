using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject backpackPanel;
    private GameObject slots;
    private int idBackpack;

    // �������� �� ������� ����������� ���������
    private List<GameObject> backpackImages;
    private List<GameObject> backpackPanels;
    private List<Vector3> originalPositions;

    // ������ �� ������ - ����������� � ������ ��������
    public static bool IsOpen { get; set; } = false;

    void Start()
    {
        // 0 - Small, 1 - Medium, 2 - Large Backpack Panel
        idBackpack = DataHolder.IdBackpack;
        slots = backpackPanel.transform.GetChild(idBackpack).gameObject;
        // ������ ��������� Backpack Panel ��������
        slots.SetActive(true);

        // ������ gameobject ������� (���������) ������� � �����
        backpackImages = new List<GameObject>();
        // ������ gameobject ����� ������� (���������)
        backpackPanels = new List<GameObject>();
        // ������ ������������ ������� ����� �������
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
        // ���������� ��� ���������� ������ � ����� "SelectItems"
        if (backpackPanel.GetComponent<Image>() is null) return;

        // ��������/��������� ���������� �����������, ������� ��������� ����������� ����������
        backpackPanel.GetComponent<Image>().enabled = enable;

        // ��������/��������� ����������� ��������
        foreach (GameObject image in backpackImages)
        {
            image.GetComponent<Image>().enabled = enable;
        }

        // ���������� ������ � �������� ��������� ��� ������ (��� ���� ��������� ������)
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
        // �������� ��� ������ �������
        if (!backpackPanel.activeSelf)
        {
            backpackPanel.SetActive(true);
            ItemsController.RestoreInformation();
        }
        // ���� ������ ������
        if (!IsOpen) {
            EnableBackpack(true);
            IsOpen = true;
        }
        // ���� ������ ������
        else if (IsOpen) {
            EnableBackpack(false);
            IsOpen = false;
        }
    }
}