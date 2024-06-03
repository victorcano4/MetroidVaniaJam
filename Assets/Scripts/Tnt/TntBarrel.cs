using System.Collections;
using UnityEngine;

public class TntBarrel : MonoBehaviour
{
    // Define a delegate and event for the explosion
    public delegate void BarrelExplodedHandler(TntBarrel barrel);
    public static event BarrelExplodedHandler OnBarrelExploded;

    // Explosion parameters
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    [SerializeField] private float tntChainExplosionTimer = 0.5f;

    // Reference to the spawner
    public TntBarrelSpawner spawner;

    public void Explode()
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
                otherBarrel.StartCoroutine(otherBarrel.BlowUpCountdown(tntChainExplosionTimer));
            }
        }

        // Trigger the explosion event
        OnBarrelExploded?.Invoke(this);

        // Destroy the barrel object
        Destroy(gameObject);
    }

    public IEnumerator BlowUpCountdown(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    // Draw the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
