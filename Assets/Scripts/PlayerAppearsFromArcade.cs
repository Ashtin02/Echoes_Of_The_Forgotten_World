using UnityEngine;
using System.Collections;
// Assuming your player controller might be a common type, or you'll define it
// using YourGame.PlayerController; // Example if your player controller is in a namespace

public class PlayerAppearsFromArcade : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Transform playerCharacter; // Will's character transform
    [SerializeField] private Transform arcadeMachineOrigin; // Where he appears from (e.g., screen of arcade)
    [SerializeField] private Transform landingSpot; // Where he lands next to the machine

    [Header("Effect Settings")]
    [SerializeField] private float appearDuration = 2.0f; // How long the appearing effect takes
    [SerializeField] private float initialScaleFactor = 0.1f; // How small he starts
    [SerializeField] private float spinSpeed = 720f; // Degrees per second for spinning

    [Header("Effects")]
    [SerializeField] private ParticleSystem appearParticleEffect; // Optional particle effect
    [SerializeField] private AudioClip appearSound;        // Sound effect for appearing

    // Reference to the player's movement script to disable/enable controls
    // You'll need to drag the GameObject with the player's movement script here
    // and ensure that script has public methods like DisableMovement() and EnableMovement().
    [SerializeField] private MonoBehaviour playerMovementScript; // More generic, cast later or use interface

    private AudioSource audioSource;
    private bool isAppearing = false;
    private Vector3 targetPlayerScale;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (playerCharacter == null || arcadeMachineOrigin == null || landingSpot == null)
        {
            Debug.LogError("Player, ArcadeMachineOrigin, or LandingSpot not assigned!");
            enabled = false;
            return;
        }

        targetPlayerScale = playerCharacter.localScale; // Assume player is at normal scale initially in prefab/scene
        playerCharacter.gameObject.SetActive(false); // Start with player invisible/inactive

        // Automatically trigger the appearance when the scene starts for this specific request
        StartPlayerAppearance();
    }

    public void StartPlayerAppearance()
    {
        if (!isAppearing)
        {
            StartCoroutine(AppearFromArcadeRoutine());
        }
    }

    private IEnumerator AppearFromArcadeRoutine()
    {
        isAppearing = true;

        // Disable player controls
        if (playerMovementScript != null)
        {
            // Example: (playerMovementScript as YourPlayerControllerType)?.DisableMovement();
            // Or if it's a generic MonoBehaviour, you might need to use SendMessage or an interface
            // For simplicity, let's assume a method "DisableControls" exists.
            // If your player script is e.g. "PlayerMovement":
            // PlayerMovement pm = playerMovementScript as PlayerMovement;
            // if (pm != null) pm.DisableControls(); else Debug.LogWarning("Could not cast to PlayerMovement script");
            // For now, we'll just log it. You'll need to implement this part based on your player script.
            Debug.Log("Attempting to disable player controls (implement specific call).");
             // Example using a common method name (you'd need this on your player script)
            playerMovementScript?.SendMessage("DisablePlayerInput", SendMessageOptions.DontRequireReceiver);


        }

        playerCharacter.position = arcadeMachineOrigin.position;
        playerCharacter.localScale = targetPlayerScale * initialScaleFactor;
        playerCharacter.gameObject.SetActive(true);

        if (appearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(appearSound);
        }

        if (appearParticleEffect != null)
        {
            appearParticleEffect.transform.position = arcadeMachineOrigin.position; // Or parent to player
            appearParticleEffect.Play();
        }

        float elapsedTime = 0;
        Quaternion initialRotation = playerCharacter.rotation; // Or a specific starting orientation

        while (elapsedTime < appearDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / appearDuration); // Ensure t is between 0 and 1

            playerCharacter.position = Vector3.Lerp(arcadeMachineOrigin.position, landingSpot.position, t);
            playerCharacter.localScale = Vector3.Lerp(targetPlayerScale * initialScaleFactor, targetPlayerScale, t);

            // Spin: You can adjust how it spins (e.g., slow down at the end)
            playerCharacter.Rotate(0, 0, spinSpeed * Time.deltaTime); // Spinning around Y-axis, adjust as needed

            yield return null;
        }

        playerCharacter.position = landingSpot.position; // Ensure exact final position
        playerCharacter.localScale = targetPlayerScale;   // Ensure exact final scale
        playerCharacter.rotation = landingSpot.rotation; // Ensure final orientation (align with landing spot's rotation)

        if (appearParticleEffect != null && appearParticleEffect.isPlaying)
        {
            appearParticleEffect.Stop();
        }

        // Consider a brief screen flash here if desired, using your FlashScreen coroutine
        // StartCoroutine(FlashScreen());
        // yield return new WaitForSeconds(0.5f); // If FlashScreen takes 0.5s

        // Enable player controls
        if (playerMovementScript != null)
        {
            Debug.Log("Attempting to enable player controls (implement specific call).");
            playerMovementScript?.SendMessage("EnablePlayerInput", SendMessageOptions.DontRequireReceiver);

        }

        isAppearing = false;
        Debug.Log("Player appearance complete. Player control should be enabled.");
    }

    // You can copy your FlashScreen and OnGUI methods here if you want the screen flash
    // private float flashAlpha = 0;
    // private IEnumerator FlashScreen() { ... }
    // private void OnGUI() { ... }
}