using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_movement : MonoBehaviour
{
    // Public variables to set the movement points, speed, and detection range
    public float speed;
    public float detectionRange;

    // Reference to the player's transform
    public Transform player;

    // Private variables to track the direction and initial position
    public bool movingRight = true;
    private bool isChasing = false;
    private bool sprite_flipped = false;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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


        // Change the color based on chasing state
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
        // Move the enemy back and forth
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }


    void MoveTowardsPlayer()
    {
        // Move towards the player's position
        if (player.position.x > transform.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            movingRight = true;
            FlipSprite();
        }
        else if (player.position.x < transform.position.x)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            movingRight = false;
            FlipSprite();
        }
    }


    void FlipSprite()
    {

        // Flip the sprite horizontally
        if (sprite_flipped == false)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            sprite_flipped = true;
        }
            
     
    }


}
