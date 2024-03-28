using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakePlayerMovement : MonoBehaviour
{

    private Vector3 moveDirection;
    private float moveSpeed = 8f;
    private PlayerInput playerInput;
    private Rigidbody rb;
    private Transform snakeHead;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // Movement 액션에 대한 콜백 등록
        var movementAction = playerInput.actions[Define.ObjectName.movement];
        movementAction.performed += OnMovementStarted;
    }

    private void Start()
    {
        snakeHead = GetComponent<SnakeController>().GetSnakehead();
        rb = snakeHead.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            // 방향 전환
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(newRotation);

            // 전진 이동
            rb.velocity = moveDirection.normalized * moveSpeed;
        }
    }

    private void OnDestroy()
    {
        // 콜백 해제
        if (playerInput)
        {
            var movementAction = playerInput.actions["Movement"];
            movementAction.performed -= OnMovementStarted;
        }
    }



    #region Subscribe
    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0f, input.y);
    }

    #endregion

}
