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
        0, // Timmy
        1, // Axel
        2, // Huntress
        3, // Robot 1
        2, // Huntress
        4, // Robot 2
        5, // Robot 3
        0, // Timmy
        1, // Axel
        4, // Robot 2
        2, // Huntress
        0, // Timmy
        1, // Axel
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
                avatar.gameObject.SetActive(false);
                
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