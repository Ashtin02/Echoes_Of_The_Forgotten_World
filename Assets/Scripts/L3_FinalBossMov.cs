using UnityEngine;

/// <summary>
/// Controls the final boss's side-to-side movement within screen bounds,
/// reversing direction when it reaches the screen edges.
/// </summary>
public class L3_FinalBossMov : MonoBehaviour
{
    private Rigidbody2D ship;
    public float speed;
    private Vector2 Direction = Vector2.right;

    /// <summary>
    /// gets the rigid body of the final boss
    /// </summary>
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// continously moves boss left and right between camera bounds
    /// </summary> 
    void Update()
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