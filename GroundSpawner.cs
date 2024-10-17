using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab; // The ground prefab to spawn
    public Transform playerTransform; // Reference to the player's transform
    public float groundLength = 10f; // The length of each ground piece
    public int initialGroundPieces = 5; // Initial number of ground pieces to spawn
    private List<GameObject> groundPieces = new List<GameObject>(); // List to keep track of spawned ground pieces

    void Start()
    {
        for (int i = 0; i < initialGroundPieces; i++)
        {
            SpawnGroundPiece(i * groundLength);
        }
    }

    void Update()
    {
        if (groundPieces[0].transform.position.x + groundLength < playerTransform.position.x)
        {
            MoveGroundPiece();
        }
    }

    void SpawnGroundPiece(float xPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, transform.position.y, transform.position.z);
        GameObject groundPiece = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
        groundPieces.Add(groundPiece);
    }

    void MoveGroundPiece()
    {
        GameObject movedPiece = groundPieces[0];
        groundPieces.RemoveAt(0);
        float newXPosition = groundPieces[groundPieces.Count - 1].transform.position.x + groundLength;
        movedPiece.transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        groundPieces.Add(movedPiece);
    }
}
