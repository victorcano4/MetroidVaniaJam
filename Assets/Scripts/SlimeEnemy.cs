using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidBody;
    private PhysicsMaterial2D slipperyMaterial;
    [SerializeField] private CircleCollider2D wallDetection;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRange;
    [SerializeField] private bool movingRight = true;
    private bool isChasing = false;
    private int monsterDamage = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        wallDetection = GetComponent<CircleCollider2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Create and assign a slippery physics material to prevent sticking
        //slipperyMaterial = new PhysicsMaterial2D { friction = 0, bounciness = 0 };
        //myRigidBody.sharedMaterial = slipperyMaterial;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            MoveTowardsPlayer();
        }
        else
        {
            isChasing = false;
            Patrol();
        }

        if (isChasing)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-speed, myRigidBody.velocity.y);
        }
    }

    void MoveTowardsPlayer()
    {
        if (player.position.x > transform.position.x)
        {
            myRigidBody.velocity = new Vector2(speed * 2, myRigidBody.velocity.y);
            if (!movingRight)
                FlipSprite();
            movingRight = true;
        }
        else if (player.position.x < transform.position.x)
        {
            myRigidBody.velocity = new Vector2(-speed * 2, myRigidBody.velocity.y);
            if (movingRight)
                FlipSprite();
            movingRight = false;
        }
    }

    void FlipSprite()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Checkpoint") && !isChasing)
        {
            movingRight = !movingRight;
            FlipSprite();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(monsterDamage);
        }
    }
}
