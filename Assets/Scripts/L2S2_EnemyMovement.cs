using System.Data;
using UnityEngine;

public class L2S2_EnemyMovement : MonoBehaviour
{
    public int rows = 5;
    public int columns = 7;
    public float spacing = 2.0f;
    public GameObject enemyShip;
    public Vector3 Direction = Vector2.right;
    public float speed = 5.0f;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// This method initializes the enemy formation by instantiating enemy ships in a grid pattern.
    /// </summary>
    void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector2 rowPos = new Vector2(centering.x, centering.y + (row * spacing));

            for (int col = 0; col < this.columns; col++)
            {
                GameObject enemy = Instantiate(this.enemyShip, this.transform);

                Vector2 position = rowPos;
                position.x += col * spacing;
                enemy.transform.localPosition = position;
            }
        }
    }
    /// <summary>
    /// Update is called once per frame.
    /// This method moves the enemy formation in the specified direction and checks for boundaries.
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
    /// Advances the enemy formation by changing its direction and moving it down.
    /// </summary>
    private void Advance()
    {
        Direction.x *= -1f;
        Vector2 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }


}