using UnityEngine;

/// <summary>
/// Handles the downward movement of a boss projectile over time.
/// This script should be attached to projectile prefabs used by boss enemies.
/// </summary>
public class BossProjectileMovement : MonoBehaviour
{
    public float fireSpeed = 10f;

    /// <summary>
    /// Moves the projectile downward at a constant speed every frame.
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector2.down * fireSpeed * Time.deltaTime, Space.World);
    }
}