using System.Collections;
using Unity.Mathematics;
using UnityEngine;

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


    void Start()
    {
        StartCoroutine(firingDelay());
    }
    void Update()
    {

        if (canFire)
        {
            int randomNum = UnityEngine.Random.Range(1, 6);
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
            else if (Time.time >= nextFireTime && randomNum == 4)
            {
                FireShot4();
                nextFireTime = Time.time + fireRate;
            }
            else if (Time.time >= nextFireTime && randomNum == 5)
            {
                FireShot5();
                nextFireTime = Time.time + fireRate;
            }
        }

    }

    public void FireShot1()
    {
        StartCoroutine(shot1Burst());
    }

    public void FireShot2()
    {
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot2, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
    }

    public void FireShot3()
    {
        fireOffset(1f);
        fireOffset(-1f);
    }

    public void FireShot4()
    {
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot4, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
    }

    public void FireShot5()
    {
        StartCoroutine(fireLaser());
    }



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

    private IEnumerator fireLaser()
    {
        Vector2 spawnpos = new Vector2(firePoint.position.x, firePoint.position.y - 5);
        GameObject laser = Instantiate(shot5, spawnpos, Quaternion.identity);
        laser.tag = "EnemyProjectile";

        laser.transform.SetParent(firePoint);

        yield return new WaitForSeconds(2f);

        Destroy(laser);
    }

    private IEnumerator firingDelay()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
    }

    void fireOffset(float offset)
    {
        Vector3 spawnPos = firePoint.position + new Vector3(offset, 0f, 0f);
        GameObject newProjectile = Instantiate(shot3, spawnPos, Quaternion.identity);
        newProjectile.tag = "EnemyProjectile";

        // Add and configure projectile movement
        var movement = newProjectile.AddComponent<BossProjectileMovement>();
        movement.fireSpeed = bulletSpeed;

        Destroy(newProjectile, 3f);
    }
}