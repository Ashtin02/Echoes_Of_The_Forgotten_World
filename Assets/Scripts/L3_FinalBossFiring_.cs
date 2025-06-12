using System.Collections;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Controls the firing patterns of the final boss, randomly selecting between
/// 5 unique projectile attacks with delay and fire rate management.
/// </summary>
public class L3_FinalBossFiring_ : MonoBehaviour
{
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public GameObject shot5;
    public float fireRate = 2;
    public float bulletSpeed;
    public float nextFireTime;
    public Transform firePoint;
    private bool canFire;

    /// <summary>
    /// Delays initial firing after spawn to give the boss a short buffer.
    /// </summary>
    void Start()
    {
        StartCoroutine(firingDelay());
    }

    /// <summary>
    /// handles the firing of the ship randomly shooting 1 of 5 different kinds of shots 
    /// </summary>
void Update()
{
    if (canFire && Time.time >= nextFireTime)
    {
        Debug.Log("Firing from: " + gameObject.name); // See how many fire logs appear per interval

        int randomNum = UnityEngine.Random.Range(1, 6);
        nextFireTime = Time.time + fireRate;

        switch (randomNum)
        {
            case 1:
                FireShot1();
                break;
            case 2:
                FireShot2();
                break;
            case 3:
                FireShot3();
                break;
            case 4:
                FireShot4();
                break;
            case 5:
                FireShot5();
                break;
        }
    }
}


    /// <summary>
    /// Fires a burst of shot1 projectiles.
    /// </summary>
    public void FireShot1()
    {
        StartCoroutine(shot1Burst());
    }

    /// <summary>
    /// Fires a single shot2 projectile.
    /// </summary>
    public void FireShot2()
    {
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot2, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
    }

    /// <summary>
    /// Fires two shot3 projectiles with horizontal offset.
    /// </summary>
    public void FireShot3()
    {
        fireOffset(1f);
        fireOffset(-1f);
    }

    /// <summary>
    /// Fires a single shot4 projectile.
    /// </summary>
    public void FireShot4()
    {
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot4, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
    }

    /// <summary>
    /// Fires a laser beam that stays attached to the boss temporarily.
    /// </summary>
    public void FireShot5()
    {
        StartCoroutine(fireLaser());
    }

    /// <summary>
    /// Repeatedly fires shot1 projectiles in a burst pattern.
    /// </summary>
    private IEnumerator shot1Burst()
    {
        int bulletCount = 4;
        float bulletRate = 0.1f;
        Vector2 shotSpawn = firePoint.position;

        for (int x = 0; x < bulletCount; x++)
        {
            Vector2 spawnPos = firePoint.position;
            GameObject newshot = Instantiate(shot1, shotSpawn, Quaternion.identity);
            newshot.tag = "EnemyProjectile";

            Destroy(newshot, 3f);
            yield return new WaitForSeconds(bulletRate);
        }
    }

    /// <summary>
    /// Fires a laser that stays attached to the boss for 2 seconds.
    /// </summary>
    private IEnumerator fireLaser()
    {
        Vector2 spawnpos = new Vector2(firePoint.position.x, firePoint.position.y - 5);
        GameObject laser = Instantiate(shot5, spawnpos, Quaternion.identity);
        laser.tag = "EnemyProjectile";
        laser.transform.SetParent(firePoint);

        yield return new WaitForSeconds(2f);
        Destroy(laser);
    }

    /// <summary>
    /// Waits before enabling boss firing logic.
    /// </summary>
    private IEnumerator firingDelay()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
    }

    /// <summary>
    /// Instantiates a projectile at a horizontal offset from the fire point.
    /// </summary>
    /// <param name="offset">Offset from the center of the boss</param>
    void fireOffset(float offset)
    {
        Vector3 spawnPos = firePoint.position + new Vector3(offset, 0f, 0f);
        GameObject newProjectile = Instantiate(shot3, spawnPos, Quaternion.identity);
        newProjectile.tag = "EnemyProjectile";

        var movement = newProjectile.AddComponent<BossProjectileMovement>();
        movement.fireSpeed = bulletSpeed;

        Destroy(newProjectile, 3f);
    }
}