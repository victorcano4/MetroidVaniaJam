using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool alreadyChecked = false;
    private Animator checkpointAnimator;

    //Light 2d component in player prefab
    public LightDescrease lightDescrase_component;

    private void Start()
    {
        checkpointAnimator = GetComponent<Animator>();

        lightDescrase_component = GetComponent<LightDescrease>();
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

            //Restore light
            lightDescrase_component.playerLight.intensity += lightDescrase_component.increaseAmount;
        }
    }
}
