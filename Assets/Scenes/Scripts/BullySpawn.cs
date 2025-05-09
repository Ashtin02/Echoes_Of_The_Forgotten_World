using System.Collections;
using UnityEngine;

public class BullySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs; // Assign your character prefabs here
    [SerializeField] private float delayBeforeSpawn = 3.0f; // Time after scene load before characters appear
    [SerializeField] private float timeBetweenCharacters = 0.5f; // Time between each character spawning
    [SerializeField] private Transform spawnPoint; // The door position

    private void Start()
    {
        StartCoroutine(SpawnCharactersSequence());
    }

    private IEnumerator SpawnCharactersSequence()
    {
        // Wait for initial delay after player enters
        yield return new WaitForSeconds(delayBeforeSpawn);

        // Spawn each character with a delay between them
        foreach (GameObject characterPrefab in characterPrefabs)
        {
            Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}