using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    private PlayerRespawn respawn;
    private PlayerMovement player_movement;
    private ScreenShake screen_shake;
    public GameObject camera_shake;

    public int maxHealth = 3;
    public int health;

    [SerializeField] private Animator player_animator;

    private void Awake()
    {
        respawn = GetComponent<PlayerRespawn>();
        // Check if the instance already exists
        if (instance == null)
        {
            // If not, set it to this instance
            instance = this;

            // Ensure that this instance persists between scenes
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If an instance already exists and it's not this one, destroy this instance to enforce the singleton pattern
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        //Get animator component
        //player_animator = GetComponent<Animator>();

        //Get player movement component
        player_movement = GetComponent<PlayerMovement>();

        //Get Camera shake component
        screen_shake = camera_shake.GetComponent<ScreenShake>();
        
    }


    // Method to reduce health
    public void TakeDamage(int damage)
    {
        health -= damage;

        //Camera shake when player is damaged
        screen_shake.TriggerShake();

        if (health <= 0)
        {
            player_movement.isDead = true;
        }
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        health += amount;
    }

}
