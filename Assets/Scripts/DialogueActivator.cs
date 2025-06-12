using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    // Drag your "DialogueRunner_BullyStandoff" GameObject here in the Inspector
    public BullyStandoffManager dialogueManagerToActivate;
    private bool hasBeenTriggered = false; // To ensure it only triggers once

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenTriggered && other.CompareTag("Player")) // Make sure your player GameObject has the tag "Player"
        {
            if (dialogueManagerToActivate != null)
            {
                Debug.Log("Player entered bully trigger zone. Starting dialogue.");
                dialogueManagerToActivate.BeginDialogue();
                hasBeenTriggered = true; // Prevent re-triggering

                // Optional: Disable this trigger's collider or the GameObject itself
                // GetComponent<Collider2D>().enabled = false;
                // gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("DialogueManagerToActivate is not assigned on " + gameObject.name);
            }
        }
    }
}