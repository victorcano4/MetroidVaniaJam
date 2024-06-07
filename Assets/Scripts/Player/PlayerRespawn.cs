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
        // Add 1.5 to the Y position of the respawn position
        Vector3 adjustedRespawnPosition = new Vector3(respawnPosition.x, respawnPosition.y + 1.5f, respawnPosition.z);
        transform.position = adjustedRespawnPosition;
    }

    public void Die()
    {
        Respawn();
    }
}
