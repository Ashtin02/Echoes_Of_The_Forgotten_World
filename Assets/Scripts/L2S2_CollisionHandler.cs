using UnityEngine;

public class L2S2_CollisionHandler : MonoBehaviour{

    public L2S2_HealthSystem healthSystem;


    void OnTriggerEnter2D(Collider2D collision)
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
}