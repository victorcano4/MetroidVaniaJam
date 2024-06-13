using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPosition;
    private PlayerHealth player_health_component;
    

    private void Start()
    {
        // Initialize the respawn position to the player's starting position
        respawnPosition = transform.position;

        //Get Health component
        player_health_component = GetComponent<PlayerHealth>();
    }

    public void SetCheckpoint(Vector3 newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }

    public void Respawn()
    {
        // Add 1.5 to the Y position of the respawn position
        Vector3 adjustedRespawnPosition = new Vector3(respawnPosition.x, respawnPosition.y + 1.5f, respawnPosition.z);
        transform.position = adjustedRespawnPosition;

        player_health_component.health += player_health_component.maxHealth;
    }

    public void Die()
    {
        Respawn();
    }
}
