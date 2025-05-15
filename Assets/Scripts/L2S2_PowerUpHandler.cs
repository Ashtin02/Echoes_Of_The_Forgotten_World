using UnityEngine;

public class L2S2_PowerUpHandler : MonoBehaviour
{
    public enum PowerUpType { Heal, Shield, Piercing, SpeedBoost, DoubleShot, TripleShot, ExtraLife, SlowTime, Nuke, Ghost, Shrink, Grow }
    public PowerUpType type;
    public int duration = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<L2S2_PowerUpController>();

            if (player != null)
            {
                player.ApplyPowerUp(type, duration);
            }

            Destroy(gameObject);
        }
    }
}