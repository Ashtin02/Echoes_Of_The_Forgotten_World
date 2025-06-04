
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

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


    private void Start()
    {
        StartCoroutine(firingDelay());
    }

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
        Vector2 spawnPos = firePoint.position;
        GameObject newshot = Instantiate(shot3, spawnPos, quaternion.identity);
        newshot.tag = "EnemyProjectile";

        Destroy(newshot, 3f);
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

    private IEnumerator firingDelay()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
    }
}