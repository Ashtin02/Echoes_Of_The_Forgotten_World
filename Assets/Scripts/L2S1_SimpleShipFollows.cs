using UnityEngine;

public class L2S1_SimpleShipFollows : MonoBehaviour
{
    public Transform shipTarget; 
    public float zDistance = -10f;    // How far back the camera is
    
    void LateUpdate()
    {
        if (shipTarget != null)
        {
            // Only follows the X and Y position of the ship
            transform.position = new Vector3(
                shipTarget.position.x, 
                shipTarget.position.y, 
                zDistance
            );
        }
    }
}