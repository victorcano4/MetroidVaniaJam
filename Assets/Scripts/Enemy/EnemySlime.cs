using System.Collections;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidBody;
    [SerializeField] private CircleCollider2D wallDetection;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float detectionRange;
    [SerializeField] private bool movingRight = true;
    private bool isChasing = false;
    private int monsterDamage = 1;
    private int monsterHealth = 2;

    private AudioSource audioSource;
    public AudioClip alertSound;       // Sound to play when the player is detected

    private Coroutine damageCoroutine; // To keep track of the damage coroutine

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        wallDetection = GetComponent<CircleCollider2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Get audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from this GameObject.");
        }
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


        //Play sfx if the player is at X distance
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            PlayAlertSound();
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

    void PlayAlertSound()
    {
        if (!audioSource.isPlaying) // Ensure the sound is not played repeatedly
        {
            audioSource.PlayOneShot(alertSound);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Checkpoint") && !isChasing)
        {
            movingRight = !movingRight;
            FlipSprite();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(collision.gameObject));
            }
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("monster health: " + monsterHealth);
            monsterHealth--;
            if (monsterHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(GameObject player)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (player.CompareTag("Player"))
            {
                PlayerHealth.instance.TakeDamage(monsterDamage);
            }
        }
    }
}
