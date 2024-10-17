using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; // Array of different obstacle prefabs
    public float spawnInterval = 17.5f; // Distance between each obstacle
    public float maxSpawnDistance = 52.5f; // Maximum distance to spawn obstacles (3 * 17.5)
    private float nextSpawnPositionX; // Next position to spawn an obstacle

    private Transform bunnyTransform; // Reference to the bunny's transform

    void Start()
    {
        bunnyTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the bunny using its tag
        nextSpawnPositionX = bunnyTransform.position.x + spawnInterval; // Initialize the next spawn position
    }

    void Update()
    {
        // Check if it's time to spawn a new obstacle
        if (bunnyTransform.position.x + maxSpawnDistance >= nextSpawnPositionX)
        {
            SpawnObstacle();
            nextSpawnPositionX += spawnInterval; // Update the next spawn position
        }

        // Despawn obstacles that are too far behind
        DespawnObstacles();
    }

    void SpawnObstacle()
    {
        // Randomly select an obstacle prefab to spawn
        int randomIndex = Random.Range(0, obstacles.Length);
        Vector3 spawnPosition = new Vector3(nextSpawnPositionX, -1.7f, 0); // Ensure the obstacle spawns at the correct height
        Instantiate(obstacles[randomIndex], spawnPosition, Quaternion.identity);
    }

    void DespawnObstacles()
    {
        // Find all obstacle instances in the scene
        GameObject[] spawnedObstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in spawnedObstacles)
        {
            if (obstacle.transform.position.x < bunnyTransform.position.x - maxSpawnDistance)
            {
                Destroy(obstacle); // Destroy obstacles that are too far behind
            }
        }
    }
}
