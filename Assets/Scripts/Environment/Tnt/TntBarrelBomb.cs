using System.Collections;
using UnityEngine;

public class TntBarrelBomb : MonoBehaviour
{
    [SerializeField] private float timeUntilBarrelExplodes = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TntBarrel parentBarrel = GetComponentInParent<TntBarrel>();
            if (parentBarrel != null)
            {
                StartCoroutine(parentBarrel.BlowUpInstant(timeUntilBarrelExplodes));
            }
        }
    }
}
