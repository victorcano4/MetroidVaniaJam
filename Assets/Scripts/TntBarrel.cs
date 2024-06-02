using System.Collections;
using UnityEngine;

public class TntBarrel : MonoBehaviour
{
    private float timeUntilBarrelExplodes = 3;

    // Define a delegate and event for the explosion
    public delegate void BarrelExplodedHandler(TntBarrel barrel);
    public static event BarrelExplodedHandler OnBarrelExploded;

    // Explosion parameters
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 1.0f;

    // Reference to the spawner
    public TntBarrelSpawner spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(BlowUpCountdown());
        }
    }

    IEnumerator BlowUpCountdown()
    {
        yield return new WaitForSeconds(timeUntilBarrelExplodes);

        // Trigger the explosion effect
        Explode();

        // Trigger the explosion event
        OnBarrelExploded?.Invoke(this);

        // Destroy the barrel object
        Destroy(gameObject);
    }

    void Explode()
    {
        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // Loop through each collider
        foreach (Collider2D hit in colliders)
        {
            // Get the Rigidbody2D component of the object
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            // If the object has a Rigidbody2D, apply explosion force
            if (rb != null)
            {
                Vector2 explosionDirection = rb.position - (Vector2)transform.position;
                float explosionDistance = explosionDirection.magnitude;
                float explosionEffect = 1 - (explosionDistance / explosionRadius);

                rb.AddForce(explosionDirection.normalized * explosionForce * explosionEffect, ForceMode2D.Impulse);
            }

            // Check if the object is another TNT barrel
            TntBarrel otherBarrel = hit.GetComponent<TntBarrel>();
            if (otherBarrel != null && otherBarrel != this)
            {
                // Start the explosion countdown for the other barrel
                otherBarrel.StartCoroutine(otherBarrel.BlowUpCountdown());
            }
        }
    }

    // Draw the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}