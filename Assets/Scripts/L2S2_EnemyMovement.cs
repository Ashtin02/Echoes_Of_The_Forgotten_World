using System.Data;
using UnityEngine;

/// <summary>
/// Controls the collective movement of a grid of enemies,
/// moving them horizontally and advancing them downward upon hitting screen edges.
/// </summary>
public class L2S2_EnemyMovement : MonoBehaviour
{
    public int rows = 5;
    public int columns = 7;
    public float spacing = 2.0f;
    public Vector3 Direction = Vector2.right;
    public float speed = 5.0f;

    /// <summary>
    /// Updates the enemy formation's position each frame.
    /// Reverses direction and advances downward when hitting the screen edge.
    /// </summary>
    void Update()
    {
        this.transform.position += Direction * this.speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1))
            {
                Advance();
                break;
            }
            else if (Direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1))
            {
                Advance();
                break;
            }
        }
    }

    /// <summary>
    /// Logic that moves the enemies closer to the ship each time they hit the wall and changes their direction 
    /// </summary>
    private void Advance()
    {
        Direction.x *= -1f;
        Vector2 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }


}