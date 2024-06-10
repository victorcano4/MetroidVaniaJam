using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool alreadyChecked = false;
    private Animator checkpointAnimator;

    private void Start()
    {
        checkpointAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Save the checkpoint position to the player
            if (!alreadyChecked)
            {
                collision.GetComponent<PlayerRespawn>().SetCheckpoint(transform.position);
                alreadyChecked = true;
                checkpointAnimator.SetBool("SteppedOn", true);
            }
        }
    }
}
