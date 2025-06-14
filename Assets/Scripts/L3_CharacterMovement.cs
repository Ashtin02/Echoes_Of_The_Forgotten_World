using UnityEngine;

public class L3_CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private bool inputEnabled = true;

    [Header("Collision Settings")]
    public LayerMask solidObjectsLayer;
    /// <summary>
    /// Initializes references to Rigidbody2D and Animator components.
    /// </summary>
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
    /// <summary>
    /// Handles player input, movement, and collision detection/resolution.
    /// </summary>
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

            Vector3 fullDesiredPosition = transform.position + movement * moveSpeed * Time.deltaTime;

            CircleCollider2D charCollider = GetComponent<CircleCollider2D>();
            float castRadius = (charCollider != null) ? charCollider.radius : 0.5f;

            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                castRadius,
                movement.normalized, 
                (fullDesiredPosition - transform.position).magnitude, 
                solidObjectsLayer 
            );

            
            if (hit.collider == null || hit.collider.gameObject == gameObject || hit.collider.isTrigger)
            {
                transform.position = fullDesiredPosition;
            }
            else
            {
                float distanceToMove = hit.distance - 0.01f; 

                if (distanceToMove > 0)
                {
                    transform.position = transform.position + movement.normalized * distanceToMove;
                }
                else
                {
                    transform.position = transform.position; 
                }
                Debug.Log("BLOCKED and Adjusted Position by: " + hit.collider.name);
            }
        }
        else 
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