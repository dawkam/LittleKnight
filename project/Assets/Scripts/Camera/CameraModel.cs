using UnityEngine;

public class CameraModel : MonoBehaviour
{
    [Header("Zoom")]
    public float zoomSpeed;
    public float zoomMax;
    public float zoomMin;
    public float zoomRoughness;

    [Header("Rotation")]
    public float rotationSpeed;
    public float smoothFactor;

    [Header("Other")]
    public int cameraYOffset;
}
