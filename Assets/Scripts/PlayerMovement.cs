using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0f;

    private Rigidbody2D rb;
    private bool isGrounded;
    bool facingRight = true;
    SpriteRenderer rbSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = rb.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isGrounded)
            Move();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
