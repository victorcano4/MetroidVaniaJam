using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AimAndShoot();
        FlipBasedOnMousePosition();
    }

    void AimAndShoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(direction);
        }
    }

    void Shoot(Vector2 direction)
    {
        // Calculate the angle of the projectile
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0,0,angle));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Debug.Log(direction);
        rb.velocity = (direction * projectileSpeed).normalized * projectileSpeed;
    }


    void FlipBasedOnMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (mousePosition.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
