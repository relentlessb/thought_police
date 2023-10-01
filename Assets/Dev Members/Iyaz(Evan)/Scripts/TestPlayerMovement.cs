using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection = moveDirection.normalized;
    }

    private void Move()
    {
        rb2D.velocity = moveDirection * moveSpeed;
    }
}
