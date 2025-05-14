using UnityEngine;

public class L2S2_CollisionHandler : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Projectile"))
        {
            GetComponent<L2S2_HealthSystem>().TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}