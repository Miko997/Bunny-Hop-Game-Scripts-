using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera should follow
    public float yOffset = 3.31f; // The fixed y position for the camera
    public float xOffset = 8.6f; // The offset for the x position to keep the bunny at the right border

    void Update()
    {
        // Follow the target's x position with an offset, and keep the y position fixed
        transform.position = new Vector3(target.position.x + xOffset, yOffset, transform.position.z);
    }
}
