using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages automatic dialog playback, including avatar switching and enabling movement after dialog ends.
/// </summary>
public class AutoDialogPlayer : MonoBehaviour
{
    public GameObject dialogBox; 
    public TMP_Text dialogText;   
    public string[] lines; 
    public GameObject playerObject; 
    public GameObject BullyObject1; 
    public GameObject BullyObject2; 
    private BullyMovement bullyMovement1; 
    private BullyMovement bullyMovement2;
    public Image avatar;
    public Sprite[] avatars; 
    public int[] speakerInd; 
    private PlayerMovement playerMovement;
    private int currentLine = 0;
    private bool dialogActive = false;
    
    /// <summary>
    /// Initializes the dialog system, disables player movement, and starts the dialog sequence.
    /// </summary>
    void Start()
    {
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.canMove = false;
        }
    
        if (BullyObject1 != null && BullyObject2 != null)
        {
            bullyMovement1 = BullyObject1.GetComponent<BullyMovement>();
            bullyMovement2 = BullyObject2.GetComponent<BullyMovement>();
        }
        
        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        dialogActive = true;
       
        if (speakerInd.Length > 0 && speakerInd[0] < avatars.Length)
        {
            avatar.sprite = avatars[speakerInd[0]];
        }
        else
        {
            avatar.sprite = null;
            avatar.color = new Color(1, 1, 1, 0);
        }
    }
    /// <summary>
    /// Handles player input to progress dialog and manages enabling movement after dialog ends.
    /// </summary>
    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

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
                dialogBox.SetActive(false);
                dialogActive = false;
                avatar.sprite = null;
                avatar.color = new Color(1, 1, 1, 0);

                if (playerMovement != null)
                {
                    playerMovement.canMove = true;
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
