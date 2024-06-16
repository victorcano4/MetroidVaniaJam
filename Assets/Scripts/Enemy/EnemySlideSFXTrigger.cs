using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlideSFXTrigger : MonoBehaviour
{
    public float detectionRange;
    public float detectionRangeSfx;    //detection range to play the sfx
    private Transform player;

    public AudioClip idleSound;       // Sound to play when patroling
    public AudioClip detectSound;     // Sound to play when the player is detected
    private AudioSource audioSource;
    

    void Start()
    {
        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Find player object
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Play sfx when the player is at X distance
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            PlaySFX(detectSound);
        }
        else if (Vector3.Distance(transform.position, player.position) <= detectionRangeSfx)
        {
            PlaySFX(idleSound);
        }
        
        else
        {
            audioSource.Stop();
        }
    }

    //Play sfx
    void PlaySFX(AudioClip sfx)
    {
        if (audioSource != null && sfx != null && !audioSource.isPlaying) // Ensure the sound is not played repeatedly
        {
            Debug.Log("audio is playing");
            audioSource.PlayOneShot(sfx);
        }
    }



}
