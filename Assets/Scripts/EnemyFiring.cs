using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    public float minFireInterval = .1f;
    public float maxFireInterval = .5f;
    
    private float nextFireTime;
    
    void Start()
    {
        if (missileSpawnPoint == null)
    {
        missileSpawnPoint = transform.Find("MissileSpawnPoint");
    }
        SetNextFireTime();
    }
    
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireMissile();
            SetNextFireTime();
        }
    }
    
    void FireMissile()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
    }
    
    void SetNextFireTime()
    {
        float randomInterval = Random.Range(minFireInterval, maxFireInterval);
        nextFireTime = Time.time + randomInterval;
    }
}