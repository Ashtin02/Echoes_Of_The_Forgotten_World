using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    public GameObject boss1;
    public GameObject boss2;
    public GameObject finalBoss;

    public Transform boss1SpawnPoint;
    public Transform boss2SpawnPoint;
    public Transform finalBossSpawnPoint;

    private bool boss1Defeated = false;
    private bool boss2Defeated = false;


    void Start()
    {
        spawnBosses();
    }

    private void spawnBosses()
    {
        GameObject MB1 = Instantiate(boss1, boss1SpawnPoint.position, Quaternion.identity);
        GameObject MB2 = Instantiate(boss2, boss2SpawnPoint.position, Quaternion.identity);

        L3_Boss_Health MB1Health = MB1.GetComponent<L3_Boss_Health>();
        L3_Boss_Health MB2Health = MB2.GetComponent<L3_Boss_Health>();

        MB1Health.onBossDefeated += () =>
        {
            boss1Defeated = true;
            SpawnFinalBoss();
        };

        MB2Health.onBossDefeated += () =>
        {
            boss2Defeated = true;

            SpawnFinalBoss();
        };

    }

    private void SpawnFinalBoss()
    {
        if (boss1Defeated && boss2Defeated)
        {
            StartCoroutine(DelayFinalSpawn());
        }
    }

    private IEnumerator DelayFinalSpawn()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(finalBoss, finalBossSpawnPoint.position, Quaternion.identity);

    }
}