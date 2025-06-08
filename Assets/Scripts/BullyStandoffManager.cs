using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BullyStandoffManager : MonoBehaviour
{
    [Header("Dialog UI - Assign in Inspector")]
    public GameObject dialogBox;
    public TMP_Text dialogText;
    public Image avatarDisplay;

    [Header("Character Avatars - Assign in Inspector")]
    public Sprite willAvatar;
    public Sprite bullyMaxAvatar;
    public Sprite bullyChadAvatar;

    [Header("Scene Transition - Assign in Inspector")]
    public Animator levelLoaderAnimator;
    public string nextSceneName; 
    public float sceneTransitionWaitTime = 1.5f;
    public float preFadeDelay = 1.0f;    // Delay after dialogue ends, before fade starts

    private string[] lines = new string[]
    {
        "Well, well, Will. Fancy meeting you here, Dweebo.", // Bully Max
        "Yeah! It's Donation time. You know the drill!", // Bully Chad
        "Actually, guys, I think we need to clear up a misunderstanding.", // Will
        "The ONLY misunderstanding is why my hand's not full of your lunch money!", // Bully Max
        "Guys, Im going to have to say 'No'", // Will
        "Aww.. Did ya hear that Max, he says 'No!?!?'", // Bully Chad
        "Relying on intimidation for a few bucks? That's not strength. Honestly, it just looks a bit... sad.", // Will
        "Guys... Are you sad?? Its ok to be sad.", // Will
        "Hey, that's not... we're not sad!", // Bully Chad
        "Think about it. Is extorting lunch money your life's achievement?", // Will
        "Quite the legacy. You can't achieve more than that, can you?!?", // Will
        "(Looks at Chad, muttering) Whatever. This is boring anyway. Let's go." // Bully Max
    };

    
    // Speaker Index: 0 = Bully Max, 1 = Bully Chad, 2 = Will
    private int[] speakerIndices = new int[]
    {
        0, 
        1, 
        2, 
        0, 
        2, 
        1, 
        2, 
        2, 
        1, 
        2, 
        2, 
        0  
    };

    private int currentLine = 0;
    private bool dialogActive = false;
    private bool playerControlsDisabled = false;
    private bool isTransitioning = false; // To prevent multiple transitions

    void Start()
    {
        if (dialogBox == null || dialogText == null || avatarDisplay == null)
        {
            Debug.LogError("Essential UI elements (DialogBox, DialogText, AvatarDisplay) are not assigned!");
            this.enabled = false;
            return;
        }
        if (willAvatar == null || bullyMaxAvatar == null || bullyChadAvatar == null)
        {
            Debug.LogWarning("One or more character avatars are not assigned. Avatars may not display correctly.");
        }

        dialogBox.SetActive(false);
        if (avatarDisplay != null) avatarDisplay.gameObject.SetActive(false);
    }

    public void BeginDialogue()
    {
        if (dialogActive || isTransitioning) return;

        Debug.Log("Bully Standoff Dialogue Starting.");
        dialogActive = true;
        currentLine = 0;


        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        SetAvatarForSpeaker(speakerIndices[currentLine]);
    }

    void Update()
    {
        if (!dialogActive || isTransitioning) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            currentLine++;

            if (currentLine < lines.Length)
            {
                dialogText.text = lines[currentLine];
                SetAvatarForSpeaker(speakerIndices[currentLine]);
            }
            else
            {
                EndDialogueAndPrepareTransition();
            }
        }
    }

    void SetAvatarForSpeaker(int speakerIndex)
    {
        if (avatarDisplay == null) return;

        avatarDisplay.gameObject.SetActive(true);
        switch (speakerIndex)
        {
            case 0: 
                avatarDisplay.sprite = bullyMaxAvatar;
                break;
            case 1: 
                avatarDisplay.sprite = bullyChadAvatar;
                break;
            case 2: 
                avatarDisplay.sprite = willAvatar;
                break;
            default: // Should not happen with current setup
                avatarDisplay.sprite = null;
                avatarDisplay.gameObject.SetActive(false);
                break;
        }
    }

    void EndDialogueAndPrepareTransition()
    {
        dialogActive = false;

        Debug.Log("Dialogue finished. Preparing for scene transition.");

        if (!isTransitioning)
        {
            if (string.IsNullOrEmpty(nextSceneName))
            {
                Debug.LogWarning("Next Scene Name is not set. Cannot transition. Enabling player controls.");
            }
            else
            {
                StartCoroutine(EndSceneAndTransitionCoroutine());
            }
        }
    }

    IEnumerator EndSceneAndTransitionCoroutine()
    {
        isTransitioning = true;

        if (preFadeDelay > 0)
        {
            yield return new WaitForSeconds(preFadeDelay);
        }

        if (dialogBox != null) dialogBox.SetActive(false);
        if (avatarDisplay != null) avatarDisplay.gameObject.SetActive(false);

        if (levelLoaderAnimator != null)
        {
            levelLoaderAnimator.SetTrigger("start");
            yield return new WaitForSeconds(sceneTransitionWaitTime);
        }
        else
        {
            Debug.LogWarning("Level Loader Animator not assigned. Scene transition will lack visual fade.");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Loading scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

}