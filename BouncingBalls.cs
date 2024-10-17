using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float minHeight = 2f; // Minimum bounce height
    public float maxHeight = 9.5f; // Maximum bounce height
    public float bounceSpeed = 2f; // Speed of the bounce
    public float rotationSpeed = 360f; // Speed of rotation in degrees per second

    private float bounceHeight; // The height to which the ball will bounce
    private Vector3 groundPos; // The ground position of the ball
    private bool movingUp = true; // Direction of movement
    private Transform spriteTransform; // Reference to the sprite's transform

    void Start()
    {
        groundPos = new Vector3(transform.position.x, -1.7f, transform.position.z); // Initialize the ground position
        bounceHeight = Random.Range(minHeight, maxHeight); // Set a random bounce height within the range
        spriteTransform = GetComponent<SpriteRenderer>().transform; // Get the sprite transform
    }

    void Update()
    {
        float move = bounceSpeed * Time.deltaTime;
        float rotate = rotationSpeed * Time.deltaTime;

        if (movingUp)
        {
            transform.Translate(Vector3.up * move, Space.World);
            if (transform.position.y >= groundPos.y + bounceHeight)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * move, Space.World);
            if (transform.position.y <= groundPos.y)
            {
                movingUp = true;
                bounceHeight = Random.Range(minHeight, maxHeight); // Set a new random bounce height for the next bounce
            }
        }

        // Rotate the sprite around its local Z axis
        spriteTransform.Rotate(Vector3.forward * rotate);
    }
}
