using UnityEngine;

public class L2S2_ProjectileMovement : MonoBehaviour
{
    public float fireSpeed = 10f;
    public bool isPiercing = false;


    private void Update()
    {
        transform.Translate(Vector2.right * fireSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
        }
    }
}