using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Manages the player's health system, including lives, healing, shield handling,
/// damage logic, and game over behavior.
/// </summary>
public class L2S2_HealthSystem : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;
    public int maxHealth = 3;
    private int currentHealth;
    public Slider slider;
    public GameObject ship;
    public GameObject explosion;
    public GameObject gameOverUI;
    public bool hasShield = false;

    /// <summary>
    /// Creates the health system for our ship initializing current health and lives
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        currentLives = 1;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    /// <summary>
    /// Applies damage to the player, accounting for shield and lives.
    /// Triggers game over if health and lives are depleted.
    /// </summary>
    /// <param name="damage">Amount of damage to apply</param>
    public void TakeDamage(int damage)
    {
        if (hasShield)
        {
            destroyShield();
            return;
        }
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            if (currentLives > 1)
            {
                currentLives--;
                currentHealth = maxHealth;
            }
            else
            {
                currentLives = 0;
                Explode();
            }
        }
    }

    /// <summary>
    /// Function that heals the player for a certain amount
    /// </summary>
    /// <param name="amount"> healing amount</param>
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    /// <summary>
    /// Handles the ship's destruction and triggers the game over sequence.
    /// </summary>
    void Explode()
    {
        Instantiate(explosion, ship.transform.position, ship.transform.rotation);
        Destroy(ship);
        StartCoroutine(ShowGameOverAfterDelay(1f));
    }

    /// <summary>
    /// updates UI health each frame depeneding on current health
    /// </summary>
    void Update()
    {
        slider.value = currentHealth;
    }

    /// <summary>
    /// Adds a life to our ship, maximum of 3
    /// </summary>
    public void AddLife()
    {
        if (currentLives < maxLives)
        {
            currentLives = Mathf.Min(maxLives, currentLives + 1);
        }
    }

    /// <summary>
    /// Shows game over screen
    /// </summary>
    /// <param name="delay"> delay in seconds for screen to show up</param>
    IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// Destroys shield instead of doing damage if the shield is active
    /// </summary>
    private void destroyShield()
    {
        hasShield = false;
        var sprite = ship.GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
    }
}