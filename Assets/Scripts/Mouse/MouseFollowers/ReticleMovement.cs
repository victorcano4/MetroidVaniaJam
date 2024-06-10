using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMovement : MonoBehaviour
{
    // Speed at which the object will follow the mouse
    private float speed = 5f;
    [SerializeField] private float minProximityToShoot = 2f;
    private PlayerShooting playerShooting;

    private void Start()
    {
        playerShooting = GetComponentInParent<PlayerShooting>();
    }

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z position is 0 for 2D

        float distance = Vector3.Distance(mousePosition, transform.position);
        if (distance < 0)
            distance = distance * -1;
        if (distance < minProximityToShoot)
        {
            playerShooting.isAllowedToShoot = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }            
        else
        {
            playerShooting.isAllowedToShoot = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }           
        // Move the object towards the mouse position
        transform.position = Vector3.Lerp(transform.position, mousePosition, speed * Time.deltaTime);
    }

}

