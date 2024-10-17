using UnityEngine;
using System.Collections; 

public class FallingRock : MonoBehaviour
{
    public float fallSpeed = 5f; // Speed at which the rock falls
    public GameObject rockPrefab; // Reference to the rock prefab
    private bool hasHitGround = false; // Flag to check if the rock has hit the ground
    private Transform bunnyTransform;

    void Start()
    {
        // Set the initial Y position to 13
        Vector3 position = transform.position;
        position.y = 13f;
        transform.position = position;

        bunnyTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the bunny using its tag
    }

    void Update()
    {
        // Move the rock downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Check if the rock falls below a certain point and has not already hit the ground
        if (!hasHitGround && transform.position.y <= -1.9f)
        {
            hasHitGround = true; // Set the flag
            StartCoroutine(SpawnNewRock()); // Start the coroutine to spawn a new rock
        }

        // Destroy the rock if it falls below a certain point (e.g., -5 on the Y axis)
        if (transform.position.x < bunnyTransform.position.x - 52.5f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SpawnNewRock()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Spawn a new rock at the initial Y position
        Vector3 spawnPosition = new Vector3(transform.position.x, 13f, transform.position.z);
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        // Destroy the current rock after spawning a new one
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        

            // Destroy the rock upon collision with the player
            Destroy(gameObject);
        }
    }
}
