using UnityEngine;

/// <summary>
/// Allows testing of all power-up effects by pressing corresponding keys.
/// Used in development to manually trigger power-ups without pickups.
/// </summary>
public class PowerUpTester : MonoBehaviour
{
    public L2S2_PowerUpController powerUpController;
    public int duration = 5;
    
    /// <summary>
    /// Monitors key presses and applies the selected power-up for testing.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Heal, duration);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.ExtraLife, duration);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.SpeedBoost, duration);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Shrink, duration);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Grow, duration);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.DoubleShot, duration);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.TripleShot, duration);

        if (Input.GetKeyDown(KeyCode.Alpha8))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Ghost, duration);

        if (Input.GetKeyDown(KeyCode.Alpha9))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.SlowTime, duration);

        if (Input.GetKeyDown(KeyCode.Alpha0))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Nuke, duration);

        if (Input.GetKeyDown(KeyCode.Q))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Piercing, duration);

        if (Input.GetKeyDown(KeyCode.P))
            powerUpController.ApplyPowerUp(L2S2_PowerUpHandler.PowerUpType.Shield, duration);
    }
}
