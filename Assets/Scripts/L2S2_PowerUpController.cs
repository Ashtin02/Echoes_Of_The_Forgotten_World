using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the application and effect durations of various power-ups
/// Includes logic for healing, speed boosts, shooting variations, resizing, shielding, and more.
/// </summary>
public class L2S2_PowerUpController : MonoBehaviour
{
    public L2S2_HealthSystem healthSystem;
    public GameObject explosion;
    public L2S2_ShipFiring shipFiring;
    private Vector3 defaultScale;

    /// <summary>
    /// saves original scale of our ship so we can use it later for shrinking or growing
    /// </summary>
    //
    void Start()
    {
        defaultScale = transform.localScale;
    }
    /// <summary>
    /// Applies power up depeninf on which type it is
    /// </summary>
    /// <param name="type"> type of power up </param>
    /// <param name="duration"> duration of power up</param>
    public void ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType type, int duration)
    {
        switch (type)
        {
            case L2S2_PowerUpHandler.PowerUpType.Heal:
                if (healthSystem != null)
                {
                    healthSystem.Heal(1);
                }
                break;

            case L2S2_PowerUpHandler.PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost(duration));
                break;

            case L2S2_PowerUpHandler.PowerUpType.DoubleShot:
                StartCoroutine(doubleShot(duration));
                break;

            case L2S2_PowerUpHandler.PowerUpType.TripleShot:
                StartCoroutine(TripleShot(duration));
                break;

            case L2S2_PowerUpHandler.PowerUpType.ExtraLife:
                if (healthSystem != null)
                {
                    healthSystem.AddLife();
                }
                break;

            case L2S2_PowerUpHandler.PowerUpType.Ghost:
                StartCoroutine(Ghost(duration));
                break;

            case L2S2_PowerUpHandler.PowerUpType.Grow:
                StartCoroutine(Resize(duration, 2));
                break;

            case L2S2_PowerUpHandler.PowerUpType.Shrink:
                StartCoroutine(Resize(duration, 0.5f));
                break;

            case L2S2_PowerUpHandler.PowerUpType.Nuke:
                Nuke();
                break;

            case L2S2_PowerUpHandler.PowerUpType.Piercing:
                StartCoroutine(Pierce(duration));
                break;

            case L2S2_PowerUpHandler.PowerUpType.Shield:
                applyShield();
                break;

            case L2S2_PowerUpHandler.PowerUpType.SlowTime:
                StartCoroutine(SlowTime(duration));
                break;

            default:
                Debug.LogWarning($"Power-up {type} is not implemented.");
                break;

        }
    }


    /// <summary>
    /// function that gives speed boost for 10 seconds
    /// </summary>
    /// <param name="duration"> duration of power</param>
    private IEnumerator SpeedBoost(int duration)
    {
        var movement = GetComponent<L2S2_ShipMovement>();
        if (movement != null)
        {
            float originalSpeed = movement.movSpeed;
            movement.movSpeed *= 2f;
            yield return new WaitForSeconds(duration);
            movement.movSpeed = originalSpeed;
        }
    }


    /// <summary>
    /// Resizes my player by the multiplier given in params, reverts to original size after duration
    /// </summary>
    /// <param name="duration"> duration of size change</param>
    /// <param name="multiplier"> the multiplier of the size change</param>
    private IEnumerator Resize(int duration, float multiplier)
    {
        transform.localScale = defaultScale * multiplier;

        var movement = GetComponent<L2S2_ShipMovement>();
        if (movement != null)
        {
            float originalSpeed = movement.movSpeed;
            movement.movSpeed = originalSpeed / multiplier;

            yield return new WaitForSeconds(duration);
            movement.movSpeed = originalSpeed;
        }
        transform.localScale = defaultScale;
    }


    /// <summary>
    /// Destoys all enemies on the screen, Does not affect bosses
    /// </summary>
    private void Nuke()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (explosion != null)
            {
                Instantiate(explosion, enemy.transform.position, enemy.transform.rotation);
            }

            Destroy(enemy);
        }
    }

    /// <summary>
    /// Temporarily disables collision and makes the ship semi-transparent.
    /// </summary>
    /// <param name="duration"> duration of power </param>

    private IEnumerator Ghost(int duration)
    {
        var collider = GetComponent<Collider2D>();
        var ship = GetComponent<SpriteRenderer>();

        if (collider != null)
        {
            collider.enabled = false;
        }

        if (ship != null)
        {
            ship.color = new Color(ship.color.r, ship.color.g, ship.color.b, 0.5f);
        }
        yield return new WaitForSeconds(duration);
        collider.enabled = true;
        ship.color = new Color(ship.color.r, ship.color.g, ship.color.b, 1f);
    }

    /// <summary>
    /// Slows time for the whole game (60% speed)
    /// </summary>
    /// <param name="duration"> duration of power</param>

    private IEnumerator SlowTime(int duration)
    {
        Time.timeScale = 0.6f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Gives the bullets a piercing effect which does not destoy them on contact
    /// </summary>
    /// <param name="duration"> duraton of power </param>

    private IEnumerator Pierce(int duration)
    {
        shipFiring.Piercing = true;
        yield return new WaitForSeconds(duration);
        shipFiring.Piercing = false;
    }

    /// <summary>
    /// Lets our ship shoot two bullets at a time instead of one
    /// </summary>
    /// <param name="duration"> duration of power </param>

    private IEnumerator doubleShot(int duration)
    {
        shipFiring.Double = true;
        shipFiring.Triple = false;
        yield return new WaitForSeconds(duration);
        shipFiring.Double = false;
    }

    /// <summary>
    /// Lets ourt ship shoot three bullets at a time instead of one
    /// </summary>
    /// <param name="duration"> duration of power </param>
    private IEnumerator TripleShot(int duration)
    {
        shipFiring.Double = false;
        shipFiring.Triple = true;
        yield return new WaitForSeconds(duration);
        shipFiring.Triple = false;
    }

    /// <summary>
    /// Applies a "shield" to our ship which will block one incoming attack
    /// </summary>
    private void applyShield()
    {
        if (healthSystem != null)
        {
            healthSystem.hasShield = true;
            var ship = GetComponent<SpriteRenderer>();
            ship.color = Color.cyan;
        }
    }

}