using UnityEngine;

public class L2S2_ShipFiring : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float fireSpeed = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        // Instantiate the projectile at the fire point
        GameObject newProjectile = Instantiate(projectile, firePoint.position, Quaternion.Euler(0, 0, 90));

        // Optionally, destroy the projectile after a set time (e.g., 2 seconds)
        Destroy(newProjectile, 3f);

        newProjectile.AddComponent<L2S2_ProjectileMovement>().fireSpeed = fireSpeed;    }

}
