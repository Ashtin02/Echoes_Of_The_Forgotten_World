using UnityEngine;

public class L2S1_MissileBehavior : MonoBehaviour
{
    public float speed = 15f;
    private Transform targetShip;
    private GameObject explosionEffectPrefab; // This will be your explosion animation

    public void SetTarget(Transform newTarget, GameObject explosionPrefabFromDialog)
    {
        targetShip = newTarget;
        explosionEffectPrefab = explosionPrefabFromDialog;

        if (targetShip != null)
        {
            Vector3 direction = (targetShip.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Assuming your missile sprite points "upwards" by default.
            // If it points right, you might remove the -90 or adjust.
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    void Update()
    {
        if (targetShip == null)
        {
            // If no target, missile flies straight based on its initial rotation.
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
            // Optional: Destroy if it goes off-screen after a while
            // Destroy(gameObject, 5f); // Example: destroy after 5 seconds if no target/off-screen
            return;
        }

        // Move towards the target
        transform.position = Vector2.MoveTowards(transform.position, targetShip.position, speed * Time.deltaTime);
    }

    // This is called when the missile's trigger collider ENTERS another collider.
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Check if the object we hit is tagged "Player"
        if (otherCollider.CompareTag("Player"))
        {
            Debug.Log("Collision Detected with: " + otherCollider.name + " (Tagged as Player)");
            HandleImpact();
        }
        // You could add other checks here, e.g., if it hits an asteroid tagged "Obstacle"
        // else if (otherCollider.CompareTag("Obstacle"))
        // {
        //    Debug.Log("Hit an obstacle!");
        //    HandleImpact(); // Or a different kind of impact
        // }
    }

    void HandleImpact()
    {
        // 1. Instantiate Explosion Effect
        if (explosionEffectPrefab != null)
        {
            // Create the explosion at the missile's current position and rotation
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Debug.Log("Explosion effect instantiated.");
        }
        else
        {
            Debug.LogWarning("ExplosionEffectPrefab was not assigned to this missile instance.");
        }

        // 2. Destroy the Missile Itself
        Destroy(gameObject); // This removes the missile from the game
        Debug.Log("Missile GameObject destroyed.");
    }
}