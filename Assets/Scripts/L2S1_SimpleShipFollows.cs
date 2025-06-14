using UnityEngine;

public class L2S1_SimpleShipFollows : MonoBehaviour
{
    public Transform shipTarget; 
    public float zDistance = -10f;
    
    /// <summary>
    /// Updates the camera position to follow the ship target while maintaining a fixed z-distance.
    /// </summary>
    void LateUpdate()
    {
        if (shipTarget != null)
        {

            transform.position = new Vector3(
                shipTarget.position.x, 
                shipTarget.position.y, 
                zDistance
            );
        }
    }
}