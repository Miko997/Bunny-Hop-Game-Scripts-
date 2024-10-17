using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public float floatAmplitude = 0.5f; // Amplitude of the floating motion
    public float floatFrequency = 1f; // Frequency of the floating motion

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Add floating motion
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BunnyController bunnyController = other.GetComponent<BunnyController>();
            if (bunnyController != null)
            {
                Debug.Log("Health pickup collected by player");
                bunnyController.RestoreHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
