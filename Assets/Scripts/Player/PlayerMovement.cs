using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float slowDownFactor = 0.5f;
    private float jumpForce = 0f;

    public bool isGrounded;
    public bool isJumping;
    public bool isCrouching;
    public bool isTransforming;
    public bool isInfected;
    public bool isDead;
    [SerializeField] private bool canStand = true;
    public float animation_duration;
    private float moveSpeed_previous;
    public float track_duration;
    public GameObject boss;

    [SerializeField] private Animator player_animator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    private PlayerShooting player_shooting;
    private Rigidbody2D myRigidbody;
    private FlipOrientation flipOrientation;
    private BoxCollider2D myBoxCollider;
    private CapsuleCollider2D myCapsuleCollider;
    private PlayerRespawn player_respawn;
    private PlayerHealth player_health_component;
    private AudioSource player_audio;
    public AudioClip infected_audioTrack_start;
    public AudioClip infected_audioTrack_loop;
    public AudioClip audioTrack_start;
    public AudioClip audioTrack_loop;
    public AudioClip transforming_ripping_sfx;
    private Vector2 boxColliderSize;
    private float shrinkFactor = 0.85f;
    public GameObject tntPrefab;
    public Transform tntLocation;

    [SerializeField] private float currentMovementSpeed;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        flipOrientation = GetComponent<FlipOrientation>();
        myBoxCollider = GetComponentInChildren<BoxCollider2D>();
        myCapsuleCollider = GetComponentInChildren<CapsuleCollider2D>();
        player_respawn = GetComponent<PlayerRespawn>();
        player_shooting = GetComponent<PlayerShooting>();
        player_audio = GetComponent<AudioSource>();
        boxColliderSize = myBoxCollider.size;
        moveSpeed_previous = moveSpeed;
    }

    void Update()
    {
        Move();
        Crouching();
        Dead();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDead = true;
        }
    }

    private void Crouching()
    {
        if (Input.GetKey(KeyCode.S))
        {
            isCrouching = true;
            myCapsuleCollider.enabled = true;
            myBoxCollider.enabled = false;
            player_animator.SetBool("isCrawling", true);
        }
        else
        {
            if (canStand && !IsColliderAbove())
            {
                isCrouching = false;
                myBoxCollider.enabled = true;
                myCapsuleCollider.enabled = false;
                player_animator.SetBool("isCrawling", false);
            }
        }
    }

    private bool IsColliderAbove()
    {
        Vector2 rayStart = (Vector2)transform.position + Vector2.up * myCapsuleCollider.size.y / 2;
        Vector2 rayDirection = Vector2.up;
        float rayDistance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, rayDirection, rayDistance);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            return true;
        }

        return false;
    }


    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            currentMovementSpeed = moveSpeed;

            if ((moveInput > 0 && flipOrientation.PlayerFacingRight()) || (moveInput < 0 && !flipOrientation.PlayerFacingRight()) || isCrouching)
            {
                currentMovementSpeed *= slowDownFactor;
            }

            Vector2 moveVelocity = new Vector2(moveInput * currentMovementSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = moveVelocity;

            // Animation
            if (player_animator != null)
                player_animator.SetBool("isRunning", true);
        }
        else
            player_animator.SetBool("isRunning", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            if(mySpriteRenderer.bounds.min.y +0.5f >= collision.contacts[0].point.y)
            {
                player_shooting.recoilJumpNumber = player_shooting.maxRecoilJumpNumber;
                player_shooting.UIRecoilJumpsController.ResetRecoilJumps();
            }                

            // Animation
            if (player_animator != null)
                player_animator.SetBool("isGrounded", true);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDead = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Neccesary to fix animation jump
        // Remove the following 6 lines to fix jumping animation glitch when crouching
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isJumping = true;
            player_animator.SetBool("isGrounded", false);
        }

        if (collision.gameObject.CompareTag("Ground") && mySpriteRenderer.bounds.min.y >= collision.gameObject.GetComponent<TilemapRenderer>().bounds.max.y)
        {
            isGrounded = false;
            isJumping = true;

            // Animation
            player_animator.SetBool("isGrounded", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Upgrade Gliding")
        {
            isTransforming = true;

            //Change audio track to techno one
            player_audio.clip = infected_audioTrack_start;
            player_audio.Play();
            track_duration = 162f;
            StartCoroutine(PlayLoopableMusicTrack());

            //Stop movement while transforming animation is playing
            animation_duration = 3f;
            StartCoroutine(StopMovement(animation_duration));
        }

        if (collision.gameObject.CompareTag("Upgrade Recoil Jumping"))
        {
            isTransforming = true;

            //Stop movement while transforming animation is playing
            animation_duration = 3f;
            StartCoroutine(StopMovement(animation_duration));
            
        }

        //Check colision for the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            player_shooting.recoilJumpNumber = player_shooting.maxRecoilJumpNumber;
        }

        if (collision.gameObject.name == "TriggetToEnding")
        {
            //spawn tnt
            Instantiate(tntPrefab, tntLocation);
            StartCoroutine(TheEnd());
        }
    }

    private IEnumerator StopMovement(float animation_duration)
    {
        //Store previous moveSpeed value
        //moveSpeed_previous = moveSpeed;

        while (isTransforming)
        {
            moveSpeed = 0;
            isTransforming = false;

            //Play transforming animation
            player_animator.SetBool("isTransforming", true);

            //Play sfx transforming
            player_audio.PlayOneShot(transforming_ripping_sfx);

            //Wait for the animation to finish
            yield return new WaitForSeconds(animation_duration);

            //Restore speed, move to infected animation
            player_animator.SetBool("isTransforming", false);
            player_animator.SetBool("isInfected", true);
            isInfected = true;
            moveSpeed = moveSpeed_previous;

        }

        while (isDead)
        {
            moveSpeed = 0;
            Debug.Log("move speed is 0");
            isDead = false;
            player_animator.SetBool("isDead", true);

            yield return new WaitForSeconds(animation_duration);

            player_respawn.Respawn();
            player_animator.SetBool("isDead", false);
            moveSpeed = moveSpeed_previous;
            Debug.Log("move speed is restored");
        }

    }

    private void Dead()
    {
        if (isDead)
        {
            //Stop movement when dead
            animation_duration = 2f;
            StartCoroutine(StopMovement(animation_duration));
        }
    }


    IEnumerator PlayLoopableMusicTrack()
    {
        yield return new WaitForSeconds(track_duration);
        player_audio.clip = infected_audioTrack_loop;
        player_audio.Play();
    }

    // this is the worst way to do things, but we are late ...
     public void MoveBoss()
    {
        boss.transform.position += new Vector3(20,0,0);
    }

    private IEnumerator TheEnd()
    {
        //wait for explosion
        yield return new WaitForSeconds(4);
        // destroy boss object
        Destroy(boss);
        yield return new WaitForSeconds(1);
        // change scene
        SceneManager.LoadScene("FinalDialogue");
    }
}
