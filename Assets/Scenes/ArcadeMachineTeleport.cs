using System.Collections;
using UnityEngine;

public class ArcadeMachineTeleport : MonoBehaviour
{
    [SerializeField] private Transform player;          
    [SerializeField] private Transform arcadeMachine;
    [SerializeField] private float matDuration = 10f;  // How long the materializing effect takes
    [SerializeField] private string gameWorldSceneName = "ArcadeGameWorld"; // Scene to load
    [SerializeField] private ParticleSystem matEffect;  // Optional particle effect
    [SerializeField] private AudioClip matSound;        // Sound effect
    
    private AudioSource audioSource;
    private bool isMaterializing = false;
    private bool playerInputEnabled = true;
    
    /// <summary>
    /// Initialize the ArcadeMachineTeleport component.
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    /// <summary>
    /// Trigger the teleport effect when called.
    /// This can be called from a dialog or any other event.
    /// </summary>
    public void TriggerTeleport()
{
    if (!isMaterializing)
    {
        Debug.Log("Teleport triggered by dialog - starting effect");
        StartMatPlayerIntoArcade();
    }
}
    /// <summary>
    /// Starts the materializing effect to suck the player into the arcade machine.
    /// </summary>
    public void StartMatPlayerIntoArcade()
    {
        if (!isMaterializing)
            StartCoroutine(SuckPlayerIntoArcade());
    }
    /// <summary>
    /// Disables player input during the materializing effect.
    /// This prevents the player from moving or interacting while the effect is playing.
    /// </summary>
    public void DisablePlayerInput()
    {
        playerInputEnabled = false;
    }
    /// <summary>
    /// 
    /// </summary>
    public void EnablePlayerInput()
    {
        playerInputEnabled = true;
    }
    /// <summary>
    /// Coroutine that handles the materializing effect of sucking the player into the arcade machine.
    /// This includes moving the player towards the arcade machine, shrinking them, and playing sound and particle effects.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SuckPlayerIntoArcade()
    {
        isMaterializing = true;
            
        // Play sound effect
        if (matSound != null)
            audioSource.PlayOneShot(matSound);
            
        // Start particle effect
        if (matEffect != null)
            matEffect.Play();
            
        Vector3 initialScale = player.localScale;
        Vector3 initialPosition = player.position;
        
        Vector3 directionToArcade = (arcadeMachine.position - player.position).normalized;
        
        float elapsedTime = 0;
        
        // Animation loop
        while (elapsedTime < matDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / matDuration;
            
            float speedMultiplier = Mathf.Lerp(0.5f, 3f, t);
            player.position = Vector3.Lerp(initialPosition, arcadeMachine.position, t * speedMultiplier);
            
            float scaleMultiplier = Mathf.Lerp(1f, 0.1f, t);
            player.localScale = initialScale * scaleMultiplier;
            
            player.Rotate(0, 0, 360 * Time.deltaTime);
            
            yield return null;
        }
        
        StartCoroutine(FlashScreen());
        
        player.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1f);
    
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameWorldSceneName);
        
        isMaterializing = false;
    }
    /// <summary>
    /// Coroutine that creates a screen flash effect when the player is teleported.
    /// This simulates a visual effect to indicate the teleportation is happening.
    /// Creates a white texture that fades in and out over 0.5 seconds using a sine wave pattern.
    /// </summary>
    /// <returns>IEnumerator for coroutine execution, yields null each frame during the flash animation</returns>
    private IEnumerator FlashScreen()
    {
        Texture2D whiteTexture = new Texture2D(1, 1);
        whiteTexture.SetPixel(0, 0, Color.white);
        whiteTexture.Apply();
        
        // Flash effect
        float flashDuration = 0.5f;
        float elapsedTime = 0;
        
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Sin((elapsedTime / flashDuration) * Mathf.PI);
            flashAlpha = alpha;
            
            yield return null;
        }
        
        flashAlpha = 0;
    }
    /// <summary>
    /// Checks if player input is currently enabled.
    /// This is used to determine if the player can move or interact with the game world.
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerInputEnabled()
    {
        return playerInputEnabled;
    }
    
    // For the screen flash effect
    private float flashAlpha = 0;
    
    private void OnGUI()
    {
        if (flashAlpha > 0)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();
            
            GUI.color = new Color(1, 1, 1, flashAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        }
    }

}