using DataNamespace;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] FinalMenu finalMenu;
    private float moveSpeed = 3;

    private Rigidbody2D myRigidBody;
    private Animator animator;
    private Vector2 moveDirection;
    private Transform handleJoystick;

    private float staminaMin = 0f;
    private float coroutineTime = 1f;
    private float staminaBackpack;
    private float staminaBrook;
    private float staminaRain;

    private bool isGame = true;

    private void Start()
    {
        // ������������� ������������ ��� �������
        EventsItemsData eventBrook = DataLoader.GetEventsItemsData("Brook");
        EventsItemsData eventRain = DataLoader.GetEventsItemsData("Rain");
        staminaBrook = eventBrook.ValueStamina;
        staminaRain = eventRain.ValueStamina;

        // ������������� ������������ ��� �������
        BackpackData backpack = DataLoader.GetBackpackData(DataHolder.IdBackpack);
        staminaBackpack = -backpack.Stamina;

        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        handleJoystick = joystick.transform.GetChild(0);

        // ��������� ��������� ��� ������������� ������������ 
        StartCoroutine(CoroutineStamina());
    }

    void Update() => ProcessInput(); 
    void FixedUpdate() => Move();

    private IEnumerator CoroutineStamina()
    {
        while (true)
        {
            // ���� ����� ����, �� �� �������� ������������
            bool isHome = HouseTransfer.IsHome;

            // ���� ����������� ������������
            if (StaminaBar.GetStamina() <= staminaMin && !isHome)
            {
                PrepareLoseMenu();
                yield break;
            }
            // ���� ����� �������������
            if (animator.GetBool("moving") && !isHome)
            {
                StaminaBar.ChangeStamina(staminaBackpack);
                Debug.Log("������. ������������ " + staminaBackpack);
            }
            // ���� ����� ��������� �� �����
            if (TrashCan.IsBrook && !isHome)
            {
                StaminaBar.ChangeStamina(staminaBrook);
                Debug.Log("�����. ������������ " + staminaBrook);
            }
            // ���� ����� ��� ������
            if (TrashCan.IsRain && !isHome)
            {
                StaminaBar.ChangeStamina(staminaRain);
                Debug.Log("�����. ������������ " + staminaRain);
            }

            // ������� ������������ �����
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    private void ProcessInput()
    {
        float moveX = isGame ? joystick.Horizontal : 0;
        float moveY = isGame ? joystick.Vertical : 0;

        // ������������ �������� ��� ��������� ���� ��������
        moveDirection = new Vector2(moveX, moveY).normalized;

        // �������� ���������
        ChangeAnimation(moveX, moveY);
    }

    private void Move()
    {
        // ������������ ����� ����������
        float newX = moveDirection.x * moveSpeed;
        float newY = moveDirection.y * moveSpeed;

        // ���������� ���������
        myRigidBody.velocity = new Vector2(newX, newY);
    }

    private void ChangeAnimation(float moveX, float moveY)
    {
        // ���� ���� �������������� � ����������
        if (moveDirection != Vector2.zero)
        {
            // ����������� ������
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveY", moveY);

            // ����� �������������
            animator.SetBool("moving", true);
        }
        // ����� ����� �� �����
        else animator.SetBool("moving", false);
    }

    private void PrepareLoseMenu()
    {
        // ������������� ��������
        handleJoystick.localPosition = Vector3.zero;
        joystick.enabled = false;
        isGame = false;

        // �������� ���� �������
        finalMenu.SetLoseMenu();
    }
}