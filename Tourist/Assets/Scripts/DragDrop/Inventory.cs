using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject backpackPanel;
    [SerializeField] GameObject smallPanel;
    [SerializeField] GameObject mediumPanel;
    [SerializeField] GameObject largePanel;
    private int idBackpack;

    public static bool IsOpen { get; set; }

    private void Start()
    {
        IsOpen = false;
        idBackpack = DataHolder.IdBackpack;
    }

    public void ShowClosePanel()
    {
        if (backpackPanel.activeSelf)
        {
            IsOpen = false;
            backpackPanel.SetActive(false);
        }
        else ShowPanel();
    }

    public void ShowPanel()
    {
        backpackPanel.SetActive(true);
        IsOpen = true;

        if (idBackpack == 0) smallPanel.SetActive(true);
        else if (idBackpack == 1) mediumPanel.SetActive(true);
        else if (idBackpack == 2) largePanel.SetActive(true);
    }
}