using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BunnyController : MonoBehaviour
{
    public float speed = 5f; // Constant speed of the bunny moving to the right
    public float jumpSpeed = 7f; // Speed of the bunny while jumping
    public float jumpForce = 12f; // Jump force applied to the bunny
    public float fallMultiplier = 2.5f; // Multiplier for falling speed
    public float dashDistance = 4f; // Distance to dash
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 2f; // Cooldown time for dash
    public HealthManager healthManager; // Reference to the HealthManager
    public ScoreManager scoreManager; // Reference to the ScoreManager
    public DashCooldown dashCooldownScript; // Reference to the DashCooldown script

    private bool isJumping = false; // Flag to check if the bunny is in the air
    private bool canDoubleJump = true; // Flag to check if the bunny can double jump
    private bool canDash = true; // Flag to check if the bunny can dash
    private bool isDead = false; // Flag to check if the bunny is dead
    private bool dashButtonPressed = false; // Flag to check if the dash button is pressed
    private bool isDashing = false; // Flag to check if the bunny is dashing
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    // Animation frames
    public Sprite runLFrame; // Left leg running frame
    public Sprite runRFrame; // Right leg running frame
    public Sprite jumpStartFrame; // Frame for jump start
    public Sprite jumpMidFrame; // Frame for mid-jump
    public Sprite jumpEndFrame; // Frame for end of jump
    public Sprite deathFrame; // Frame for death

    private bool toggleRunFrame = false; // Toggle between run frames
    private float frameTimer = 0f;
    private float frameRate = 0.1f; // Change frame every 0.1 seconds

    private float jumpStartTime; // Time when the jump started
    private float jumpStartDuration = 0.2f; // Duration of the jump start animation

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the SpriteRenderer component

        // Ensuring the colliders are correctly set up
        if (!GetComponent<Collider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        if (isDead) return; // Do nothing if the bunny is dead

        // Determine the horizontal speed based on whether the bunny is jumping
        float currentSpeed = isJumping ? jumpSpeed : speed;

        // Move the bunny to the right
        transform.Translate(new Vector2(currentSpeed * Time.deltaTime, 0));

        // Handle jump input for both mouse click and touch, but not if the dash button is pressed
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && !dashButtonPressed && (!isJumping || canDoubleJump))
        {
            if (isJumping)
            {
                canDoubleJump = false; // Disable further double jumps
                rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity before double jump
            }
            isJumping = true; // Set jumping flag
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Apply jump force
            spriteRenderer.sprite = jumpStartFrame; // Set jump start frame
            jumpStartTime = Time.time; // Record the time when the jump started
            AudioManager.Instance.PlayJumpSound(); // Play jump sound
        }

        // Handle dash input for PC (space bar)
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Dash();
            dashCooldownScript.TriggerDashCooldown(); // Trigger the dash cooldown
        }

        // Apply faster fall speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Update animation based on velocity
        if (isJumping)
        {
            if (Time.time - jumpStartTime > jumpStartDuration)
            {
                spriteRenderer.sprite = jumpMidFrame; // Transition to mid-jump frame
            }

            if (rb.velocity.y < 0)
            {
                spriteRenderer.sprite = jumpEndFrame; // Set end-jump frame
            }
        }
        else
        {
            frameTimer += Time.deltaTime;
            if (frameTimer >= frameRate)
            {
                frameTimer = 0f;
                toggleRunFrame = !toggleRunFrame; // Toggle between run frames
                spriteRenderer.sprite = toggleRunFrame ? runLFrame : runRFrame; // Set running frame
            }
        }

        // Reset dash button flag after handling input
        dashButtonPressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead || isDashing) return; // Do nothing if the bunny is dead or dashing

        // When the bunny collides with the ground, reset the jumping flag and animation
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // Reset jumping flag
            canDoubleJump = true; // Reset double jump capability
            frameTimer = 0f; // Reset frame timer
            spriteRenderer.sprite = runLFrame; // Start with left leg running frame
        }
    }

    private void Dash()
    {
        // Perform the dash
        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        canDash = false; // Disable dashing during the dash
        isDashing = true; // Set dashing flag to true

        float startTime = Time.time;
        Vector3 start = transform.position;
        Vector3 end = transform.position + Vector3.right * dashDistance;

        AudioManager.Instance.PlayDashSound(); // Play dash sound

        while (Time.time < startTime + dashDuration)
        {
            transform.position = Vector3.Lerp(start, end, (Time.time - startTime) / dashDuration);
            yield return null;
        }

        transform.position = end;

        isDashing = false; // Reset dashing flag after dash
        // Start cooldown coroutine
        StartCoroutine(DashCooldownCoroutine());
    }

    private IEnumerator DashCooldownCoroutine()
    {
        yield return new WaitForSeconds(dashCooldown); // Wait for cooldown period
        canDash = true; // Enable dashing
    }

    // Method to be called by the mobile button
    public void OnDashButtonPressed()
    {
        if (canDash)
        {
            dashButtonPressed = true; // Set the dash button flag
            Dash();
            dashCooldownScript.TriggerDashCooldown(); // Trigger the dash cooldown
        }
    }

    // Method to handle damage
    public void TakeDamage()
    {
        if (isDashing) return; // Prevent taking damage while dashing
        healthManager.TakeDamage(); // Reduce health by 1
        if (healthManager.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        healthManager.Heal(amount); // Call the heal method in the HealthManager
        AudioManager.Instance.PlayHealthPickupSound(); // Play health pickup sound
    }

    private void Die()
    {
        isDead = true; // Set the bunny to dead
        spriteRenderer.sprite = deathFrame; // Change sprite to death frame
        rb.velocity = Vector2.zero; // Stop movement
        rb.isKinematic = true; // Disable physics
        scoreManager.OnPlayerDeath(); // Notify the ScoreManager of the player's death
        AudioManager.Instance.PlayDeathSound(); // Play death sound
        // Add a delay before restarting the game
        StartCoroutine(RestartGameAfterDelay());
    }

    private IEnumerator RestartGameAfterDelay()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }
}
