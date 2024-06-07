using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float slowDownFactor = 0.5f;
    private float jumpForce = 0f;

    private Rigidbody2D myRigidbody;
    public bool IsGrounded;
    public bool isRunning;
    public bool isJumping;

    [SerializeField] private Animator player_animator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    private FlipOrientation flipOrientation;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        flipOrientation = GetComponent<FlipOrientation>();
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

            if ((moveInput > 0 && flipOrientation.PlayerFacingRight() ) || (moveInput < 0 && !flipOrientation.PlayerFacingRight()))
            {
                calculatedMoveSpeed *= slowDownFactor; // Slow down the player
            }

            Vector2 moveVelocity = new Vector2(moveInput * calculatedMoveSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = moveVelocity;
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
            IsGrounded = true;
            isJumping = false;

            //Animation
            if (player_animator != null)
                player_animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Changed SpriteRenderer into TilemapRenderer in the if check. Maps are built with tilemaps.
        if (collision.gameObject.CompareTag("Ground") && mySpriteRenderer.bounds.min.y >= collision.gameObject.GetComponent<TilemapRenderer>().bounds.max.y)
        {
            IsGrounded = false;
            isJumping = true;

            //Animation
            if (player_animator != null)
                player_animator.SetBool("isJumping", true);

        }
    }
}
