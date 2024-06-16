using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private Transform target;
    private SpriteRenderer spriteRenderer;
    public List<GameObject> PatrolWaypoints = new List<GameObject>();
    private int patrolIndex = 0;
    private int patrolWaypointQuantity;
    public float regularSpeed = 400f;
    public float speed = 400f;
    public float chaseSpeed = 500f;
    public PatrolMode patrolMode;
    public float nextWaypointDistance = 3f;
    public float detectionRange;
    private bool facingRight = true;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public float slimeHealth;

    Seeker seeker;
    Rigidbody2D rb;

    public enum PatrolMode
    {
        Sequential,
        Random
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        patrolWaypointQuantity = PatrolWaypoints.Count;
        target = PatrolWaypoints[patrolIndex].transform;
        InvokeRepeating(nameof(UpdatePath), 0f, 1f);

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if(target  != null)
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            else
                seeker.StartPath(rb.position, PatrolWaypoints[patrolIndex].transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    private void Update()
    {
        if (slimeHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            target = player;
            nextWaypointDistance = 1f;
            //spriteRenderer.color = Color.red;
            speed = chaseSpeed;
        }
        else
        {
            if(target == player)
            {
                spriteRenderer.color = Color.white;
                speed = regularSpeed;
                target = null;
            }            
        }            

        if (path == null) { return; }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            if (target != player)
            {
                switch (patrolMode)
                {
                    case PatrolMode.Sequential:
                        patrolIndex++;
                        if (patrolIndex == patrolWaypointQuantity) { patrolIndex = 0; }
                        break;
                    case PatrolMode.Random:
                        patrolIndex = UnityEngine.Random.Range(0, patrolWaypointQuantity);
                        break;
                }
                target = PatrolWaypoints[patrolIndex].transform;
            }
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force * new Vector2(1, 0.01f));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x <= -0.01f && facingRight)
        {
            Flip();
        }

        void Flip()
        {
            Vector3 scale = transform.localScale;
            if (target == player)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;

                if (directionToPlayer.x > 0 && !facingRight)
                {
                    facingRight = true;
                    scale.x = Mathf.Abs(scale.x); // Ensure the sprite is facing right
                }
                else if (directionToPlayer.x < 0 && facingRight)
                {
                    facingRight = false;
                    scale.x = -Mathf.Abs(scale.x); // Ensure the sprite is facing left
                }
            }
            else
            {
                facingRight = !facingRight;
                scale.x *= -1;
            }
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Set the color of the gizmo
        Gizmos.color = Color.yellow;

        // Draw a wire sphere to represent the detection range
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("enemu health: " + slimeHealth);
            slimeHealth -= 1;
            Debug.Log("enemu health: " + slimeHealth);
        }
    }


}