using UnityEngine;

/// <summary>
/// Restricts the player ship's movement within the screen bounds,
/// specifically limiting vertical movement to the bottom third of the screen.
/// </summary>
public class L2S2_ShipBounds : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    /// <summary>
    /// Gets the measurments of the screen and playable ship
    /// </summary>
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Get object size to prevent it from going partially off screen
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    /// <summary>
    /// Limits the players movement to the bottom 1/3 of screen
    /// </summary> 
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);

        float bottomY = -screenBounds.y + objectHeight;
        float bottomThirdY = -screenBounds.y + (screenBounds.y * 2 / 3) - objectHeight;

        viewPos.y = Mathf.Clamp(viewPos.y, bottomY, bottomThirdY);

        transform.position = viewPos;
    }
}
