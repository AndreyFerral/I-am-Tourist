using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContextClue : MonoBehaviour
{
    [SerializeField] GameObject interactPanel;
    [SerializeField] Button backpackButton;
    [SerializeField] ParticleSystem rain;

    // ��������� � ������� ������ �������������
    private InteractPanel interact;

    // ���� ��������, ������� � �������
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
        // ��������� �����
        rain.Stop();
        // ���������� ������ �������
        interact = interactPanel.GetComponent<InteractPanel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ���� ������ ����
        if (other.CompareTag(signTag))
        {
            // �������� �������
        }
        // ���� ���� ������ ������
        else if (other.CompareTag(picnicTag))
        {
            // ����������� ������ ��������������
            interact.SetEatPanels(other, true);
        }
        // ���� ���� ������ �����
        else if (other.CompareTag(campfireTag))
        {
            // ����������� ������ ��������������
            interact.SetEatPanels(other, false);
        }
        // ���� ���� ������ �����������
        else if (other.CompareTag(itemPickTag))
        {
            backpackButton.onClick.AddListener(delegate
            {
                interact.SetItemPick(other);
            });

            // ����������� ������ ��������������
            interact.SetItemPick(other);
        }
        else if (other.CompareTag(trachCanTag))
        {
            backpackButton.onClick.AddListener(delegate
            {
                interact.SetTrashCan(other);
            });

            // ����������� ������ ��������������
            interact.SetTrashCan(other);
        }
        else if (other.CompareTag(notifyTag))
        {
            // ����������� ������ ��������������
            interact.SetNotify(other);
        }
        else if (other.CompareTag(finishTag))
        {
            // ����������� ������ ��������������
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
        // ���� ���� ������ �� �������� � �� ����
        if (!other.CompareTag(teleportTag) &&
            !other.CompareTag(signTag) &&
            !other.CompareTag(brookTag) &&
            !other.CompareTag(rainTag))
        {
            // �������� ������ ��������������
            interactPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���� ���� ������ �� ��������
        if (!other.CompareTag(teleportTag))
        {
            // ��������� ������ ���
            interact.ShowEatPanel(false, true);
            interact.ShowEatPanel(false, false);

            // ��������� ������ ��������������
            interactPanel.SetActive(false);

            // ������ ��� ����������� ������� �� ������
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

        // ������� ������������ ���������� �������
        const int awaitTime = 10;
        yield return new WaitForSeconds(awaitTime);

        // ������������� �����
        TrashCan.IsRain = false;
        rain.Stop();
        // ������ ��� ����������� ������� �� ������
        backpackButton.onClick.RemoveAllListeners();
    }
}