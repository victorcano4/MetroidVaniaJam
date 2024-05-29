using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float knockbackForce = 5f;
    public float recoilJumpCooldown = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private bool isRecoilJumpInCooldown = false;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = (direction * projectileSpeed).normalized * projectileSpeed;

        if(!isRecoilJumpInCooldown)
        {
            isRecoilJumpInCooldown = true;
            StartCoroutine(ResetRecoilCooldown());
            ApplyKnockback(direction);
        }       
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

    void ApplyKnockback(Vector2 direction)
    {        
        Vector2 knockbackDirection = -direction; // Knockback is in the opposite direction of the shot
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Force);
    }

    IEnumerator ResetRecoilCooldown()
    {
        yield return new WaitForSeconds(recoilJumpCooldown);
        isRecoilJumpInCooldown = false;
    }
}
