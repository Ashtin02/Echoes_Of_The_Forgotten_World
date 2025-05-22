using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public GameObject explosionPrefab;
    public GameObject ship;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}