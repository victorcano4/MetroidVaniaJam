using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public AudioClip pickup_sfx;
    private AudioSource audioSource;

    private void Start()
    {
        // Get audio source component
        audioSource = GetComponent<AudioSource>();

    }


    public enum Upgrade
    {
        Gliding,
        RecoilJump
    }
    public Upgrade UpgradeType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player_prefab")
        {
            switch(UpgradeType)
            {
                case Upgrade.Gliding:
                    // play transforming animation
                    collision.GetComponent<PlayerGlide>().isGlidingUnlocked = true;
                    break;
                case Upgrade.RecoilJump:
                    //play transforming animation
                    collision.GetComponent<PlayerShooting>().isRecoilJumpUnlocked = true;
                    break;
            }

            //Play sfx for picking up upgrade
            PlaySFX(pickup_sfx);

            //Destroy self
            Destroy(gameObject);
        }
    }


    void PlaySFX(AudioClip sfx)
    {
        if (!audioSource.isPlaying) // Ensure the sound is not played repeatedly
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}
