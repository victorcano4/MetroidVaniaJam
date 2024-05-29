using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0f;

    private Rigidbody2D rb;
    public CharacterController characterController;
    public Animator player_animator;

    private bool isGrounded;
    public bool isRunning;
    public bool isJumping;
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
        if (moveInput != 0)
        {
            Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            rb.velocity = moveVelocity;
            isRunning = true;

            //Animation
            player_animator.SetBool("isRunning", true);
        }

        else if (moveInput == 0)
        {
            isRunning = false;

            //Animation
            player_animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;

            //Animation
            player_animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isJumping = true;

            //Animation
            player_animator.SetBool("isJumping", true);

        }
    }
}
