using UnityEngine;

/// <summary>
/// Controls the movement and behavior of friendly projectiles
/// Handles piercing logic and adjusts direction based on sprite orientation.
/// </summary>
public class L2S2_ProjectileMovement : MonoBehaviour
{
    public float fireSpeed = 10f;
    public bool isPiercing = false;

    /// <summary>
    /// moves projectile up each frame
    /// Note: The projectile sprite faces right, so we move right to simulate upward motion.
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector2.right * fireSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Handles collision with enemy ships.
    /// Destroys the projectile on contact unless it has the piercing power-up.
    /// </summary>
    /// <param name="collision">The collider the projectile hit</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
        }
    }
}