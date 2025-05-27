using UnityEngine;
using TMPro;

public class L1S2_DialogArcade : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject whiteScreenOverlay;
    
    [Header("Dialog Content")]
    private string[] lines = new string[]
    {
        "Ooo an arcade machine!! Where did this come from?",
        "Bzzz... (arcade turns on)",
        "What game is this?",
        "I'm gonna press <spacebar> and see what happens!"
    };

    private int currentLine = 0;
    private bool dialogActive = false;
    private bool hasTriggered = false;
    private ArcadeMachineTeleport teleportScript;

    /// <summary>
    /// Initializes the dialog system and finds necessary components.
    /// Ensures the dialog box and text are set up correctly.
    /// Also initializes the ArcadeMachineTeleport script.
    /// </summary>
    void Start()
    {
        if (dialogBox == null) dialogBox = GameObject.Find("DialogueBox");
        if (dialogText == null) 
        {
            GameObject textObj = GameObject.Find("TMP (UI)");
            if (textObj != null) dialogText = textObj.GetComponent<TextMeshProUGUI>();
        }

        teleportScript = GetComponent<ArcadeMachineTeleport>(); 

        // Start with everything hidden
        if (dialogBox != null)
            dialogBox.SetActive(false);
        
        // Ensure the overlay starts hidden
        if (whiteScreenOverlay != null)
            whiteScreenOverlay.SetActive(false);
        
        Debug.Log("Start: WhiteScreenOverlay initial state: " + (whiteScreenOverlay != null ? whiteScreenOverlay.activeSelf.ToString() : "NULL"));
    }
    
    /// <summary>
    /// Checks for player input to advance dialog lines.
    /// If the dialog is active and the spacebar is pressed, it advances to the next line.
    /// </summary>
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            
            if (currentLine < lines.Length)
            {
                if (dialogText != null)
                    dialogText.text = lines[currentLine];
            }
            else
            {
                EndDialog();
            }
        }
    }
    
    /// <summary>
    /// Detects when the player character enters the trigger area.
    /// If the character is detected and the dialog hasn't been triggered yet, it starts the dialog.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Character" && !hasTriggered)
        {
            StartDialog();
            hasTriggered = true;
        }
    }

    /// <summary>
    /// Starts the dialog by activating the dialog box and displaying the first line.
    /// Also activates the white screen overlay to create the illusion of the arcade machine turning on.
    /// </summary>
    void StartDialog()
    {
        if (dialogBox != null && dialogText != null)
        {
            dialogBox.SetActive(true);
            dialogText.text = lines[0];
            currentLine = 0;
            dialogActive = true;
        }
        
        // Activates the white screen overlay
        if (whiteScreenOverlay != null)
        {
            whiteScreenOverlay.SetActive(true);
            Debug.Log("StartDialog: WhiteScreenOverlay activated.");
        } else {
            Debug.LogError("StartDialog: whiteScreenOverlay is NULL! Please assign it in the Inspector.");
        }
    }

    /// <summary>
    /// Ends the dialog by deactivating the dialog box and the white screen overlay.
    /// Also triggers the teleport effect to suck the player into the arcade machine.
    /// </summary>
    void EndDialog()
    {
        Debug.Log("EndDialog called");
        dialogActive = false;
        
        if (dialogBox != null)
            dialogBox.SetActive(false);
        
        if (teleportScript != null)
        {
            teleportScript.TriggerTeleport();
        }
    }
}
