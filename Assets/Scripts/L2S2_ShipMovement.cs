using UnityEngine;

/// <summary>
/// Controls player ship movement using Rigidbody2D physics,
/// based on directional input from the keyboard.
/// </summary>
public class L2S2_ShipMovement : MonoBehaviour
{
    public float movSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    /// <summary>
    /// Gets the rigid body that the enemeies are connected to 
    /// </summary> 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Gets positions in each frame
    /// </summary>
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Applies velocity to the Rigidbody2D based on player input.
    /// Uses FixedUpdate for consistent physics behavior.
    /// </summary>
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * movSpeed, movement.y * movSpeed);
    }
}