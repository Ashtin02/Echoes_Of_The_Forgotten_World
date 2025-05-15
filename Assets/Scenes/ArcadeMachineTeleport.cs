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
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    
    public void StartMatPlayerIntoArcade()
    {
        if (!isMaterializing)
            StartCoroutine(SuckPlayerIntoArcade());
    }
    
    public void DisablePlayerInput()
    {
        playerInputEnabled = false;
    }

    public void EnablePlayerInput()
    {
        playerInputEnabled = true;
    }
    
    private IEnumerator SuckPlayerIntoArcade()
    {
        isMaterializing = true;
        
        DisablePlayerInput();
            
        // Play sound effect
        if (matSound != null)
            audioSource.PlayOneShot(matSound);
            
        // Start particle effect if assigned
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
            
            // Move player toward arcade machine with increasing speed
            float speedMultiplier = Mathf.Lerp(0.5f, 3f, t);
            player.position = Vector3.Lerp(initialPosition, arcadeMachine.position, t * speedMultiplier);
            
            // Shrink player as they approach the machine
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
    
    private IEnumerator FlashScreen()
    {
        // Create a white texture to cover the screen
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

    private void OnTriggerEnter2D(Collider2D other)
{
    // Automatically start the effect when the character enters the trigger area
    if (other.gameObject.name == "Character" && !isMaterializing)
    {
        Debug.Log("Character entered arcade machine area - starting effect");
        StartMatPlayerIntoArcade();
    }
}
}