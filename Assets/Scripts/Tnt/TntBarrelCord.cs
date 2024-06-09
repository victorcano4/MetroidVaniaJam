using System.Collections;
using UnityEngine;

public class TntBarrelCord : MonoBehaviour
{
    [SerializeField] private float timeUntilBarrelExplodes = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
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
