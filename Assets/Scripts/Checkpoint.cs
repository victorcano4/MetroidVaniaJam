using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool alreadyChecked = false;
    private Animator checkpointAnimator;
    public GameObject text_saving;
    public float timeCountdown = 1;
    private GameObject player;

    //Light 2d component in player prefab
    public LightDescrease lightDescrase_component;

    private void Start()
    {
        checkpointAnimator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        lightDescrase_component = player.GetComponent<LightDescrease>();
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

            //Activate loading text for X secs
            StartCoroutine(savingCountdown());
        }
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            //Restore light
            lightDescrase_component.playerLight.intensity = lightDescrase_component.maxIntensity;
        }
        
    }

    private IEnumerator savingCountdown()
    {
        text_saving.SetActive(true);

        yield return new WaitForSeconds(timeCountdown);

        text_saving.SetActive(false);
    }
}
