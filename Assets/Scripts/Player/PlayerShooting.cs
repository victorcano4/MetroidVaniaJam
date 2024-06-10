using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float knockbackForce = 5f;
    public float recoilJumpCooldown = 3f;
    public float maxBulletNumber = 3f;
    public float bulletNumber;
    public float rechargeTime;
    public bool isRechargingGun = false;
    public UIBulletsController UIController;
    public bool isAllowedToShoot = false;
    public bool isRecoilJumpUnlocked = false;


    private Rigidbody2D myRigidbody;
    private bool isRecoilJumpInCooldown = false;
    private PlayerMovement myPlayerMovement;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        bulletNumber = maxBulletNumber;
        myPlayerMovement = GetComponent<PlayerMovement>();
        UIController = GameObject.Find("BulletsContainer").GetComponent<UIBulletsController>();
    }

    void Update()
    {
        AimAndShoot();

        if (bulletNumber <= 0 & isRechargingGun == false)
            RechargeGun();
    }

    void AimAndShoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        if (Input.GetButtonDown("Fire1") && bulletNumber > 0 && isAllowedToShoot)
        {
            Shoot(direction);

            //Sreenshake trigger
           ScreenShake.Instance.TriggerShake();

        }
        if (isRecoilJumpUnlocked && Input.GetButtonDown("Fire2"))
        {
            ShootRecoilJump(direction);

            //Sreenshake trigger
            ScreenShake.Instance.TriggerShake();
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
        UIController.BulletShot();    
    }

    void ShootRecoilJump(Vector2 direction)
    {
        if (!isRecoilJumpInCooldown)
        {
            isRecoilJumpInCooldown = true;
            StartCoroutine(ResetRecoilCooldown());
            ApplyKnockback(direction);
        }
    }

    void ApplyKnockback(Vector2 direction)
    {        
        Vector2 knockbackDirection = -direction; // Knockback is in the opposite direction of the shot
        if(myPlayerMovement.isGrounded)
            knockbackDirection.x *= 0.25f;
        myRigidbody.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode2D.Force);

    }

    void RechargeGun() 
    {
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
        UIController.Reloaded();
    }
}
