// using UnityEngine;
// using TMPro;

// public class L1S2_DialogArcade : MonoBehaviour
// {
//     [Header("Dialog UI")]
//     public GameObject dialogBox;
//     [SerializeField] TMP_Text dialogText;
    
//     [Header("Dialog Content")]
//     public string[] lines = new string[]
//     {
//         "Ooo an arcade machine!! Where did this come from?",
//         "Bzzz... (arcade turns on)",
//         "What game is this?",
//         "I'm gonna press start and see what happens!"
//     };
    
//     private int currentLine = 0;
//     private bool dialogActive = false;
//     private bool hasTriggered = false;
//     private Animator arcadeAnimator;
    
//     void Start()
//     {
//         // Get the animator component for the arcade machine animation
//         arcadeAnimator = GetComponent<Animator>();
        
//         // Make sure dialog starts hidden
//         if (dialogBox != null)
//             dialogBox.SetActive(false);
//     }
    
//     void Update()
//     {
//         if (dialogActive && Input.GetKeyDown(KeyCode.Space))
//         {
//             currentLine++;
            
//             if (currentLine < lines.Length)
//             {
//                 dialogText.text = lines[currentLine];
//             }
//             else
//             {
//                 // End dialog
//                 EndDialog();
//             }
//         }
//     }
    
//     void OnTriggerEnter2D(Collider2D other)
//     {
//         // Check if the character entered the trigger
//         if (other.name == "Character" && !hasTriggered)
//         {
//             StartDialog();
//             hasTriggered = true; // Prevent dialog from restarting
//         }
//     }
    
//     void StartDialog()
//     {
//         if (dialogBox != null && dialogText != null)
//         {
//             dialogBox.SetActive(true);
//             dialogText.text = lines[0];
//             currentLine = 0;
//             dialogActive = true;
//         }
//     }
    
//     void EndDialog()
//     {
//         dialogActive = false;
//         if (dialogBox != null)
//             dialogBox.SetActive(false);
            
//         // Trigger the arcade animation after dialog ends
//         if (arcadeAnimator != null)
//         {
//             arcadeAnimator.SetTrigger("StartAnimation"); // Replace with your actual trigger name
//         }
//     }
// }


// -------------------------------------------------------------------------------------------------------------

using UnityEngine;
using TMPro;

public class L1S2_DialogArcade : MonoBehaviour
{
    private GameObject dialogBox;
    private TextMeshProUGUI dialogText;
    private int currentLine = 0;
    private bool dialogActive = false;
    private bool hasTriggered = false;
    private ArcadeMachineTeleport teleportScript;
    
    private string[] lines = new string[]
    {
        "To start a new game press the <Spacebar>"
    };
    
    void Start()
    {
        // Find components automatically - no assignments needed
        dialogBox = GameObject.Find("DialogueBox");
        GameObject textObj = GameObject.Find("TMP (UI)");
        if (textObj != null)
            dialogText = textObj.GetComponent<TextMeshProUGUI>();
            
        teleportScript = GetComponent<ArcadeMachineTeleport>();
        
        if (dialogBox != null)
            dialogBox.SetActive(false);
    }
    
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            
            if (currentLine < lines.Length)
            {
                dialogText.text = lines[currentLine];
            }
            else
            {
                EndDialog();
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Character" && !hasTriggered)
        {
            StartDialog();
            hasTriggered = true;
        }
    }
    
    void StartDialog()
    {
        if (dialogBox != null && dialogText != null)
        {
            dialogBox.SetActive(true);
            dialogText.text = lines[0];
            currentLine = 0;
            dialogActive = true;
        }
    }
    
    void EndDialog()
    {
        dialogActive = false;
        if (dialogBox != null)
            dialogBox.SetActive(false);
            
        if (teleportScript != null)
        {
            teleportScript.TriggerTeleport();
        }
    }
}