using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    private PlayerRespawn respawn;

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
            DontDestroyOnLoad(gameObject);
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
        player_animator = GetComponent<Animator>();
    }

    // Method to reduce health
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            respawn.Die();
            health = maxHealth;
            player_animator.SetBool("isDead", true);
        }
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        health += amount;
    }

}
