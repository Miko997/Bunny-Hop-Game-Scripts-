using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject statsPanel;
    public GameObject settingsPanel;
    public Text personalBestText;
    public Text gamesPlayedText;
    public Toggle musicToggle;
    public Toggle soundToggle;

    private void Start()
    {
        musicToggle.isOn = !SettingsManager.Instance.IsMusicMuted;
        soundToggle.isOn = !SettingsManager.Instance.IsSoundMuted;

        musicToggle.onValueChanged.AddListener(isOn => SettingsManager.Instance.ToggleMusic(!isOn));
        soundToggle.onValueChanged.AddListener(isOn => SettingsManager.Instance.ToggleSound(!isOn));
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your actual game scene name
    }

    public void OpenSettings()
    {
        AudioManager.Instance.PlayButtonClickSound();
        Debug.Log("Settings button clicked");
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        AudioManager.Instance.PlayButtonClickSound();
        Debug.Log("Close settings button clicked");
        settingsPanel.SetActive(false);
    }

    public void OpenStats()
    {
        AudioManager.Instance.PlayButtonClickSound();
        Debug.Log("Stats button clicked");
        statsPanel.SetActive(true);

        // Update stats display
        personalBestText.text = "Personal Best: " + PlayerStats.PersonalBest;
        gamesPlayedText.text = "Games Played: " + PlayerStats.GamesPlayed;
    }

    public void CloseStats()
    {
        AudioManager.Instance.PlayButtonClickSound();
        statsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonClickSound();
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        AudioManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual main menu scene name
    }
}
