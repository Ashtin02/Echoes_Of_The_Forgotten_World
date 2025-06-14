using UnityEngine;

public class L2S1_MissileBehavior : MonoBehaviour
{
    public float speed = 15f;
    private Transform targetShip;
    private GameObject explosionEffectPrefab;

    /// <summary>
    /// Sets the target for the missile and orients it towards the target.
    /// </summary>
    /// <param name="newTarget"></param>
    /// <param name="explosionPrefabFromDialog"></param>
    public void SetTarget(Transform newTarget, GameObject explosionPrefabFromDialog)
    {
        targetShip = newTarget;
        explosionEffectPrefab = explosionPrefabFromDialog;

        if (targetShip != null)
        {
            Vector3 direction = (targetShip.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
    /// <summary>
    /// Moves the missile towards the target or straight ahead if no target is set.
    /// </summary>
    void Update()
    {
        if (targetShip == null)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetShip.position, speed * Time.deltaTime);
    }

        void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Debug.Log("Collision Detected with: " + otherCollider.name + " (Tagged as Player)");
            HandleImpact();
        }
        
    }
    /// <summary>
    /// Handles the impact of the missile, instantiating an explosion effect and destroying the missile.
    /// </summary>
    void HandleImpact()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Debug.Log("Explosion effect instantiated.");
        }
        else
        {
            Debug.LogWarning("ExplosionEffectPrefab was not assigned to this missile instance.");
        }
        Destroy(gameObject);
        Debug.Log("Missile GameObject destroyed.");
    }
}