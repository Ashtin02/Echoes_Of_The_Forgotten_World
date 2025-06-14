using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    public GameObject interactionPrompt;
    public Animator transition;
    public float TransitionTime = 1f;
    private bool inRange = false; 

    public GameObject BullyObject1; 
    public GameObject BullyObject2; 
    private BullyMovement bullyMovement1; 

    private BullyMovement bullyMovement2; 

    /// <summary>
    /// Checks for player input to trigger level transition when in range of the door.
    /// </summary>
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Space))
        {
            bullyMovement1 = BullyObject1.GetComponent<BullyMovement>();
            bullyMovement2 = BullyObject2.GetComponent<BullyMovement>();
            Rigidbody2D rb1 = BullyObject1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = BullyObject2.GetComponent<Rigidbody2D>();
            bullyMovement1.enabled = false;
            bullyMovement2.enabled = false;
            rb1.linearVelocity = Vector2.zero;
            rb2.linearVelocity = Vector2.zero;



            LoadNextLevel();
        }
    }
    /// <summary>
    /// Detects when the player enters the door's trigger zone to show interaction prompt.
    /// </summary>
    /// <param name="collision"></param>
       void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            interactionPrompt.SetActive(true);
        }
    }
    /// <summary>
    /// Detects when the player exits the door's trigger zone to hide interaction prompt.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            interactionPrompt.SetActive(false);
        }
    }

    /// <summary>
    /// Initiates loading the next level with a transition effect.
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    /// <summary>
    /// Handles the level loading process with a transition animation and delay.
    /// </summary>
    /// <param name="LevelIndex"></param>
    /// <returns></returns>
    IEnumerator LoadLevel(int LevelIndex)
    {

        transition.SetTrigger("start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(LevelIndex);


    }

}