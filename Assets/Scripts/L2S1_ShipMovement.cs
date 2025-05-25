using UnityEngine;

public class L2S1_ShipMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float duration = 10f;
    private float elapsedTime = 0f;
    
    void Update()
    {
        if (elapsedTime < duration)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
    }
}