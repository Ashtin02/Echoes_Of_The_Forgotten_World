using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance { get; private set; }

    public string lastGameplaySceneName { get; private set; }
    
    /// <summary>
    /// Singleton pattern to ensure only one instance of SceneTracker exists.
    /// This method is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Subscribes to the sceneLoaded event when the script is enabled.
    /// This method is called when the script instance is being enabled.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    /// <summary>
    /// Unsubscribes from the sceneLoaded event when the script is disabled.
    /// This method is called when the script instance is being disabled.
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    /// <summary>
    /// Handles the scene loaded event.
    /// This method is called whenever a new scene is loaded.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu" && scene.name != "GameOver" && scene.name != "LoadingScreen")
        {
            lastGameplaySceneName = scene.name;
            Debug.Log($"SceneTracker: Stored last gameplay scene as: {lastGameplaySceneName}");
        }
        else
        {
            Debug.Log($"SceneTracker: Loaded non-gameplay scene: {scene.name}. Not storing as last gameplay scene.");
        }
    }
}