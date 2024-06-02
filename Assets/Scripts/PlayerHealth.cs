using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int maxHealth = 3;
    public int health;

    private void Awake()
    {
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
    }

    // Method to reduce health
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        health += amount;
    }

}
