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

        // Movement �׼ǿ� ���� �ݹ� ���
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
            // ���� ��ȯ
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(newRotation);

            // ���� �̵�
            rb.velocity = moveDirection.normalized * moveSpeed;
        }
    }

    private void OnDestroy()
    {
        // �ݹ� ����
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
