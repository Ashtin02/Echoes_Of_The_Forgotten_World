using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 
public class PostBossDialogueManager : MonoBehaviour
{
    public GameObject dialogBox;
    public TMP_Text dialogText;

    [Header("Characters - Assign in Inspector")]
    
    [Header("Dialog UI - Assign in Inspector")]
    public Image avatarDisplay; 
    public Sprite heroAvatar;         
    public Sprite captainAvatar;     
    public Sprite arcadeMasterAvatar; 

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
    public string nextSceneName;
    public float sceneTransitionWaitTime = 1.5f;
    public float preFadeDelay = 1.0f;

    private bool isTransitioning = false;
    /// <summary>
    /// Initializes the dialogue system and ensures all necessary components are assigned.
    /// </summary>
    void Start()
    {
        if (dialogBox == null || dialogText == null || avatarDisplay == null)
        {
            Debug.LogError("Essential UI elements (DialogBox, DialogText, AvatarDisplay) are not assigned!");
            this.enabled = false; 
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
    /// <summary>
    /// Handles player input to progress dialog and manages scene transition after dialog ends.
    /// </summary>
    void Update()
    {
        if (isTransitioning) return;

        if (dialogActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) 
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
        if (avatarDisplay == null) return;

        avatarDisplay.gameObject.SetActive(true); 
        switch (speakerIndex)
        {
            case 0: // Arcade Master
                avatarDisplay.sprite = arcadeMasterAvatar;
                break;
            case 1: // Captain
                avatarDisplay.sprite = captainAvatar;
                break;
            case 2: // Will
                avatarDisplay.sprite = heroAvatar;
                break;
            default:
                avatarDisplay.sprite = null; 
                avatarDisplay.gameObject.SetActive(false); 
                break;
        }
    }
    /// <summary>
    /// Handles the end-of-scene transition with optional delays and fade effects.
    /// </summary>
    /// <returns></returns>
    IEnumerator EndSceneAndTransition()
    {
        isTransitioning = true;

        if (preFadeDelay > 0)
        {
            yield return new WaitForSeconds(preFadeDelay);
        }

        if (levelLoaderAnimator != null)
        {
            levelLoaderAnimator.SetTrigger("start");
            yield return new WaitForSeconds(sceneTransitionWaitTime);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("EndScene: Loading scene - " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}