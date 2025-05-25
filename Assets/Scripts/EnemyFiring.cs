using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    public float minFireInterval = .1f;
    public float maxFireInterval = .5f;
    
    private float nextFireTime;
    /// <summary>
    /// Initialize the enemy firing system.
    /// This method sets the missile spawn point and calculates the next fire time.
    /// </summary>
    void Start()
    {
        if (missileSpawnPoint == null)
    {
        missileSpawnPoint = transform.Find("MissileSpawnPoint");
    }
        SetNextFireTime();
    }
    /// <summary>
    /// Update is called once per frame.
    /// This method checks if it's time to fire a missile and fires one if so.
    /// </summary>
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireMissile();
            SetNextFireTime();
        }
    }
    /// <summary>
    /// Fires a missile from the enemy's missile spawn point.
    /// This method instantiates a missile prefab at the specified spawn point.
    /// </summary>
    void FireMissile()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, Quaternion.identity);
    }
    /// <summary>
    /// Sets the next fire time for the enemy's missile.
    /// This method calculates a random interval between the minimum and maximum fire intervals
    /// </summary>
    void SetNextFireTime()
    {
        float randomInterval = Random.Range(minFireInterval, maxFireInterval);
        nextFireTime = Time.time + randomInterval;
    }
}