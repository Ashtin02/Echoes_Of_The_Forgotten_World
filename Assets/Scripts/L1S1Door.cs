using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    public GameObject interactionPrompt; //Reference to prompt 
    public Animator transition;
    public float TransitionTime = 1f;
    private bool inRange = false; //Dertermines if player is in range of interaction or not

    public GameObject BullyObject1; //reference to bully sprite 1
    public GameObject BullyObject2; //reference to bully sprite 2
    private BullyMovement bullyMovement1; //bully 1 movement reference 

    private BullyMovement bullyMovement2; //bully 2 movement reference 


    void Update()
    {
        //if player is in the range and hits space bar move to next scene (enter pizza shop)
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

    //Wehn player is in range activate the interation prompt
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    //Player leaves range so we get rid of the prompt
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            interactionPrompt.SetActive(false);
        }
    }



    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {

        //Play anim 
        transition.SetTrigger("start");

        //Wait 
        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(LevelIndex);


    }

}