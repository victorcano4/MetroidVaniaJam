using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    public AudioClip sfx;
    private AudioSource audioSource;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Find player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}
