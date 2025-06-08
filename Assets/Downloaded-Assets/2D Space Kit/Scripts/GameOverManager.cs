using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    /// <summary>
    /// Handles the game restart state by loading the first level scene.
    /// This method is called when the player chooses to restart the game.
    /// </summary>
    public void RestartGame()
    {
        Debug.Log("Restarting game to Level 1 Scene 1.");
        SceneManager.LoadScene("Level 1 Scene 1");
    }

    /// <summary>
    /// Handles the game continuation state by loading the last gameplay scene.
    /// This method is called when the player chooses to continue from a saved state.
    /// It checks if the SceneTracker instance exists and has a stored scene name.
    /// If it does, it loads that scene; otherwise, it defaults to "Level 1 Scene 1".
    /// </summary>
    public void ContinueGame()
    {
        if (SceneTracker.Instance != null && !string.IsNullOrEmpty(SceneTracker.Instance.lastGameplaySceneName))
        {
            string sceneToLoad = SceneTracker.Instance.lastGameplaySceneName;
            Debug.Log($"Continuing game. Loading last gameplay scene: {sceneToLoad}");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {                      
            SceneManager.LoadScene("Level 1 Scene 1");
        }
    }
}