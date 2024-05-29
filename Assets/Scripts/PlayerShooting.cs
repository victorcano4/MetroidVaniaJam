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
    public float maxBulletNumber = 3f;
    public float bulletNumber;
    public float rechargeTime;
    

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private bool isRecoilJumpInCooldown = false;
    public bool isRechargingGun = false;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bulletNumber = maxBulletNumber;
    }

    void Update()
    {
        AimAndShoot();
        FlipBasedOnMousePosition();

        if (bulletNumber <= 0 & isRechargingGun == false)
            RechargeGun();
    }

    void AimAndShoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        if (Input.GetButtonDown("Fire1") & bulletNumber > 0)
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
        bulletNumber -= 1;

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

    void RechargeGun () 
    {
        Debug.Log("I am recharging my gun");
        isRechargingGun = true;
        StartCoroutine(RechargeTime());  
    }

    IEnumerator ResetRecoilCooldown()
    {
        yield return new WaitForSeconds(recoilJumpCooldown);
        isRecoilJumpInCooldown = false;

    }

    IEnumerator RechargeTime()
    {
        yield return new WaitForSeconds(rechargeTime);
        isRechargingGun = false;

        bulletNumber = maxBulletNumber;
        Debug.Log("Ready to go again!");
    }
}
