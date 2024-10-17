using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public bool IsMusicMuted { get; private set; }
    public bool IsSoundMuted { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic(bool isMuted)
    {
        IsMusicMuted = isMuted;
        PlayerPrefs.SetInt("MusicMuted", IsMusicMuted ? 1 : 0);
        ApplySettings();
    }

    public void ToggleSound(bool isMuted)
    {
        IsSoundMuted = isMuted;
        PlayerPrefs.SetInt("SoundMuted", IsSoundMuted ? 1 : 0);
        ApplySettings();
    }

    private void LoadSettings()
    {
        IsMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        IsSoundMuted = PlayerPrefs.GetInt("SoundMuted", 0) == 1;
        ApplySettings();
    }

    public void ApplySettings()
    {
        var soundSources = GameObject.FindGameObjectsWithTag("Sound");
        foreach (var source in soundSources)
        {
            var audioSource = source.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = IsSoundMuted;
            }
        }

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.ApplySettings();
        }
    }
}
