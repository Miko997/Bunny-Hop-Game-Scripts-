using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private bool hasDamagedPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDamagedPlayer && collision.CompareTag("Player"))
        {
            collision.GetComponent<BunnyController>().TakeDamage();
            StartCoroutine(ResetDamageFlag());
        }
    }

    private IEnumerator ResetDamageFlag()
    {
        hasDamagedPlayer = true;
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame to prevent immediate multiple collisions
        hasDamagedPlayer = false; // Reset the flag, making it possible for the obstacle to damage again if needed in the future
    }

    // Reset the flag when the obstacle is reused or respawned
    public void ResetDamage()
    {
        hasDamagedPlayer = false;
    }
}
