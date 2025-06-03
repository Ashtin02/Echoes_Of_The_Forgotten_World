using UnityEngine;

public class L2S2_CollisionHandler : MonoBehaviour
{
    public L2S2_HealthSystem healthSystem;
    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Projectile hit: " + collision.name + " (tag: " + collision.tag + ")");
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
                Destroy(gameObject); // destroy this enemy object
            }
        }

        if (collision.CompareTag("Boss"))
        {
            L3_Boss_Health bossHealth = collision.GetComponent<L3_Boss_Health>(); // fix: get from the boss
            if (bossHealth != null)
            {
                bossHealth.takeDamage();
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject); // fix: destroy the projectile, not the boss
        }

    }
}
