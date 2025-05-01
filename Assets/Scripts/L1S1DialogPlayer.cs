using UnityEngine;
using TMPro;

public class AutoDialogPlayer : MonoBehaviour
{
    public GameObject dialogBox; //Reference to the Dialog Box
    public TMP_Text dialogText;   //Reference to the Dialog Text
    public string[] lines;
    public GameObject playerObject; // Reference to the player object

    public GameObject BullyObject1;
    public GameObject BullyObject2;
    private BullyMovement bullyMovement1;

    private BullyMovement bullyMovement2;




    private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private int currentLine = 0;
    private bool dialogActive = false;

    void Start()
    {
        // Ensure playerObject is assigned
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.canMove = false; // Stop player movement
        }

        if (BullyObject1 != null && BullyObject2 != null)
        {
            bullyMovement1 = BullyObject1.GetComponent<BullyMovement>();
            bullyMovement2 = BullyObject2.GetComponent<BullyMovement>();
        }

        dialogBox.SetActive(true);
        dialogText.text = lines[currentLine];
        dialogActive = true;
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
                dialogBox.SetActive(false);
                dialogActive = false;

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
