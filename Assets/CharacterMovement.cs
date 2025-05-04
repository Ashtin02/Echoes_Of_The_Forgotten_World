using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isMoving = false;
    
    void Start()
    {
        // Initialize target position to current position
        targetPosition = transform.position;
    }
    
    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Create movement vector
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        // Animation control for direction and speed
        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);
        animator.SetFloat("Speed", movement.sqrMagnitude);


        // Check if we're trying to move
        if (movement.magnitude > 0.1f)
        {
            // Normalize to prevent faster diagonal movement
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }
            
            // Calculate position we want to move to
            Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;
            
            // Cast a ray to check if we'd hit something
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                GetComponent<CircleCollider2D>() ? GetComponent<CircleCollider2D>().radius * 0.9f : 0.5f,
                movement,
                movement.magnitude * moveSpeed * Time.deltaTime
            );
            
            // If we hit something, don't move
            if (hit.collider == null || hit.collider.gameObject == gameObject)
            {
                transform.position = newPosition;
            }
        }
    }
}