using System.Collections;
using UnityEngine;

public class TntBarrelCord : MonoBehaviour
{
    private float timeUntilBarrelExplodes = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TntBarrel parentBarrel = GetComponentInParent<TntBarrel>();
            if (parentBarrel != null)
            {
                StartCoroutine(parentBarrel.BlowUpCountdown(timeUntilBarrelExplodes));
            }
        }
    }
}
