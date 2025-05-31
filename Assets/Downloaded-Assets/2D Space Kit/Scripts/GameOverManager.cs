using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    /// <summary>
    /// Handles the game over state by restarting the game.
    /// This method is called when the player loses all lives or fails a level.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1 Scene 1");
    }
}