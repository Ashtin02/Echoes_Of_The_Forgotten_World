using UnityEngine;

public class L2S1_AutoDestroyEffect : MonoBehaviour
{
    public float lifetime = 1.0f;
    /// <summary>
    /// Destroys the GameObject after the specified lifetime.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}