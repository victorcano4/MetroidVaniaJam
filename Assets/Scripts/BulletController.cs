using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float iveBeenAliveForTooLong = 3f;
    public float damage = 1;  // Modify me to alter the ammount of damage done

    private void Update()
    {
        MrMeeseeks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<EnemyAI>().slimeHealth -= damage;
        }

        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(gameObject);
    }

    private void MrMeeseeks()
    {
        iveBeenAliveForTooLong -= Time.deltaTime;
        if (iveBeenAliveForTooLong < 0)
        {
            Destroy(gameObject);
        }
    }
}
