using UnityEngine;

public class DialogueActivator : MonoBehaviour
/// Attach this script to a trigger collider GameObject
/// <summary>
/// Activates a BullyStandoffManager dialogue sequence when the player enters the trigger zone.
{
    public BullyStandoffManager dialogueManagerToActivate;
    private bool hasBeenTriggered = false; 
    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenTriggered && other.CompareTag("Player"))
        {
            if (dialogueManagerToActivate != null)
            {
                Debug.Log("Player entered bully trigger zone. Starting dialogue.");
                dialogueManagerToActivate.BeginDialogue();
                hasBeenTriggered = true; 
            }
            else
            {
                Debug.LogError("DialogueManagerToActivate is not assigned on " + gameObject.name);
            }
        }
    }
}