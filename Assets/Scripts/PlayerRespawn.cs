using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPosition;

    private void Start()
    {
        // Initialize the respawn position to the player's starting position
        respawnPosition = transform.position;
    }

    public void SetCheckpoint(Vector3 newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }

    public void Respawn()
    {
        transform.position = respawnPosition;
    }

    // Example method to simulate player death
    public void Die()
    {
        // Call this method when the player dies
        Respawn();
    }
}
