using System.Collections;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    public int trapDamage = 3;
    public float waitTime;
    private Coroutine damageCoroutine;
    public Animator spike_animator;
    public AudioClip sfx;
    private AudioSource audioSource;


    private void Start()
    {
        spike_animator = gameObject.GetComponent<Animator>();

        //Get audio source
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(trapDamage);
            spike_animator.SetBool("isAttacking", true);

            //Play sfx
            audioSource.PlayOneShot(sfx);

            if (damageCoroutine == null)
            {
                //damageCoroutine = StartCoroutine(DealDamageOverTime(collision.gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spike_animator.SetBool("isAttacking", false);

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
            yield return new WaitForSeconds(waitTime);

            if (player.CompareTag("Player"))
            {
                PlayerHealth.instance.TakeDamage(trapDamage);
            }
        }
    }

}