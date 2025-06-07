using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns power-ups at random intervals from a weighted list, making rare power-ups less likely.
/// </summary>
public class L2S2_PowerUpSpawner : MonoBehaviour
{
    public GameObject Heal;
    public GameObject Shield;
    public GameObject Piercing;
    public GameObject SpeedBoost;
    public GameObject DoubleShot;
    public GameObject TripleShot;
    public GameObject ExtraLife;
    public GameObject SlowTime;
    public GameObject Nuke;
    public GameObject Ghost;
    public GameObject Shrink;
    public GameObject Grow;
    public float spawnRate = 30f;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 9f;
    public List<GameObject> PowerUps;

    /// <summary>
    /// Initializes the power-up list and starts the spawn loop.
    /// </summary>
    private void Start()
    {
        CreateWeightedList();
        StartCoroutine(SpawnPowerUps());
    }

    /// <summary>
    /// Creates a "weighted" list of power ups (rarer the power up the less amount of entries it has)
    /// </summary>
    private void CreateWeightedList()
    {
        PowerUps = new List<GameObject>();

        AddPowerUps(Heal, 20);
        AddPowerUps(SpeedBoost, 18);
        AddPowerUps(DoubleShot, 15);
        AddPowerUps(Shield, 15);
        AddPowerUps(Piercing, 10);
        AddPowerUps(TripleShot, 10);
        AddPowerUps(Shrink, 8);
        AddPowerUps(Grow, 8);
        AddPowerUps(SlowTime, 6);
        AddPowerUps(Ghost, 4);
        AddPowerUps(ExtraLife, 3);
        AddPowerUps(Nuke, 2);
    }

    /// <summary>
    /// Function to add power ups to the list
    /// </summary>
    /// <param name="powerUp"> power up name </param>
    /// <param name="amount"> amount of times to add it to the list </param>
    private void AddPowerUps(GameObject powerUp, int amount)
    {
        for (int x = 0; x < amount; x++)
        {
            PowerUps.Add(powerUp);
        }
    }

    /// <summary>
    /// Function that will actually pick a random power up form the list and spawn in to fall from the top of the screen 
    /// </summary>
    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            GameObject powerUp = PowerUps[Random.Range(0, PowerUps.Count)];
            Vector2 SpawnPos = new Vector2(Random.Range(minX, maxX), spawnY);
            Instantiate(powerUp, SpawnPos, Quaternion.identity);
        }
    }
}