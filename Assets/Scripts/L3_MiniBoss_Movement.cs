using UnityEngine;

/// <summary>
/// Handles horizontal movement of a mini boss,
/// causing it to bounce between the left and right edges of the screen.
/// </summary>
public class L3_MiniBoss_Movement : MonoBehaviour
{
    private Rigidbody2D ship;
    public float speed = 5;
    private Vector2 Direction = Vector2.right;

    /// <summary>
    /// Gets rigid body for the mini boss 
    /// </summary>
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves bosses constantly left and right between screen bounds
    /// </summary>
    void FixedUpdate()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (ship.position.x >= (rightEdge.x - 2))
        {
            Direction = Vector2.left;
        }
        if (ship.position.x <= (leftEdge.x + 2))
        {
            Direction = Vector2.right;
        }

        ship.linearVelocity = Direction * speed;
    }

}