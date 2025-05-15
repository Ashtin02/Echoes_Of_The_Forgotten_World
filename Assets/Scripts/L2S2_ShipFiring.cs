using UnityEngine;

public class L2S2_ShipFiring : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float fireSpeed = 10f;
    public bool Piercing = false;

    public bool Double = false;

    public bool Triple = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

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
    void fireOffset(float offset)
    {
        Vector3 spawnPos = firePoint.position + new Vector3(offset, 0f, 0f);
        GameObject newProjectile = Instantiate(projectile, spawnPos, Quaternion.Euler(0, 0, 90));
        newProjectile.tag = "FriendlyProjectile";

        // Add and configure projectile movement
        var movement = newProjectile.AddComponent<L2S2_ProjectileMovement>();
        movement.fireSpeed = fireSpeed;
        movement.isPiercing = Piercing;

        Destroy(newProjectile, 3f);
    }

}
