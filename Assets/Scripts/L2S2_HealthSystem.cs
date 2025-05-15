using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class L2S2_HealthSystem : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public Slider slider;
    public GameObject ship;
    public GameObject explosion;
    public GameObject gameOverUI;


    void Start()
    {
        currentHealth = maxHealth;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    void Explode()
    {

        Instantiate(explosion, ship.transform.position, ship.transform.rotation);

        Destroy(ship);

        StartCoroutine(ShowGameOverAfterDelay(1f));
    }

    void Update()
    {
        // Press 'X' to simulate taking 1 damage
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(1);
        }

        //Press 'Z' to simulate healing 1 health point
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Heal(1);
        }

        slider.value = currentHealth;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2 Scene 2");
    }

    IEnumerator ShowGameOverAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    Time.timeScale = 0f;
    gameOverUI.SetActive(true);
}


}