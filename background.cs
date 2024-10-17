using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Vector3 startPosition;
    private float length;

    void Start()
    {
        startPosition = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (Time.time * scrollSpeed) % length;
        transform.position = startPosition + Vector3.left * temp;
    }
}
