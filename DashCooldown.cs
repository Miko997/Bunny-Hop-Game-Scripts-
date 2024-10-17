using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DashCooldown : MonoBehaviour
{
    public Button dashButton;
    public Image cooldownOverlay;
    public float cooldownTime = 3f; // Cooldown duration in seconds
    private bool isCooldown = false;
    private static bool dashButtonPressed = false; // Static flag to prevent jump

    void Start()
    {
        dashButton.onClick.AddListener(OnDashButtonPressed);
        cooldownOverlay.fillAmount = 0;
    }

    void OnDashButtonPressed()
    {
        if (!isCooldown)
        {
            Debug.Log("Dash button pressed");
            dashButtonPressed = true; // Set the static flag
            // Call the dash functionality here if necessary
            StartCoroutine(DashCooldownRoutine());
        }
    }

    private IEnumerator DashCooldownRoutine()
    {
        isCooldown = true;
        float elapsedTime = 0;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            cooldownOverlay.fillAmount = 1 - (elapsedTime / cooldownTime);
            Debug.Log($"Cooldown: {cooldownOverlay.fillAmount}");
            yield return null;
        }

        cooldownOverlay.fillAmount = 0;
        isCooldown = false;
    }

    public static bool IsDashButtonPressed()
    {
        return dashButtonPressed;
    }

    public void TriggerDashCooldown()
    {
        StartCoroutine(DashCooldownRoutine());
    }

    void LateUpdate()
    {
        // Reset the flag after the frame
        dashButtonPressed = false;
    }
}
