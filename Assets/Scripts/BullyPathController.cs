using System.Collections;
using UnityEngine;

public class BullyPathController : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2.0f;
    public GameObject dialogueCanvas; // Reference to dialogue UI
    public float conversationDelay = 0.5f; // Time before conversation starts
    public string[] dialogueLines; // Lines for the conversation
    
    private int currentWaypoint = 0;
    private bool isMoving = true;
    private bool hasReachedDestination = false;
    
    void Start()
    {
        // Make sure dialogue is initially hidden
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }
    
    void Update()
    {
        if (!isMoving || waypoints.Length == 0)
            return;
            
        // Move towards current waypoint
        Transform targetWaypoint = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, 
                                               targetWaypoint.position, 
                                               moveSpeed * Time.deltaTime);
        
        // Handle sprite flipping based on movement direction
        if (targetWaypoint.position.x > transform.position.x)
        {
            // Moving right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
                                             transform.localScale.y, 
                                             transform.localScale.z);
        }
        else if (targetWaypoint.position.x < transform.position.x)
        {
            // Moving left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 
                                             transform.localScale.y, 
                                             transform.localScale.z);
        }
        
        // If reached waypoint, move to next one
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypoint++;
            
            // If we've reached the final waypoint
            if (currentWaypoint >= waypoints.Length)
            {
                // Stop moving and start conversation
                isMoving = false;
                hasReachedDestination = true;
                StartCoroutine(StartConversation());
            }
        }
    }
    
    private IEnumerator StartConversation()
    {
        // Wait a moment before starting dialogue
        yield return new WaitForSeconds(conversationDelay);
        
        // Show dialogue UI
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(true);
            
        // Here you'd trigger your dialogue system
        // This is a placeholder - connect to your actual dialogue system
        Debug.Log("Starting conversation with NPC");
        
        // Example of displaying dialogue lines
        if (dialogueLines != null && dialogueLines.Length > 0)
        {
            foreach (string line in dialogueLines)
            {
                Debug.Log("Dialogue: " + line);
                yield return new WaitForSeconds(2.0f); // Time between lines
            }
        }
    }
}