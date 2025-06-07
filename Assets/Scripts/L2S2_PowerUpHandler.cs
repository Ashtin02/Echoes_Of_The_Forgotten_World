using UnityEngine;

/// <summary>
/// Handles the interaction between power-up items and the player ship.
/// Applies the correct power-up effect upon collision and then destroys the item.
/// </summary>
public class L2S2_PowerUpHandler : MonoBehaviour
{
    /// <summary>
    /// Defines the types of power-ups available in the game.
    /// </summary>
    public enum PowerUpType { Heal, Shield, Piercing, SpeedBoost, DoubleShot, TripleShot, ExtraLife, SlowTime, Nuke, Ghost, Shrink, Grow }
    public PowerUpType type;
    public int duration = 10;

    /// <summary>
    /// Handles collison of power ups and playable ship
    /// </summary>
    /// <param name="collision"> item that will collide (power up, in this case)</param>
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