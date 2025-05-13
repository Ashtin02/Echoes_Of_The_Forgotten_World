using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    public GameObject explosionPrefab;
    public float bottomOfScreen = -5f;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        if (transform.position.y < bottomOfScreen)
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}