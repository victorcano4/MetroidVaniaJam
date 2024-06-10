using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float slowDownFactor = 0.5f;
    private float jumpForce = 0f;

    public bool isGrounded;
    public bool isJumping;
    private bool isCrouching;

    [SerializeField] private Animator player_animator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidbody;
    private FlipOrientation flipOrientation;
    private BoxCollider2D myBoxCollider;
    private CapsuleCollider2D myCapsuleCollider;
    private Vector2 boxColliderSize;
    private float shrinkFactor = 0.85f;
    private float boxColliderShrinkWhileCrouching;
    private bool canStand = true;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        flipOrientation = GetComponent<FlipOrientation>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        boxColliderSize = myBoxCollider.size;
        boxColliderShrinkWhileCrouching = boxColliderSize.y * shrinkFactor;
    }

    void Update()
    {
        Move();
        Crouching();
        HandleColliderSize();
    }

    private void Crouching()
    {
        if (Input.GetKey(KeyCode.S))
        {
            //isCrouching = true;
            //myCapsuleCollider.enabled = true;
            player_animator.SetBool("isCrawling", true);
        }
        else
        {
            player_animator.SetBool("isCrawling", false);
            if (canStand)
            {
                isCrouching = false;
                //myCapsuleCollider.enabled = false;
            }
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            float calculatedMoveSpeed = moveSpeed;

            if ((moveInput > 0 && flipOrientation.PlayerFacingRight()) || (moveInput < 0 && !flipOrientation.PlayerFacingRight()) || isCrouching)
            {
                calculatedMoveSpeed *= slowDownFactor;
            }

            Vector2 moveVelocity = new Vector2(moveInput * calculatedMoveSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = moveVelocity;

            // Animation
            if (player_animator != null)
                player_animator.SetBool("isRunning", true);
        }

    }

    private void HandleColliderSize()
    {
        if (isCrouching)
        {
            // Shrink the collider
            myBoxCollider.size = new Vector2(boxColliderSize.x, Mathf.Lerp(myBoxCollider.size.y, boxColliderShrinkWhileCrouching, Time.deltaTime * 10f));
        }
        else
        {
            // Reset to original size
            myBoxCollider.size = new Vector2(boxColliderSize.x, Mathf.Lerp(myBoxCollider.size.y, boxColliderSize.y, Time.deltaTime * 10f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;

            // Animation
            if (player_animator != null)
                player_animator.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Line of code eliminated: && mySpriteRenderer.bounds.min.y >= collision.gameObject.GetComponent<TilemapRenderer>().bounds.max.y
            isGrounded = false;
            isJumping = true;

            // Animation
            if (player_animator != null)
                player_animator.SetBool("isGrounded", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canStand = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canStand = true;
        }
    }
}
