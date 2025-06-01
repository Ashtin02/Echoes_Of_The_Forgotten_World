using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Required for scene management

public class PostBossDialogueManager : MonoBehaviour
{
    public GameObject dialogBox;
    public TMP_Text dialogText;

    [Header("Characters - Assign in Inspector")]
    // Optional: GameObjects if you want to highlight characters in scene.
    // For this dialogue, avatars are likely sufficient.
    // public GameObject heroObject; // Will
    // public GameObject captainObject; // Spaceship Captain
    // public GameObject arcadeMasterVoiceSource; // Or an object representing AM

    [Header("Dialog UI - Assign in Inspector")]
    public Image avatarDisplay; // Renamed from 'avatar' for clarity
    public Sprite heroAvatar;         // Will's avatar
    public Sprite captainAvatar;      // Spaceship Captain's avatar
    public Sprite arcadeMasterAvatar; // Arcade Master's avatar

    [Header("Dialog Content")]
    public string[] lines = new string[]
    {
        // Arcade Master (AM)
        "No! Systems critical! Hull breach! I surrender! Don't shoot!",
        // Captain
        "Surrender? After this chaos? We're gonna turn you to stardust for what you did, Arcade Master!!",
        // AM
        "Please! Not like this! I... I was just lonely. Wanted to share my worlds.",
        // Captain
        "Lonely? That excuses destroying planets?",
        // Will (Hero)
        "Captain, wait! Mr. Arcade Master... you just wanted to share? Is that why you trapped people through the arcade transporters?",
        // AM
        "Yes, young one! My worlds are wonders! But no one stayed. I had to make them see!",
        // Will (Hero)
        "But forcing people isn't sharing. At home, arcades are for fun.",
        "People visit, then go home. What if your worlds were like that? Places people *choose* to visit?",
        // AM
        "Choose? They could... come and go? Would they... return?",
        // Will (Hero)
        "If it's fun and not a trap, sure! You'd have willing visitors, not prisoners. No need to destroy.",
        // AM
        "Invitations... not prisons. Visitors... by choice.",
        "All this destruction... to avoid being alone. I was so wrong.",
        "Will... you have shown me a better way. A new code. I'll send you back. To your pizza shop.",
        "The machine there... it'll be a true portal. For adventure, not a trap.",
        // Captain
        "Well, I'll be. The kid talked him down. Alright, Arcade Master. Prove it. Send him home. Safely."
    };

    // Speaker Index: 0 = Arcade Master, 1 = Captain, 2 = Will (Hero)
    public int[] speakerIndices = new int[]
    {
        0, // AM
        1, // Captain
        0, // AM
        1, // Captain
        2, // Will
        0, // AM
        2, // Will
        2, // Will (continuation)
        0, // AM
        2, // Will
        0, // AM
        0, // AM (continuation)
        0, // AM (continuation)
        0, // AM (continuation)
        1  // Captain
    };

    private int currentLine = 0;
    private bool dialogActive = false;

    [Header("Scene Transition - Assign in Inspector")]
    public Animator levelLoaderAnimator;
    public string nextSceneName; // e.g., "PizzaShopReturnScene" or "EpilogueScene"
    public float sceneTransitionWaitTime = 1.5f; // Duration of LevelLoader's fade-out
    public float preFadeDelay = 1.0f; // Delay after dialogue ends, before fade starts

    private bool isTransitioning = false;

    void Start()
    {
        if (dialogBox == null || dialogText == null || avatarDisplay == null)
        {
            Debug.LogError("Essential UI elements (DialogBox, DialogText, AvatarDisplay) are not assigned!");
            this.enabled = false; // Disable script if setup is incomplete
            return;
        }

        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        dialogActive = true;
        SetAvatarForSpeaker(speakerIndices[0]);

        if (levelLoaderAnimator == null)
        {
            Debug.LogWarning("Level Loader Animator not assigned. Scene transition may not have a visual fade.");
        }
         if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("Next Scene Name is not set. Scene transition will not occur.");
        }
    }

    void Update()
    {
        if (isTransitioning) return;

        if (dialogActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) // Allow Space or Mouse Click
        {
            currentLine++;

            if (currentLine < lines.Length)
            {
                dialogText.text = lines[currentLine];
                SetAvatarForSpeaker(speakerIndices[currentLine]);
            }
            else
            {
                dialogBox.SetActive(false);
                dialogActive = false;
                if (avatarDisplay != null) avatarDisplay.gameObject.SetActive(false);

                if (!isTransitioning && !string.IsNullOrEmpty(nextSceneName))
                {
                    StartCoroutine(EndSceneAndTransition());
                }
                else if (string.IsNullOrEmpty(nextSceneName))
                {
                     Debug.Log("Dialogue finished. No next scene specified.");
                     // Optionally, disable this script or hide the dialogue manager GameObject
                     // this.enabled = false;
                }
            }
        }
    }

    void SetAvatarForSpeaker(int speakerIndex)
    {
        if (avatarDisplay == null) return;

        avatarDisplay.gameObject.SetActive(true); // Ensure avatar display is active
        switch (speakerIndex)
        {
            case 0: // Arcade Master
                avatarDisplay.sprite = arcadeMasterAvatar;
                break;
            case 1: // Captain
                avatarDisplay.sprite = captainAvatar;
                break;
            case 2: // Will (Hero)
                avatarDisplay.sprite = heroAvatar;
                break;
            default:
                avatarDisplay.sprite = null; // Or a default "unknown" sprite
                avatarDisplay.gameObject.SetActive(false); // Hide if no specific avatar
                break;
        }
    }

    IEnumerator EndSceneAndTransition()
    {
        isTransitioning = true;

        if (preFadeDelay > 0)
        {
            yield return new WaitForSeconds(preFadeDelay);
        }

        if (levelLoaderAnimator != null)
        {
            levelLoaderAnimator.SetTrigger("start"); // Ensure your animator has a "start" trigger
            yield return new WaitForSeconds(sceneTransitionWaitTime);
        }
        else
        {
            // Fallback if no animator, wait a bit before direct load
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("EndScene: Loading scene - " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}