using UnityEngine;

public class L2S2_ShipBounds : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Get object size to prevent it from going partially off screen
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        // Clamp position within screen bounds (adjusted for object size)
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);

        float bottomY = -screenBounds.y + objectHeight;
        float bottomThirdY = -screenBounds.y + (screenBounds.y * 2 / 3) - objectHeight;
        
        viewPos.y = Mathf.Clamp(viewPos.y, bottomY, bottomThirdY);

        transform.position = viewPos;
    }
}
