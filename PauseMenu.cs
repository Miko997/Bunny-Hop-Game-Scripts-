using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject navMenuUI;
    public GameObject settingsPanelUI;
    public Toggle musicToggle;
    public Toggle soundToggle;
    public Button navButton;

    private bool isPaused = false;

    void Start()
    {
        if (navMenuUI != null) navMenuUI.SetActive(false);
        if (settingsPanelUI != null) settingsPanelUI.SetActive(false);

        navButton.onClick.AddListener(TogglePauseMenu);

        musicToggle.isOn = !SettingsManager.Instance.IsMusicMuted;
        soundToggle.isOn = !SettingsManager.Instance.IsSoundMuted;

        musicToggle.onValueChanged.AddListener(SettingsManager.Instance.ToggleMusic);
        soundToggle.onValueChanged.AddListener(SettingsManager.Instance.ToggleSound);
    }

    void TogglePauseMenu()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        navMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        navMenuUI.SetActive(false);
        settingsPanelUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenSettings()
    {
        settingsPanelUI.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanelUI.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual main menu scene name
    }
}
