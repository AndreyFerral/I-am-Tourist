using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContextClue : MonoBehaviour
{
    [SerializeField] GameObject interactPanel;
    [SerializeField] Button backpackButton;
    [SerializeField] ParticleSystem rain;

    // Обращение к скрипту панели взаимодествия
    private InteractPanel interact;

    // Теги действий, событий и прочего
    private string signTag = "Sign";
    private string teleportTag = "Teleport";
    private string picnicTag = "Picnic";
    private string campfireTag = "Campfire";
    private string itemPickTag = "ItemPick";
    private string trachCanTag = "TrashCan";
    private string notifyTag = "Notify";
    private string finishTag = "Finish";
    private string brookTag = "Brook";
    private string rainTag = "Rain";

    private void Start()
    {
        // Выключаем дождь
        rain.Stop();
        // Определяем объект скрипта
        interact = interactPanel.GetComponent<InteractPanel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если этот объект знак
        if (other.CompareTag(signTag))
        {
            // Тестовые вопросы
        }
        // Если этот объект пикник
        else if (other.CompareTag(picnicTag))
        {
            // Настраиваем панель взаимодействия
            interact.SetEatPanels(other, true);
        }
        // Если этот объект костёр
        else if (other.CompareTag(campfireTag))
        {
            // Настраиваем панель взаимодействия
            interact.SetEatPanels(other, false);
        }
        // Если этот объект подбираемый
        else if (other.CompareTag(itemPickTag))
        {
            backpackButton.onClick.AddListener(delegate
            {
                interact.SetItemPick(other);
            });

            // Настраиваем панель взаимодействия
            interact.SetItemPick(other);
        }
        else if (other.CompareTag(trachCanTag))
        {
            backpackButton.onClick.AddListener(delegate
            {
                interact.SetTrashCan(other);
            });

            // Настраиваем панель взаимодействия
            interact.SetTrashCan(other);
        }
        else if (other.CompareTag(notifyTag))
        {
            // Настраиваем панель взаимодействия
            interact.SetNotify(other);
        }
        else if (other.CompareTag(finishTag))
        {
            // Настраиваем панель взаимодействия
            interact.SetFinish(other);
        }
        else if (other.CompareTag(brookTag))
        {
            TrashCan trashCan = other.GetComponent<TrashCan>();

            backpackButton.onClick.AddListener(delegate
            {
                trashCan.CheckBrook();
            });

            trashCan.StartBrook();
        }
        else if (other.CompareTag(rainTag))
        {
            StartCoroutine(SetRain());
            other.enabled = false;
            TrashCan trashCan = other.GetComponent<TrashCan>();

            backpackButton.onClick.AddListener(delegate
            {
                trashCan.CheckRain();
            });

            trashCan.StartRain();
        }
        // Если этот объект не телепорт и не знак
        if (!other.CompareTag(teleportTag) &&
            !other.CompareTag(signTag) &&
            !other.CompareTag(brookTag) &&
            !other.CompareTag(rainTag))
        {
            // Включаем панель взаимодействия
            interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Если этот объект не телепорт
        if (!other.CompareTag(teleportTag))
        {
            // Выключить панель еды
            interact.ShowEatPanel(false, true);
            interact.ShowEatPanel(false, false);

            // Выключить панель взаимодействия
            interactPanel.SetActive(false);

            // Убрать все подписанные события на кнопку
            backpackButton.onClick.RemoveAllListeners();
        }
        if (other.CompareTag(brookTag))
        {
            TrashCan.IsBrook = false;
        }
    }

    private IEnumerator SetRain()
    {
        rain.Play();

        // Ожидаем определенное количество времени
        const int awaitTime = 10;
        yield return new WaitForSeconds(awaitTime);

        // Останавливаем дождь
        TrashCan.IsRain = false;
        rain.Stop();
        // Убрать все подписанные события на кнопку
        backpackButton.onClick.RemoveAllListeners();
    }
}