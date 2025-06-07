using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages automatic dialog playback, including avatar switching and enabling movement after dialog ends.
/// </summary>
public class AutoDialogPlayer : MonoBehaviour
{
    public GameObject dialogBox; //Reference to the Dialog Box
    public TMP_Text dialogText;   //Reference to the Dialog Text
    public string[] lines; //lines in dialog
    public GameObject playerObject; // Reference to the player object
    public GameObject BullyObject1; //reference to bully sprite 1
    public GameObject BullyObject2; //reference to bully sprite 2
    private BullyMovement bullyMovement1; //bully 1 movement reference 
    private BullyMovement bullyMovement2; //bully 2 movement reference 
    public Image avatar; //current speaker
    public Sprite[] avatars; //all speakers in the dialog
    public int[] speakerInd; // list telling which avatars to show in what order
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private int currentLine = 0; //line we are on
    private bool dialogActive = false; //dialog on off basically
    
    void Start()
    {
        // Ensure playerObject is assigned
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.canMove = false; // Stop player movement
        }
        // Make sure bullies are assigned
        if (BullyObject1 != null && BullyObject2 != null)
        {
            bullyMovement1 = BullyObject1.GetComponent<BullyMovement>();
            bullyMovement2 = BullyObject2.GetComponent<BullyMovement>();
        }
        // Start the dialog
        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        dialogActive = true;
        //Make sure sprite avatars and indices are assigned correctly
        if (speakerInd.Length > 0 && speakerInd[0] < avatars.Length)
        {
            //set firrst avatar
            avatar.sprite = avatars[speakerInd[0]];
        }
        else
        {
            avatar.sprite = null;
            avatar.color = new Color(1, 1, 1, 0);
        }
    }

    void Update()
    {
        // moves dialog line by line if space is hit
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            //changing the line and the avatar
            if (currentLine < lines.Length)
            {
                dialogText.text = lines[currentLine];
                if (currentLine < speakerInd.Length && speakerInd[currentLine] < avatars.Length)
                {
                    avatar.sprite = avatars[speakerInd[currentLine]];
                }
            }
            else
            {
                //End dialog
                dialogBox.SetActive(false);
                dialogActive = false;
                avatar.sprite = null;
                avatar.color = new Color(1, 1, 1, 0);

                // Re-enable movement if playerMovement exists
                if (playerMovement != null)
                {
                    playerMovement.canMove = true; // Re-enable movement
                }

                if (bullyMovement1 != null && bullyMovement2 != null)
                {
                    bullyMovement1.StartMoving();
                    bullyMovement2.StartMoving();
                }
            }
        }
    }
}
