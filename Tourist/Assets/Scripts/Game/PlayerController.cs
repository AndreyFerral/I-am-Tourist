using DataNamespace;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] GameObject finalMenu;
    [SerializeField] EventInfo brookEvent;
    [SerializeField] EventInfo rainEvent;
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
        // Устанавливаем выносливость для событий
        staminaBrook = brookEvent.NegativeEffect;
        staminaRain = rainEvent.NegativeEffect;

        // Устанавливаем выносливость для рюкзака
        BackpackData item = DataLoader.GetBackpackData(DataHolder.IdBackpack);
        staminaBackpack = item.Stamina;

        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        handleJoystick = joystick.transform.GetChild(0);

        // Запускаем короутины для регулирования выносливости 
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
            // Если игрок дома, то не отнимаем выносливость
            bool isHome = HouseTransfer.IsHome;

            // Если закончилась выносливость
            if (StaminaBar.GetStamina() <= staminaMin && !isHome)
            {
                PrepareLoseMenu();
                yield break;
            }
            // Если игрок передвигается
            if (animator.GetBool("moving") && !isHome)
            {
                StaminaBar.MinusStamina(staminaBackpack);
                Debug.Log("Рюкзак. Отнимаем " + staminaBackpack);
            }
            // Если игрок находится на ручье
            if (TrashCan.IsBrook && !isHome)
            {
                StaminaBar.MinusStamina(staminaBrook);
                Debug.Log("Ручей. Отнимаем " + staminaBrook);
            }
            // Если игрок под дождем
            if (TrashCan.IsRain && !isHome)
            {
                StaminaBar.MinusStamina(staminaRain);
                Debug.Log("Дождь. Отнимаем " + staminaRain);
            }

            // Ожидаем определенное время
            yield return new WaitForSeconds(coroutineTime);
        }
    }

    private void ProcessInput()
    {
        float moveX, moveY;

        if (isGame)
        {
            // Получаем значения из джойстика
            moveX = joystick.Horizontal;
            moveY = joystick.Vertical;
        }
        else
        {
            // Если игра завершена
            moveX = 0;
            moveY = 0;
        }

        // Нормализация значения при получении двух значений
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Анимация персонажа
        ChangeAnimation(moveX, moveY);
    }

    private void Move()
    {
        // Обрабатываем новые координаты
        float newX = moveDirection.x * moveSpeed;
        float newY = moveDirection.y * moveSpeed;

        // Перемещаем персонажа
        myRigidBody.velocity = new Vector2(newX, newY);
    }

    private void ChangeAnimation(float moveX, float moveY)
    {
        // Если есть взаимодействие с джойстиком
        if (moveDirection != Vector2.zero)
        {
            // Направление ходьбы
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveY", moveY);

            // Игрок передвигается
            animator.SetBool("moving", true);
        }
        // Игрок стоит на месте
        else animator.SetBool("moving", false);
    }

    private void PrepareLoseMenu()
    {
        // Останавливаем джойстик
        Vector3 nullPos = new Vector3(0, 0, 0);
        handleJoystick.localPosition = nullPos;
        joystick.enabled = false;
        isGame = false;

        // Вызываем окно провала
        FinalMenu scriptFM = finalMenu.GetComponent<FinalMenu>();
        scriptFM.SetLoseMenu();
    }
}