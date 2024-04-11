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
    private TrashCan trashCan;

    private int interactionCount = 0;

    // ���� ��������, ������� � �������
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
        // ��������� �����
        rain.Stop();
        // ���������� ������ �������
        interact = interactPanel.GetComponent<InteractPanel>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����������� ������� ������ � ����
        interactionCount++;

        // ���� ���� ������ ����
        if (other.CompareTag(signTag))
        {
            // �������� �������
        }
        // ���� ��� ������� 1 ��������
        else if (other.CompareTag(oneEventTag))
        {
            interact.SetEatPanels(other, 0);
        }
        // ���� ��� ������� 2 ���������
        else if (other.CompareTag(twoEventTag))
        {
            interact.SetEatPanels(other, 1);
        }
        // ���� ���� ������� 3 ���������
        else if (other.CompareTag(threeEventTag))
        {
            interact.SetEatPanels(other, 2);
        }
        // ���� ���� ������ �����������
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
            trashCan.StartBrook();
            backpackButton.onClick.AddListener(() => trashCan.CheckBrook());
        }
        else if (other.CompareTag(rainTag))
        {
            StartCoroutine(SetRain(other));
            if (trashCan == null) trashCan = other.GetComponent<TrashCan>();

            trashCan.StartRain();
            backpackButton.onClick.AddListener(() => trashCan.CheckRain());
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
        // ��������� ������� ������� �� ����
        interactionCount--;

        // ������ ��� ����������� ������� �� ������
        backpackButton.onClick.RemoveAllListeners();

        // ���� ���� ������ �� ��������
        if (!other.CompareTag(teleportTag) && interactionCount == 0)
        {
            // ��������� ������ ��������������
            interactPanel.SetActive(false);
        }
        if (other.CompareTag(brookTag))
        {
            TrashCan.IsBrook = false;
        }
    }

    private IEnumerator SetRain(Collider2D rainCollider)
    {
        // �������� ����� ��� ��� ���������
        Vector3 newPosition = rainCollider.transform.position;
        newPosition.y += 15;
        rain.transform.position = newPosition;

        rain.Play();
        rainCollider.enabled = false;

        // ������� ������������ ���������� �������
        yield return new WaitForSeconds(10f);

        // ������ ��� ����������� ������� �� ������
        backpackButton.onClick.RemoveAllListeners();

        // ������������� �����
        TrashCan.IsRain = false;
        rain.Stop();
    }
}