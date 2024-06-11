using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip pickup_sfx;
    private AudioSource audioSource;

    private void Start()
    {
        // Get audio source component
        audioSource = GetComponent<AudioSource>();
    }


    void PlaySFX(AudioClip sfx)
    {
        if (!audioSource.isPlaying) // Ensure the sound is not played repeatedly
        {
            audioSource.PlayOneShot(sfx);
        }
    }

}
