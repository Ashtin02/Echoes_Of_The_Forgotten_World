using UnityEngine;

/// <summary>
/// Handles collision behavior for different object types (Player, Enemy, Boss),
/// including health deduction, destruction, and explosion effects.
/// </summary>
public class L2S2_CollisionHandler : MonoBehaviour
{
    public L2S2_HealthSystem healthSystem;
    public GameObject explosion;

     /// <summary>
    /// Called when this object enters a trigger collider.
    /// Determines interaction based on tags of both this object and the other collider.
    /// </summary>
    /// <param name="collision">The Collider2D that triggered the event</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // PLAYER LOGIC
        if (gameObject.CompareTag("Player"))
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyProjectile"))
            {
                if (healthSystem != null)
                {
                    healthSystem.TakeDamage(1);
                }
                else
                {
                    Debug.LogWarning("HealthSystem is not assigned in inspector.");
                }
                Destroy(collision.gameObject);
            }
        }

        // ENEMY LOGIC
        if (gameObject.CompareTag("Enemy"))
        {
            if (collision.CompareTag("FriendlyProjectile"))
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        //BOSS LOGIC
        if (collision.CompareTag("Boss"))
        {
            L3_Boss_Health bossHealth = collision.GetComponent<L3_Boss_Health>();
            if (bossHealth != null)
            {
                bossHealth.takeDamage();
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
