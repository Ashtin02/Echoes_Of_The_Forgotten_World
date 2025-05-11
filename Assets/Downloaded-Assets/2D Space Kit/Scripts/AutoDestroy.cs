using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 1f;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}