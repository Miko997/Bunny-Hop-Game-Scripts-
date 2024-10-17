using UnityEngine;

public class Tree : MonoBehaviour
{
    public float minHeight = 4f; // Minimum height of the tree
    public float maxHeight = 7.5f; // Maximum height of the tree
    public float groundYPosition = 0; // The Y position of the ground
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Set a random height for the tree
            float height = Random.Range(minHeight, maxHeight);

            // Adjust the tree's scale to maintain the aspect ratio
            transform.localScale = new Vector3(height, height, transform.localScale.z);

            // Calculate the offset to position the tree so the bottom is at the ground level
            float offsetY = spriteRenderer.bounds.extents.y * transform.localScale.y;

            // Position the tree so the bottom is at the ground level
            transform.position = new Vector3(transform.position.x, groundYPosition + offsetY, transform.position.z);
        }
        else
        {
            Debug.LogError("SpriteRenderer component missing from the tree object.");
        }
    }
}
