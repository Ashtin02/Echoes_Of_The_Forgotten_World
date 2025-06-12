using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the spawning of two mini bosses at level start and spawns the final boss after both are defeated.
/// </summary>
public class BossSpawner : MonoBehaviour
{
    public Animator transition;
    public float TransitionTime = 1f;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject finalBoss;
    public Transform boss1SpawnPoint;
    public Transform boss2SpawnPoint;
    public Transform finalBossSpawnPoint;
    private bool boss1Defeated = false;
    private bool boss2Defeated = false;

    /// <summary>
    /// Automatically spawns the mini bosses when the level starts.
    /// </summary>
    void Start()
    {
        spawnBosses();
    }

    /// <summary>
    /// Instantiates both mini bosses at their designated spawn points and sets up listeners
    /// to track when each is defeated. Once both are down, triggers the final boss spawn.
    /// </summary>
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

    /// <summary>
    /// Spawns final boss after a small delay 
    /// </summary>
    private void SpawnFinalBoss()
    {
        if (boss1Defeated && boss2Defeated)
        {
            StartCoroutine(DelayFinalSpawn());
        }
    }

    /// <summary>
    /// Used for delay of spawning
    /// </summary>
    /// <returns>  Waits for 2 seconds then spawns boss </returns>
    private IEnumerator DelayFinalSpawn()
    {
        yield return new WaitForSeconds(2f);
        GameObject FB = Instantiate(finalBoss, finalBossSpawnPoint.position, Quaternion.identity);
        L3_Boss_Health FBHealth = FB.GetComponent<L3_Boss_Health>();

        FBHealth.onBossDefeated += () =>
        {
            LoadNextLevel();
        };

    }

        /// <summary>
    /// Starts the transition to the next level.
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Plays the transition animation, waits, then loads the specified scene.
    /// </summary>
    /// <param name="levelIndex">The index of the level to load</param>
    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(LevelIndex);
    }
}