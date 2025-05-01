using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{

    public string nextScene = "L1S2"; //Reference to the next scene
    public GameObject interactionPrompt; //Reference to prompt 
    private bool inRange = false; //Dertermines if player is in range of interaction or not

    void Update()
    {
        //if player is in the range and hits space bar move to next scene (enter pizza shop)
        if (inRange && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene);

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


}