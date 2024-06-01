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
    public bool isFacingRight = true;

    [SerializeField]
    private bool isGrounded;
    public bool isRunning;
    public bool isJumping;    
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = rb.GetComponent<SpriteRenderer>();
    }

    void Update()
    {  
        Move();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            float calculatedMoveSpeed = moveSpeed;
            if (isFacingRight && moveInput < 0 || !isFacingRight && moveInput >0)
                calculatedMoveSpeed = moveSpeed * 0.5f;
            Vector2 moveVelocity = new Vector2(moveInput * calculatedMoveSpeed, rb.velocity.y);
            rb.velocity = moveVelocity;
            isRunning = true;

            //Animation
            if(player_animator != null) 
                player_animator.SetBool("isRunning", true);
        }

        else if (moveInput == 0)
        {
            isRunning = false;

            //Animation
            if (player_animator != null)
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
            if (player_animator != null)
                player_animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && sr.bounds.min.y >= collision.gameObject.GetComponent<SpriteRenderer>().bounds.max.y)
        {
            isGrounded = false;
            isJumping = true;

            //Animation
            if (player_animator != null)
                player_animator.SetBool("isJumping", true);

        }
    }
}
