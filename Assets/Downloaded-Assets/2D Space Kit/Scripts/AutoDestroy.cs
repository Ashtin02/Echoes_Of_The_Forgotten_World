using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 10f;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}