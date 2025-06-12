using UnityEngine;

public class L3_CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool inputEnabled = true;

    [Header("Collision Settings")]
    public LayerMask solidObjectsLayer;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) Debug.LogError("Rigidbody2D not found on " + gameObject.name + ".");
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null) Debug.LogWarning("Animator not found on " + gameObject.name + ".");
        }
    }

    void Update()
    {
        if (!inputEnabled)
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            if (animator != null) animator.SetFloat("Speed", 0);
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        if (animator != null)
        {
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        if (movement.magnitude > 0.1f)
        {
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }

            // Calculate the full desired position for this frame
            Vector3 fullDesiredPosition = transform.position + movement * moveSpeed * Time.deltaTime;

            CircleCollider2D charCollider = GetComponent<CircleCollider2D>();
            float castRadius = (charCollider != null) ? charCollider.radius : 0.5f;

            // Perform the CircleCast
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                castRadius,
                movement.normalized, // IMPORTANT: Cast in the normalized direction of movement
                (fullDesiredPosition - transform.position).magnitude, // Distance to check is how far we intend to move
                solidObjectsLayer // Only check against solid objects
            );

            // NEW & IMPROVED COLLISION RESOLUTION:
            if (hit.collider == null || hit.collider.gameObject == gameObject || hit.collider.isTrigger)
            {
                // No solid, non-self, non-trigger obstacle in the way, so apply the full desired movement.
                transform.position = fullDesiredPosition;
            }
            else
            {
                // Else, we hit a solid, non-self, non-trigger obstacle.
                // Move the character up to the point of collision, with a tiny buffer.
                // This helps prevent getting stuck and allows for smoother 'sliding' along walls.

                float distanceToMove = hit.distance - 0.01f; // Subtract a tiny buffer to avoid overlap

                // Only move if there's actual positive distance to move
                if (distanceToMove > 0)
                {
                    // Calculate the new position based on the normalized movement direction and the adjusted distance
                    transform.position = transform.position + movement.normalized * distanceToMove;
                }
                else
                {
                    // If distanceToMove is 0 or negative (already overlapping/at collision), just stay put.
                    // This prevents moving further into the wall.
                    transform.position = transform.position; // No movement
                }
                Debug.Log("BLOCKED and Adjusted Position by: " + hit.collider.name);
            }
        }
        else // No significant input
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            if (animator != null) animator.SetFloat("Speed", 0);
        }
    }

    /// <summary>
    /// Enables or disables player input and movement.
    /// </summary>
    /// <param name="enable">True to enable input, false to disable.</param>
    public void SetInputEnabled(bool enable)
    {
        inputEnabled = enable;
        if (!enable)
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            if (animator != null) animator.SetFloat("Speed", 0);
        }
        Debug.Log("Character Input Enabled: " + enable);
    }
}