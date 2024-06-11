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
    public float chaseSpeed = 500f;
    public float nextWaypointDistance = 3f;
    public float detectionRange;
    private bool facingRight = true;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public AudioClip idleSound;       // Sound to play when patroling
    public AudioClip detectSound;     // Sound to play when the player is detected
    private AudioSource audioSource;
    public float detectionRangeSfx;    //detection range to play the sfx

    Seeker seeker;
    Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        patrolWaypointQuantity = PatrolWaypoints.Count;
        target = PatrolWaypoints[patrolIndex].transform;
        InvokeRepeating(nameof(UpdatePath), 0f, 1f);

        //Get audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from this GameObject.");
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
        //Play sfx when the player is at X distance
        
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            PlaySFX(detectSound);
        }
        else if (Vector3.Distance(transform.position, player.position) <= detectionRangeSfx)
        {
            PlaySFX(idleSound);
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            target = player;
            nextWaypointDistance = 1f;
            spriteRenderer.color = Color.red;
            regularSpeed = chaseSpeed;
        }

        if (path == null) { return; }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            if (target != player)
            {
                patrolIndex++;
                if (patrolIndex == patrolWaypointQuantity) { patrolIndex = 0; }
                target = PatrolWaypoints[patrolIndex].transform;
            }
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * regularSpeed * Time.deltaTime;

        rb.AddForce(force);

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
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1; 
            transform.localScale = scale;
        }
    }


    //Play sfx
    void PlaySFX(AudioClip sfx)
    {
        if (!audioSource.isPlaying) // Ensure the sound is not played repeatedly
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}
