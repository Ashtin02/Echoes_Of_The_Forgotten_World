using UnityEngine;

public class L3_MiniBoss_Movement : MonoBehaviour
{
    private Rigidbody2D ship;
    public float speed = 5;
    private Vector2 Direction = Vector2.right;



    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        
    }

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