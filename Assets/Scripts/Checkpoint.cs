using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the checkpoint position to the player
            other.GetComponent<PlayerRespawn>().SetCheckpoint(transform.position);
        }
    }
}
