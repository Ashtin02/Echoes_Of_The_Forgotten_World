using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// Starts the game by loading the first level scene.
    /// </summary>
    public void StartGame()
    {

        SceneManager.LoadScene("Level 1 Scene 1");
    }
}