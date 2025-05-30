using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        
        SceneManager.LoadScene("Level 1 Scene 1");
    }
}