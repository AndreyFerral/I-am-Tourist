using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] StaminaBar scriptSB;
    [SerializeField] GameObject finalMenu;
    [SerializeField] EventInfo brookEvent;
    [SerializeField] EventInfo rainEvent;
    [SerializeField] BackpackInfo[] backpacks;
    private float moveSpeed = 3;

    private Rigidbody2D myRigidBody;
    private Animator animator;
    private Vector2 moveDirection;
    private Transform handleJoystick;

    private float staminaMin = 0f;
    private float staminaBackpack;
    private float coroutineTime = 1f;
    private float staminaBrook;
    private float staminaRain;

    private bool isGame = true;

    private void Start()
    {
        // ������������� ������������ ��� �������
        staminaBrook = brookEvent.NegativeEffect;
        staminaRain = rainEvent.NegativeEffect;
        // ������������� ������������ ��� �������
        int idBackpack = DataHolder.IdBackpack;
        staminaBackpack = backpacks[idBackpack].Stamina;

        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        handleJoystick = joystick.transform.GetChild(0);

        // ��������� ��������� ��� ������������� ������������ 
        StartCoroutine(CoroutineStamina());
    }

    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    private IEnumerator CoroutineStamina()
    {
        while (true)
        {
            bool isHome = HouseTransfer.IsHome;

            // ���� ����������� ������������
            if (scriptSB.GetStamina() <= staminaMin)
            {
                PrepareLoseMenu();
                yield break;
            }
            // ���� ����� ������������� ��� ����
            if (animator.GetBool("moving") && !isHome)
            {
                scriptSB.MinusStamina(staminaBackpack);
                Debug.Log("������. �������� " + staminaBackpack);
            }
            // ���� ����� ��������� �� �����
            if (TrashCan.IsBrook && !isHome)
            {
                scriptSB.MinusStamina(staminaBrook);
                Debug.Log("�����. �������� " + staminaBrook);
            }
            // ���� ����� ��� ������
            if (TrashCan.IsRain && !isHome)
            {
                scriptSB.MinusStamina(staminaRain);
                Debug.Log("�����. �������� " + staminaRain);
            }

            // ������� ������������ �����
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    private void ProcessInput()
    {
        float moveX, moveY;

        if (isGame)
        {
            // �������� �������� �� ���������
            moveX = joystick.Horizontal;
            moveY = joystick.Vertical;
        }
        else
        {
            // ���� ���� ���������
            moveX = 0;
            moveY = 0;
        }

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
        Vector3 nullPos = new Vector3(0, 0, 0);
        handleJoystick.localPosition = nullPos;
        joystick.enabled = false;
        isGame = false;

        // �������� ���� �������
        FinalMenu scriptFM = finalMenu.GetComponent<FinalMenu>();
        scriptFM.SetLoseMenu();
    }
}