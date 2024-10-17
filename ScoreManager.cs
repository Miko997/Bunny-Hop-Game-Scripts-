using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text component for displaying score during the game
    public Text endScoreText; // Reference to the UI Text component for displaying score upon death
    private float score;
    private float pointsPerSecond = 10f; // Initial points per second
    private float timeElapsed;
    private bool isDead = false;

    void Start()
    {
        score = 0f;
        UpdateScoreText();
        endScoreText.gameObject.SetActive(false); // Hide the end score text initially
    }

    void Update()
    {
        if (!isDead)
        {
            timeElapsed += Time.deltaTime;
            score += pointsPerSecond * Time.deltaTime;
            UpdateScoreText();

            if (timeElapsed >= 180f) // Every 3 minutes (180 seconds)
            {
                pointsPerSecond += 5f;
                timeElapsed = 0f; // Reset the timer
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = " " + Mathf.FloorToInt(score).ToString();
    }

    public void OnPlayerDeath()
    {
        isDead = true;
        scoreText.gameObject.SetActive(false); // Hide the in-game score text
        endScoreText.text = " " + Mathf.FloorToInt(score).ToString();
        endScoreText.gameObject.SetActive(true); // Show the end score text

        // Check if the current score is a personal best
        if (Mathf.FloorToInt(score) > PlayerStats.PersonalBest)
        {
            PlayerStats.UpdatePersonalBest(Mathf.FloorToInt(score));
        }

        PlayerStats.IncrementGamesPlayed();
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
