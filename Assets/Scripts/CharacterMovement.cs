using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isMoving = false;
    /// <summary>
    /// Initialize the SpriteMovement component.
    /// This sets the initial target position to the current position of the sprite.
    /// </summary>
    void Start()
    {
        targetPosition = transform.position;
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// This method handles player input for movement and updates the sprite's position accordingly.
    /// </summary>
    void Update()
    {
     
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.magnitude > 0.1f)
        {
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }
            
            Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;
            
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                GetComponent<CircleCollider2D>() ? GetComponent<CircleCollider2D>().radius * 0.9f : 0.5f,
                movement,
                movement.magnitude * moveSpeed * Time.deltaTime
            );
            
            if (hit.collider == null || hit.collider.gameObject == gameObject)
            {
                transform.position = newPosition;
            }
        }
    }
}