using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJoystick : MonoBehaviour
{
    // —корость передвижени€ камеры
    [SerializeField] float movingSpeed;
    [SerializeField] Joystick joystick;

    void Update()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        // Ќормализаци€ значени€ при получении двух значений
        Vector3 moveDirection = new Vector2(moveX, moveY).normalized;

        transform.Translate(moveDirection * movingSpeed * Time.deltaTime);
    }
}
