using UnityEngine;

public class LoopingGroundUI : MonoBehaviour
{
    public float speed = 2f; // Speed at which the ground moves
    public RectTransform[] grounds; // Array of ground UI elements
    public float groundWidth; // Width of each ground element

    private Vector2 offScreenPosition; // Position off the screen to reset to

    void Update()
    {
        foreach (RectTransform ground in grounds)
        {
            // Move the ground to the left
            ground.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // Check if the ground has moved past the reset position
            if (ground.anchoredPosition.x <= -groundWidth)
            {
                // Reset the ground position to create a looping effect
                offScreenPosition = new Vector2(groundWidth * (grounds.Length - 1), ground.anchoredPosition.y);
                ground.anchoredPosition = offScreenPosition;
            }
        }
    }
}
