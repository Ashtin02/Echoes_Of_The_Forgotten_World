using UnityEngine;
using System.Collections;


public class PlayerAppearsFromArcade : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Transform playerCharacter; 
    [SerializeField] private Transform arcadeMachineOrigin; 
    [SerializeField] private Transform landingSpot; 

    [Header("Effect Settings")]
    [SerializeField] private float appearDuration = 2.0f; 
    [SerializeField] private float initialScaleFactor = 0.1f;
    [SerializeField] private float spinSpeed = 720f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem appearParticleEffect; 
    [SerializeField] private AudioClip appearSound;        
    [SerializeField] private MonoBehaviour playerMovementScript; 

    private AudioSource audioSource;
    private bool isAppearing = false;
    private Vector3 targetPlayerScale;
    /// <summary>
    /// Initializes the script, setting up references and starting the appearance sequence.
    /// </summary>
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

        targetPlayerScale = playerCharacter.localScale;
        playerCharacter.gameObject.SetActive(false); 

        
        StartPlayerAppearance();
    }
    /// <summary>
    /// Begins the player appearance sequence if not already in progress.
    /// </summary>
    public void StartPlayerAppearance()
    {
        if (!isAppearing)
        {
            StartCoroutine(AppearFromArcadeRoutine());
        }
    }
    /// <summary>
    /// Coroutine that handles the player's appearance from the arcade machine with effects and movement.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AppearFromArcadeRoutine()
    {
        isAppearing = true;

        if (playerMovementScript != null)
        {
            Debug.Log("Attempting to disable player controls (implement specific call).");
             
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
            appearParticleEffect.transform.position = arcadeMachineOrigin.position; 
            appearParticleEffect.Play();
        }

        float elapsedTime = 0;
        Quaternion initialRotation = playerCharacter.rotation; 

        while (elapsedTime < appearDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / appearDuration); 

            playerCharacter.position = Vector3.Lerp(arcadeMachineOrigin.position, landingSpot.position, t);
            playerCharacter.localScale = Vector3.Lerp(targetPlayerScale * initialScaleFactor, targetPlayerScale, t);

            
            playerCharacter.Rotate(0, 0, spinSpeed * Time.deltaTime); 

            yield return null;
        }

        playerCharacter.position = landingSpot.position; 
        playerCharacter.localScale = targetPlayerScale;  
        playerCharacter.rotation = landingSpot.rotation; 

        if (appearParticleEffect != null && appearParticleEffect.isPlaying)
        {
            appearParticleEffect.Stop();
        }

        if (playerMovementScript != null)
        {
            Debug.Log("Attempting to enable player controls (implement specific call).");
            playerMovementScript?.SendMessage("EnablePlayerInput", SendMessageOptions.DontRequireReceiver);

        }

        isAppearing = false;
        Debug.Log("Player appearance complete. Player control should be enabled.");
    }
}