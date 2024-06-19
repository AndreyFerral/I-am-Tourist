using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TabButtonManager tabButtonManager;
    private Button button;

    public void Initialize(TabButtonManager manager, Button btn)
    {
        tabButtonManager = manager;
        button = btn;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button != tabButtonManager.GetSelectedButton())
        {
            tabButtonManager.SetButtonAlpha(button, 0.4f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && button != tabButtonManager.GetSelectedButton())
        {
            tabButtonManager.SetButtonAlpha(button, 0.2f);
        }
    }
}
