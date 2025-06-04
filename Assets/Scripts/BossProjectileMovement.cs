using UnityEngine;

public class BossProjectileMovement : MonoBehaviour {
    public float fireSpeed = 10f;


    private void Update()
    {
        transform.Translate(Vector2.down * fireSpeed * Time.deltaTime, Space.World);
    }
}