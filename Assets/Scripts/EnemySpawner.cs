using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public Transform[] spawnPoints;
    public float spawnInterval = 1f;

    // Scene references for boundaries:
    public Transform leftBoundaryReference;
    public Transform rightBoundaryReference;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Now the enemy prefab's movement component can be set directly.
            EnemyMovement movement = enemyInstance.GetComponent<EnemyMovement>();
            movement.leftBoundary = leftBoundaryReference;
            movement.rightBoundary = rightBoundaryReference;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}