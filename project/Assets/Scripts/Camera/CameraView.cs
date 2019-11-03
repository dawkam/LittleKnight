using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    // Start is called before the first frame update

    public void HandleZoom(ref float zoom, float zoomMin , float zoomMax, float zoomSpeed, float zoomRoughness)
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoom = Mathf.Clamp(zoom, zoomMax, zoomMin);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, Time.deltaTime * zoomRoughness);
    }

    public void HandlePosition(float rotationSpeed, ref Vector3 cameraOffset, float cameraYOffset ,float smoothFactor, Vector3 playerPosition  )
    {

        Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        cameraOffset = cameraTurnAngle * cameraOffset;


        transform.position = Vector3.Slerp(transform.position, playerPosition + cameraOffset, smoothFactor);
        var modifiedPlayerPosition = playerPosition + Vector3.up *cameraYOffset;
        transform.LookAt(modifiedPlayerPosition);

    }
}
