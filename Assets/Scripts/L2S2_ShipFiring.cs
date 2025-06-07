using UnityEngine;

/// <summary>
/// Handles firing logic for the player ship, supporting single, double, and triple shot modes.
/// Applies projectile modifiers like piercing and manages firing rate.
/// </summary>
public class L2S2_ShipFiring : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float fireSpeed = 10f;
    public bool Piercing = false;
    public bool Double = false;
    public bool Triple = false;
    public float fireRate = 0.25f;
    public float nextFiretime = 0f;

    /// <summary>
    /// Fires a projectile when space bar is pressed and limits fire rate
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFiretime)
        {
            if (projectile == null)
            {
                Debug.LogWarning("Projectile prefab is not assigned or has been destroyed.");
                return;
            }
            else
            {
                FireProjectile();
                nextFiretime = Time.time + fireRate;
            }
        }
    }

    /// <summary>
    /// Fires projectiles based on the current shot mode (single, double, or triple).
    /// </summary>
    void FireProjectile()
    {
        if (Triple)
        {
            fireOffset(0f);     // center
            fireOffset(0.6f);   // right
            fireOffset(-0.6f);  // left
        }
        else if (Double)
        {
            fireOffset(0.6f);   // right
            fireOffset(-0.6f);  // left
        }
        else
        {
            fireOffset(0f);     // center
        }
    }

    /// <summary>
    /// Spawns a projectile at a horizontal offset from the fire point.
    /// Applies projectile movement and modifiers.
    /// </summary>
    /// <param name="offset">Horizontal offset from the center of the ship</param>
    void fireOffset(float offset)
    {
        Vector3 spawnPos = firePoint.position + new Vector3(offset, 0f, 0f);
        GameObject newProjectile = Instantiate(projectile, spawnPos, Quaternion.Euler(0, 0, 90));
        newProjectile.tag = "FriendlyProjectile";

        var movement = newProjectile.AddComponent<L2S2_ProjectileMovement>();
        movement.fireSpeed = fireSpeed;
        movement.isPiercing = Piercing;

        Destroy(newProjectile, 3f);
    }

}
