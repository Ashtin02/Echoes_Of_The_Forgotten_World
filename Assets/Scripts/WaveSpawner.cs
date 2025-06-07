using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Spawns waves of enemies in a grid pattern. After a fixed number of waves,
/// transitions to the next level with an animation.
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    public Animator transition;
    public float TransitionTime = 1f;
    public GameObject enemyShip;
    Transform enemySpawn;
    private bool defeated = false;
    public int rows = 5;
    public int columns = 7;
    public float spacing = 2.0f;
    public float waveDelay = 3f;
    private bool waveSpawning = false;
    private int waveCounter = 0;

    /// <summary>
    /// spaewns first wave at the start of the level
    /// </summary> 
    void Start()
    {
        spawnWave();
    }

    /// <summary>
    /// Spawns a grid of enemies centered at the top of the screen.
    /// Tracks how many waves have been spawned.
    /// </summary>
    private void spawnWave()
    {
        float width = spacing * (columns - 1);
        float height = spacing * (rows - 1);
        Vector2 centering = new Vector2(-width / 2, -height / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(
                    centering.x + col * spacing,
                    centering.y + row * spacing
                );

                GameObject enemy = Instantiate(enemyShip, transform);
                Vector2 topOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, .85f, 0)); // center-top of screen
                enemy.transform.position = topOfScreen + position;

            }
        }

        waveCounter++;
        waveSpawning = false;
    }

    /// <summary>
    /// Monitors wave completion. If all enemies are destroyed, spawns next wave
    /// or transitions to the next level if the max wave count is reached.
    /// </summary>
    void Update()
    {
        if (!waveSpawning && transform.childCount == 0)
        {
            waveSpawning = true;
            Invoke(nameof(spawnWave), waveDelay);
        }

        if (waveCounter == 3 && transform.childCount == 0)
        {
            LoadNextLevel();
        }
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