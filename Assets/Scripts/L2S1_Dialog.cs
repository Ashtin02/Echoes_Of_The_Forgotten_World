using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        "Hey, I finally made it to the ship. What's the situation?",
        "Welcome aboard! We've been waiting for you. The navigation system is malfunctioning.",
        "I've tried to fix it, but the circuitry is fried. We need replacement parts.",
        "ANALYSIS: PROBABILITY OF SUCCESSFUL REPAIR WITH CURRENT RESOURCES: 12.34%",
        "Don't be so negative! We've gotten out of worse situations before.",
        "My sensors indicate unusual energy signatures coming from the abandoned station.",
        "Maybe we could salvage what we need from there? It's risky though.",
        "I've made it through worse. Let me grab my gear and we'll head over."
    };
    
    [Header("Speaker Order")]
    public int[] speakerInd = new int[]
    {
        0, // Hero
        1, // Human Male 
        2, // Human Female
        3, // Robot 1
        2, // Human Female
        4, // Robot 2
        5, // Robot 3
        0  // Hero
    };
    
    private int currentLine = 0;
    private bool dialogActive = false;
    
    void Start()
    {
        // Start dialog
        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        dialogActive = true;
        
        // Set initial avatar based on first speaker
        SetAvatarForSpeaker(speakerInd[0]);
    }
    
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            
            if (currentLine < lines.Length)
            {
                dialogText.text = lines[currentLine];
                SetAvatarForSpeaker(speakerInd[currentLine]);
            }
            else
            {
                // End dialog
                dialogBox.SetActive(false);
                dialogActive = false;
                
                // You can add any code here that should happen when the dialog ends
                // For example, triggering another event or animation
            }
        }
    }
    
    void SetAvatarForSpeaker(int speakerIndex)
    {
        switch (speakerIndex)
        {
            case 0:
                avatar.sprite = heroAvatar;
                break;
            case 1:
                avatar.sprite = humanMaleAvatar;
                break;
            case 2:
                avatar.sprite = humanFemaleAvatar;
                break;
            case 3:
                avatar.sprite = robot1Avatar;
                break;
            case 4:
                avatar.sprite = robot2Avatar;
                break;
            case 5:
                avatar.sprite = robot3Avatar;
                break;
            default:
                avatar.sprite = null;
                break;
        }
    }
}