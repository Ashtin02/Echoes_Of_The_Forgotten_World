
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls the randomized firing behavior of Mini Bosses , choosing from
/// multiple shot types with a delay and cooldown system.
/// </summary>
public class L3_MiniBoss1_Firing : MonoBehaviour
{
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public float fireRate = 5;
    public float bulletSpeed;
    public float nextFireTime;
    public Transform firePoint;
    private bool canFire;

    /// <summary>
    /// Delays initial firing so the boss doesn't shoot immediately on spawn.
    /// </summary>
    private void Start()
    {
        StartCoroutine(firingDelay());
    }

    /// <summary>
    /// fires a random shot every 5 seconds (fire rate)
    /// </summary>
    void Update()
    {
        if (canFire)
        {
            int randomNum = UnityEngine.Random.Range(1, 4);
            if (Time.time >= nextFireTime && randomNum == 1)
            {
                FireShot1();
                nextFireTime = Time.time + fireRate;
            }
            else if (Time.time >= nextFireTime && randomNum == 2)
            {
                FireShot2();
                nextFireTime = Time.time + fireRate;
            }
            else if (Time.time >= nextFireTime && randomNum == 3)
            {
                FireShot3();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    /// <summary>
    /// Fires shot type 1 in a burst pattern.
    /// </summary>
    public void FireShot1()
    {
        StartCoroutine(shot1Burst());
    }

    /// <summary>
    /// Fires a single projectile of shot type 2.
    /// </summary>>
    public void FireShot2()
    {
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot2, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
    }

    /// <summary>
    /// Fires a single projectile of shot type 3.
    /// </summary>
    public void FireShot3()
    {
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot3, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
    }

    /// <summary>
    /// Coroutine that fires a short burst of shot1 projectiles.
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
    /// Waits for 2 seconds before enabling the boss to fire.
    /// </summary>
    private IEnumerator firingDelay()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
    }
}