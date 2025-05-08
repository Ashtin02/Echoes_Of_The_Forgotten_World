using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    public GameObject interactionPrompt; //Reference to prompt 
    public Animator transition;
    public float TransitionTime = 1f;
    private bool inRange = false; //Dertermines if player is in range of interaction or not

    void Update()
    {
        //if player is in the range and hits space bar move to next scene (enter pizza shop)
        if (inRange && Input.GetKeyDown(KeyCode.Space))
        {
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

        //Load Scene 


    }

}