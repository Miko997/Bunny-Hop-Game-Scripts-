using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public GameObject heartPrefab;
    public Transform heartContainer; // Assign the parent transform for the hearts in the inspector
    private List<GameObject> hearts = new List<GameObject>();
    public float flickerDuration = 0.5f; // Duration of the flickering
    public int flickerCount = 3; // Number of flickers
    public float deathDelay = 5f; // Delay before restarting the scene after death

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsDisplay();
    }

    void UpdateHeartsDisplay()
    {
        // Clear existing hearts
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        // Create new hearts based on current health
        for (int i = 0; i < currentHealth; i++)
        {
            AddHeart();
        }
    }

    void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, heartContainer);
        heart.GetComponent<RectTransform>().localScale = Vector3.one; // Ensure correct scaling
        hearts.Add(heart);
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            if (hearts[currentHealth] != null)
            {
                StartCoroutine(FlickerHeart(hearts[currentHealth])); // Start flickering the heart
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            UpdateHeartsDisplay();
        }
    }

    private IEnumerator FlickerHeart(GameObject heart)
    {
        Image heartImage = heart.GetComponent<Image>();
        if (heartImage != null)
        {
            for (int i = 0; i < flickerCount; i++)
            {
                if (heartImage == null)
                {
                    yield break;
                }

                heartImage.enabled = false;
                yield return new WaitForSeconds(flickerDuration / (flickerCount * 2));

                if (heartImage == null)
                {
                    yield break;
                }

                heartImage.enabled = true;
                yield return new WaitForSeconds(flickerDuration / (flickerCount * 2));
            }

            if (heart != null)
            {
                Destroy(heart); // Destroy the heart after flickering
            }
        }
    }

    public void Heal(int amount)
    {
        for (int i = 0; i < amount && currentHealth < maxHealth; i++)
        {
            currentHealth++;
        }
        UpdateHeartsDisplay();
    }

    private void Die()
    {
        Debug.Log("Player is dead");
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your actual game scene name to restart
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
