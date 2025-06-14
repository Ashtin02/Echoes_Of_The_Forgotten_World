using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public GameObject explosionPrefab;
    public GameObject ship;
    /// <summary>
    /// Destroys the missile after its lifetime expires.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    /// <summary>
    /// Moves the missile downward at a constant speed.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    /// <summary>
    /// Handles collision with the player, instantiating an explosion effect and destroying the missile.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}