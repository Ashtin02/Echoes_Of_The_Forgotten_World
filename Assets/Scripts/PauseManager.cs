using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool paused = false;
    public GameObject PauseMenu;
    /// <summary>
    /// Checks for the Escape key press to toggle the pause state of the game.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    /// <summary>
    /// Resumes the game from a paused state.
    /// </summary>
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    /// <summary>
    /// Pauses the game, bringing up the pause menu and stopping time.
    /// </summary>
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    /// <summary>
    /// Returns to the main menu scene.
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    /// <summary>
    /// Restarts the current level.
    /// </summary>
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}