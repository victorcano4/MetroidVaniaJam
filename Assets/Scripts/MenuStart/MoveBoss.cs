using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.MoveBoss();
        }
    }
}
