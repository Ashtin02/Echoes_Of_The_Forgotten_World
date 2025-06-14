using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Required for scene management

public class L2S1_Dialog : MonoBehaviour
{
    public GameObject dialogBox;
    public TMP_Text dialogText;

    [Header("Characters")]
    public GameObject heroObject;
    public GameObject humanMaleObject;
    public GameObject humanFemaleObject;
    public GameObject robot1Object;
    public GameObject robot2Object;
    public GameObject robot3Object;

    [Header("Dialog UI")]
    public Image avatar;
    public Sprite heroAvatar;
    public Sprite humanMaleAvatar;
    public Sprite humanFemaleAvatar;
    public Sprite robot1Avatar;
    public Sprite robot2Avatar;
    public Sprite robot3Avatar;

    [Header("Dialog Content")]
    public string[] lines = new string[]
    {
        "Whoa! Where am I? How did I get here?",  // Timmy
        "Hey kid! How did you get aboard our ship?",  // Axel
        "He just... appeared out of nowhere! The teleporter wasn't even active.",  // Huntress
        "SCAN COMPLETE: SUBJECT IS HUMAN. AGE: APPROXIMATELY 10-12 EARTH YEARS.",  // Robot 1
        "This is impossible. Our shields should prevent any unauthorized teleportation.",  // Huntress
        "My sensors detect quantum displacement energy. Most unusual.",  // Robot 2
        "Perhaps he came through the arcade gateway? It was activated recently.",  // Robot 3
        "Arcade? Wait, I was just playing an arcade game and then...",  // Timmy
        "We don't have time for this! Enemy ships approaching fast!",  // Axel
        "Shields at 67% and dropping. We need to engage evasive maneuvers now.",  // Robot 2
        "Kid, hang on tight. We'll figure this out after we deal with these raiders.",  // Huntress
        "What? Raiders? Are we in... SPACE?!",  // Timmy
        "Cannons 3 and 4! Suppressing fire now!!", // Axel
    };

    public int[] speakerInd = new int[]
    {
        0, 1, 2, 3, 2, 4, 5, 0, 1, 4, 2, 0, 1
    };

    private int currentLine = 0;
    private bool dialogActive = false;

    [Header("Combat Effects")]
    public GameObject missilePrefab;
    public GameObject explosionPrefab;
    public Transform[] missileSpawnPoints;
    public GameObject playerSpaceship;
    public GameObject turretFlashObject; 

    private bool combatEffectsTriggered = false;

    [Header("Scene Transition")]
    public Animator levelLoaderAnimator;      
    public string nextSceneName;             
    public float sceneTransitionWaitTime = 1f; 
    public float preFadeDelay = 3.0f; 

    private bool isTransitioning = false;
    /// <summary>
    /// Initializes the dialog system and ensures all necessary components are assigned.
    /// </summary>
    void Start()
    {
        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        dialogActive = true;
        SetAvatarForSpeaker(speakerInd[0]);

        if (playerSpaceship == null)
        {
            Debug.LogError("Player SPACESHIP not assigned in the Inspector for L2S1_Dialog!");
        }
        if (missilePrefab == null)
        {
            Debug.LogWarning("Missile Prefab not assigned in L2S1_Dialog script.");
        }
        if (explosionPrefab == null)
        {
            Debug.LogWarning("Explosion Prefab not assigned in L2S1_Dialog script.");
        }
        if (turretFlashObject != null)
        {
            Debug.Log("L2S1_Dialog Start(): TurretFlashObject found. Initial activeSelf state: " + turretFlashObject.activeSelf);
            turretFlashObject.SetActive(false); // Ensure turret flash is off at the start
            Debug.Log("L2S1_Dialog Start(): TurretFlashObject activeSelf AFTER SetActive(false): " + turretFlashObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("L2S1_Dialog Start(): Turret Flash Object is NOT assigned in the Inspector!");
        }
        if (levelLoaderAnimator == null)
        {
            Debug.LogWarning("Level Loader Animator not assigned in L2S1_Dialog script. Scene transition may not have a visual fade.");
        }
    }
    /// <summary>
    /// Handles player input to progress dialog and manages scene transition after dialog ends.
    /// </summary>
    void Update()
    {
        if (isTransitioning) return; 

        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < lines.Length)
            {
                dialogText.text = lines[currentLine];
                SetAvatarForSpeaker(speakerInd[currentLine]);
                if (currentLine == 8 && !combatEffectsTriggered)
                {
                    TriggerCombatSequence();
                    combatEffectsTriggered = true;
                }
            }
            else
            {
                
                dialogBox.SetActive(false);
                dialogActive = false; 
                if (avatar != null) avatar.gameObject.SetActive(false);

                if (!isTransitioning) 
                {
                    StartCoroutine(EndSceneAndTransitionWithLevelLoader());
                }
            }
        }
    }
    /// <summary>
    /// Sets the avatar image based on the current speaker index.
    /// </summary>
    /// <param name="speakerIndex"></param>
    void SetAvatarForSpeaker(int speakerIndex)
    {
        switch (speakerIndex)
        {
            case 0: avatar.sprite = heroAvatar; break;
            case 1: avatar.sprite = humanMaleAvatar; break;
            case 2: avatar.sprite = humanFemaleAvatar; break;
            case 3: avatar.sprite = robot1Avatar; break;
            case 4: avatar.sprite = robot2Avatar; break;
            case 5: avatar.sprite = robot3Avatar; break;
            default: avatar.sprite = null; break;
        }
    }
    /// <summary>
    /// Triggers the combat sequence by launching missiles at the player's spaceship.
    /// </summary>
    void TriggerCombatSequence()
    {
        if (missilePrefab == null || playerSpaceship == null)
        {
            Debug.LogError("Missile Prefab or Player SPACESHIP not assigned. Cannot trigger combat sequence.");
            return;
        }
        StartCoroutine(LaunchMissilesRoutine());
    }
    /// <summary>
    /// Coroutine to launch a series of missiles at the player's spaceship with delays.
    /// </summary>
    /// <returns></returns>
    IEnumerator LaunchMissilesRoutine()
    {
        int missilesToLaunch = 5;
        float delayBetweenMissiles = 1.0f;

        for (int i = 0; i < missilesToLaunch; i++)
        {
            GameObject missileInstance;
            if (missileSpawnPoints != null && missileSpawnPoints.Length > 0)
            {
                Transform spawnPoint = missileSpawnPoints[Random.Range(0, missileSpawnPoints.Length)];
                missileInstance = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Debug.LogWarning("Missile Spawn Points are not set up. Attempting to fire from DialogController's position.");
                missileInstance = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            }

            L2S1_MissileBehavior missileScript = missileInstance.GetComponent<L2S1_MissileBehavior>();
            if (missileScript != null)
            {
                missileScript.SetTarget(playerSpaceship.transform, explosionPrefab);
            }
            else
            {
                Debug.LogError("Instantiated missile does not have L2S1_MissileBehavior script attached!");
            }

            if (i < missilesToLaunch - 1)
            {
                yield return new WaitForSeconds(delayBetweenMissiles);
            }
        }
    }
/// <summary>
/// Handles scene transition with optional pre-fade delay and fade animation.
/// </summary>
/// <returns></returns>
IEnumerator EndSceneAndTransitionWithLevelLoader()
    {
        isTransitioning = true; 

        Debug.Log("EndScene: Activating Turret Flash for continuous play.");
        Debug.Log("L2S1_Dialog EndScene: TurretFlashObject activeSelf BEFORE SetActive(true): " + turretFlashObject.activeSelf);
        turretFlashObject.SetActive(true); 
        Debug.Log("L2S1_Dialog EndScene: TurretFlashObject activeSelf AFTER SetActive(true): " + turretFlashObject.activeSelf);

        
        if (preFadeDelay > 0)
        {
            Debug.Log("EndScene: Starting pre-fade delay for: " + preFadeDelay + " seconds.");
            yield return new WaitForSeconds(preFadeDelay);
            Debug.Log("EndScene: Pre-fade delay complete.");
        }

        if (levelLoaderAnimator != null)
        {
            Debug.Log("LevelLoader Animator Name: " + levelLoaderAnimator.gameObject.name);
            Debug.Log("LevelLoader Animator Controller: " + levelLoaderAnimator.runtimeAnimatorController.name);
            bool hasStartTrigger = false;
            foreach (AnimatorControllerParameter param in levelLoaderAnimator.parameters)
            {
                if (param.name == "start" && param.type == AnimatorControllerParameterType.Trigger)
                {
                    hasStartTrigger = true;
                    break;
                }
            }
            Debug.Log("Does LevelLoader Animator have 'start' trigger parameter? " + hasStartTrigger);

            Debug.Log("EndScene: Triggering LevelLoader fade-out animation using 'start' trigger.");
            levelLoaderAnimator.SetTrigger("start"); 

            yield return new WaitForSeconds(sceneTransitionWaitTime);
            Debug.Log("EndScene: Fade-out animation presumed complete.");
        }
        else
        {
            Debug.LogWarning("LevelLoader Animator not assigned. Skipping visual fade, will attempt scene load shortly.");
            yield return new WaitForSeconds(3.0f);
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("EndScene: Loading scene - " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next Scene Name not provided in L2S1_Dialog. Cannot transition scene.");
            isTransitioning = false; 
        }
    }
}