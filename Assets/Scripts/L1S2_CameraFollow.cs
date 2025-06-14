using UnityEngine;
public class L1S2_CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);
    /// <summary>
    /// Updates the camera position to smoothly follow the target with a specified offset.
    /// </summary>
    void LateUpdate()
    {
        if (target == null)
            return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}