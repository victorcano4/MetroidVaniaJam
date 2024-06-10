using System.Collections;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    private int trapDamage = 1;
    public float waitTime;
    private Coroutine damageCoroutine;
    public Animator spike_animator;


    private void Start()
    {
        spike_animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(trapDamage);

            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(collision.gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                spike_animator.SetBool("isAttacking", false);
            }
        }
    }

    private IEnumerator DealDamageOverTime(GameObject player)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (player.CompareTag("Player"))
            {
                PlayerHealth.instance.TakeDamage(trapDamage);
                spike_animator.SetBool("isAttacking", true);
            }
        }
    }
}