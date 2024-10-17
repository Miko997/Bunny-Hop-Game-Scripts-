using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip healthPickupSound;
    public AudioClip dashSound;
    public AudioClip buttonClickSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.tag = "Sound";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayJumpSound()
    {
        if (!SettingsManager.Instance.IsSoundMuted)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void PlayDeathSound()
    {
        if (!SettingsManager.Instance.IsSoundMuted)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    public void PlayHealthPickupSound()
    {
        if (!SettingsManager.Instance.IsSoundMuted)
        {
            audioSource.PlayOneShot(healthPickupSound);
        }
    }

    public void PlayDashSound()
    {
        if (!SettingsManager.Instance.IsSoundMuted)
        {
            audioSource.PlayOneShot(dashSound);
        }
    }

    public void PlayButtonClickSound()
    {
        if (!SettingsManager.Instance.IsSoundMuted)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
