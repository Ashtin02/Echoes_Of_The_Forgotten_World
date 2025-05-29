using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public Transform[] spawnPoints;
    public float spawnInterval = 1f;

    public Transform leftBoundaryReference;
    public Transform rightBoundaryReference;

    /// <summary>
    /// Initialize the enemy spawner and start spawning enemies in waves.
    /// This method is called once at the start of the game.
    /// It begins a coroutine to spawn a specified number of enemies at defined intervals.
    /// </summary>
    void Start()
    {
        StartCoroutine(SpawnWave());
    }
    /// <summary>
    /// Spawns a wave of enemies at defined spawn points.
    /// This coroutine instantiates a specified number of enemies at random spawn points
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyMovement movement = enemyInstance.GetComponent<EnemyMovement>();
            movement.leftBoundary = leftBoundaryReference;
            movement.rightBoundary = rightBoundaryReference;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}