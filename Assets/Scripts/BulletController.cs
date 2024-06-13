using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float iveBeenAliveForTooLong = 3f;

    private void Update()
    {
        MrMeeseeks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
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
