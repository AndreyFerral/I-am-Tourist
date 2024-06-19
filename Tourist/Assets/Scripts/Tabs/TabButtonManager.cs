using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabButtonManager : MonoBehaviour
{
    public TabContentManager tabContentManager;
    private List<Button> tabButtons = new List<Button>();

    private Button selectedButton;

    void Start()
    {
        GetTabButtons();

        foreach (Button button in tabButtons)
        {
            int index = tabButtons.IndexOf(button);
            button.onClick.AddListener(() => OnTabButtonClick(button, index));
            SetButtonAlpha(button, 0.2f);

            // Добавляем компонент TabButton для обработки наведения и ухода курсора
            TabButton tabButton = button.gameObject.AddComponent<TabButton>();
            tabButton.Initialize(this, button);
        }

        // Выделяем кнопку, соответствующую активной вкладке
        SelectActiveTabButton();
    }

    private void GetTabButtons()
    {
        foreach (Transform child in transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null) tabButtons.Add(button);      
        }
    }

    private void SelectActiveTabButton()
    {
        int activeTabIndex = tabContentManager.GetActiveTabIndex();
        if (activeTabIndex != -1)
        {
            selectedButton = tabButtons[activeTabIndex];
            SetButtonAlpha(selectedButton, 0.6f);
        }
    }

    private void OnTabButtonClick(Button button, int index)
    {
        if (selectedButton != null)
        {
            selectedButton.OnDeselect(null);
            SetButtonAlpha(selectedButton, 0.2f);
        }

        selectedButton = button;
        tabContentManager.SwitchTab(index);
        SetButtonAlpha(button, 0.6f);
    }

    public void SetButtonAlpha(Button button, float alpha)
    {
        Color color = button.GetComponent<Image>().color;
        color.a = alpha;
        button.GetComponent<Image>().color = color;
    }

    public Button GetSelectedButton() => selectedButton;
}
