using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player movement")]
    public float playerSpeed = 1.9f;

    [Header("Player camera")]
    public Transform playerCamera;

    [Header("Player animator & gravity")]
    public CharacterController characterCtrl;
    public float gravity = -9.81f;

    [Header("Player jumping & velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;

    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
            velocity.y = -2f;

        // gravity
        velocity.y += gravity * Time.deltaTime;
        characterCtrl.Move (velocity * Time.deltaTime);

        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterCtrl.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
            Debug.Log("viet 1234");
        }
    }
}
