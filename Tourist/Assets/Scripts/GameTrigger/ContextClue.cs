using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContextClue : MonoBehaviour
{
    [SerializeField] GameObject interactPanel;
    [SerializeField] Button backpackButton;

    // Для манипуляцией дождем
    private ParticleSystem rain;

    // Обращение к скрипту панели взаимодествия
    private InteractPanel interact;
    private TrashCan trashCan;

    private int interactionCount = 0;

    // Теги действий, событий и прочего
    private string signTag = "Sign";
    private string teleportTag = "Teleport";
    private string oneEventTag = "OneEvent";
    private string twoEventTag = "TwoEvent";
    private string threeEventTag = "ThreeEvent";
    private string itemPickTag = "ItemPick";
    private string trachCanTag = "TrashCan";
    private string notifyTag = "Notify";
    private string finishTag = "Finish";
    private string brookTag = "Brook";
    private string rainTag = "Rain";

    private void Start()
    {        
        // Выключаем дождь
        rain = gameObject.GetComponentInChildren<ParticleSystem>();
        rain.Stop();

        // Определяем объект скрипта
        interact = interactPanel.GetComponent<InteractPanel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Увеличиваем счетчик входов в зону
        interactionCount++;

        // Если этот объект знак
        if (other.CompareTag(signTag))
        {
            interact.SetQuestion(other);
        }
        // Если это событие 1 вкладкой
        else if (other.CompareTag(oneEventTag))
        {
            interact.SetEatPanels(other, 0);
        }
        // Если это событие 2 вкладками
        else if (other.CompareTag(twoEventTag))
        {
            interact.SetEatPanels(other, 1);
        }
        // Если этот событие 3 вкладками
        else if (other.CompareTag(threeEventTag))
        {
            interact.SetEatPanels(other, 2);
        }
        // Если этот объект подбираемый
        else if (other.CompareTag(itemPickTag))
        {
            interact.SetItemPick(other);
            backpackButton.onClick.AddListener(() => interact.SetItemPick(other));
        }
        else if (other.CompareTag(notifyTag))
        {
            interact.SetNotify(other);
        }
        else if (other.CompareTag(finishTag))
        {
            interact.SetFinish(other, rain);
        }
        else if (other.CompareTag(trachCanTag))
        {
            interact.SetTrashCan(other);
            backpackButton.onClick.AddListener(() => interact.SetTrashCan(other));
        }
        else if (other.CompareTag(brookTag))
        {
            if (trashCan == null) trashCan = other.GetComponent<TrashCan>();
            trashCan.Brook();
            backpackButton.onClick.AddListener(() => trashCan.Brook());
        }
        else if (other.CompareTag(rainTag) && !HouseTransfer.IsHome)
        {
            // !HouseTransfer.IsHome если часть триггера дождя находится дома
            StartCoroutine(SetRain(other));
            if (trashCan == null) trashCan = other.GetComponent<TrashCan>();

            trashCan.Rain();
            backpackButton.onClick.AddListener(() => trashCan.Rain());
        }
        // Если этот объект не телепорт и не знак
        if (!other.CompareTag(teleportTag) &&
            !other.CompareTag(brookTag) &&
            !other.CompareTag(rainTag))
        {
            // Включаем панель взаимодействия
            interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Уменьшаем счетчик выходов из зоны
        interactionCount--;

        // Убрать все подписанные события на кнопку
        backpackButton.onClick.RemoveAllListeners();

        // Если этот объект не телепорт
        if (!other.CompareTag(teleportTag) && interactionCount == 0)
        {
            // Выключить панель взаимодействия
            interactPanel.SetActive(false);
        }
        if (other.CompareTag(brookTag))
        {
            TrashCan.IsBrook = false;
        }

    }

    private IEnumerator SetRain(Collider2D rainTrigger)
    {
        // Запускаем дождь
        rain.Play();
        rainTrigger.enabled = false;

        // Ожидаем определенное количество времени
        yield return new WaitForSeconds(10f);

        // Убрать все подписанные события на кнопку
        backpackButton.onClick.RemoveAllListeners();

        // Останавливаем дождь
        TrashCan.IsRain = false;
        rain.Stop();
    }
}