using System.Collections;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    private int trapDamage = 1;
    private Coroutine damageCoroutine;
    public Animator spike_animator;


    private void Start()
    {
        spike_animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(trapDamage);
            
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(collision.gameObject));
            }
        }

        else
        {
            //Return animation to idle
            spike_animator.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamageOverTime(GameObject player)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (player.CompareTag("Player"))
            {
                PlayerHealth.instance.TakeDamage(trapDamage);
                //Animate trap
                spike_animator.SetBool("isAttacking", true);
            }
        }
    }
}