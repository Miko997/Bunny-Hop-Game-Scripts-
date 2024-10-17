using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject healthPickupPrefab; // Reference to the health pickup prefab
    public float spawnInterval = 10f; // Time interval between spawns
    private float nextSpawnTime;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player using its tag
        nextSpawnTime = Time.time + spawnInterval; // Initialize the next spawn time
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnHealthPickup();
            nextSpawnTime = Time.time + spawnInterval; // Schedule the next spawn
        }
    }

    void SpawnHealthPickup()
    {
        float spawnX = playerTransform.position.x + 52.5f; // Spawn position X
        float spawnY = Random.Range(2f, 5f); // Random Y position between 2 and 5
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        Instantiate(healthPickupPrefab, spawnPosition, Quaternion.identity); // Spawn the health pickup
    }
}
