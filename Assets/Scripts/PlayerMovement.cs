using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;  
    public bool canMove = true; 
    private Rigidbody2D rb; 
    private Vector2 mov; 
    /// <summary>
    /// Initializes references to Rigidbody2D component.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Handles player input for horizontal movement.
    /// </summary>
    void Update()
    {
        if (!canMove) return;
        mov.x = Input.GetAxisRaw("Horizontal");
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = new Vector2(mov.x * moveSpeed, rb.linearVelocity.y);
    }
}
