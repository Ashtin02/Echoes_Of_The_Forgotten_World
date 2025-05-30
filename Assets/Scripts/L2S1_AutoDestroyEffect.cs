using UnityEngine;

public class L2S1_AutoDestroyEffect : MonoBehaviour
{
    public float lifetime = 1.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}