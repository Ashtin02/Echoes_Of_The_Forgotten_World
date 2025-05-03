using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f; //Player speed 
    public bool canMove = true; //Determines if player can move or not
    private Rigidbody2D rb; // Reference to playes rigid body 
    private Vector2 mov; // Stores movement

    void Start()
    {
        //initialized rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //dont move if not allowed
        if (!canMove) return;
        //get movement input
        mov.x = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        //if movememnt is turned off atop the player
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        // move left or right
        rb.linearVelocity = new Vector2(mov.x * moveSpeed, rb.linearVelocity.y);
    }
}
