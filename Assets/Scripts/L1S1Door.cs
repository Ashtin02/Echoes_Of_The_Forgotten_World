using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{

    public string nextScene = "L1S2";
    public GameObject interactionPrompt;
    private bool inRange = false;

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            interactionPrompt.SetActive(false);
        }
    }


}