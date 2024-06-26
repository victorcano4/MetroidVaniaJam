using System.Collections;
using UnityEngine;

public class TntBarrelSpawner : MonoBehaviour
{
    public GameObject linkedBarrel;
    public float respawnDelay = 2.0f;

    private TntBarrel currentBarrel;
    public GameObject player;

    private void OnEnable()
    {
        // Subscribe to the barrel exploded event
        TntBarrel.OnBarrelExploded += HandleBarrelExploded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the barrel exploded event
        TntBarrel.OnBarrelExploded -= HandleBarrelExploded;
    }

    private void Start()
    {
        // Spawn the initial barrel
        //SpawnBarrel();

        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.transform.position.x  >= gameObject.transform.position.x)
        {
            SpawnBarrel();
            Destroy(gameObject);
        }
    }

    private void HandleBarrelExploded(TntBarrel barrel)
    {
        // Check if the exploded barrel is the one this spawner created
        if (barrel == currentBarrel)
        {
            // Start the coroutine to spawn a new barrel after a delay
            //StartCoroutine(SpawnBarrelAfterDelay());
        }
    }

    private IEnumerator SpawnBarrelAfterDelay()
    {
        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // Spawn a new barrel
        SpawnBarrel();
    }

    private void SpawnBarrel()
    {
        // Instantiate a new barrel and keep a reference to it
        GameObject barrelObject = Instantiate(linkedBarrel, transform.position - new Vector3(0, 1, 0), transform.rotation);
        currentBarrel = barrelObject.GetComponentInChildren<TntBarrel>();

        // Set this spawner as the reference in the barrel
        currentBarrel.spawner = this;
    }
}
