using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRechargerMaster : MonoBehaviour
{
    public GameObject lightRechargePrefab; // Prefab for the light recharge
    public List<GameObject> lightRechargers; // List of light recharge GameObjects
    public float respawnTime;         // Time to respawn the light recharge

    public List<Vector2> lightPositions; // List to store the positions of the light rechargers

    private void Start()
    {
        // Initialize the lightPositions list
        lightPositions = new List<Vector2>();

        // Get positions from the light rechargers
        foreach (GameObject lightRecharge in lightRechargers)
        {
            if (lightRecharge != null)
            {
                lightPositions.Add(lightRecharge.transform.position);
            }
        }

    }

    public void CollectLight(Vector2 position)
    {
        // Start the respawn coroutine for the collected light recharge
        StartCoroutine(RespawnLightRecharge(position));
    }

    private void SpawnLightRecharge(Vector2 position)
    {
        Instantiate(lightRechargePrefab, position, Quaternion.identity);
    }

    private IEnumerator RespawnLightRecharge(Vector2 position)
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnLightRecharge(position);
    }
}

