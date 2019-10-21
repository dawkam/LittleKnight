using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
    {

    public GameObject player;

    public float zoomSpeed;
    public float zoomMax;
    public float zoomMin;
    public float zoomRoughness;

    public float rotationSpeed;
    public float smoothFactor;

    public int cameraYOffset;

    private float _zoom;

    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        _zoom = Camera.main.fieldOfView;
        _cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePosition();
        HandleZoom();
        //mainCamera.Rota(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

    }

    private void HandleZoom()
    {
        _zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _zoom = Mathf.Clamp(_zoom, zoomMax, zoomMin);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _zoom, Time.deltaTime * zoomRoughness);
    }


    private void HandlePosition()
    {

        Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        _cameraOffset = cameraTurnAngle * _cameraOffset;


        transform.position = Vector3.Slerp(transform.position, player.transform.position + _cameraOffset ,smoothFactor );
        var modifiedPlayerPosition = player.transform.position + Vector3.up * cameraYOffset;
        transform.LookAt(modifiedPlayerPosition);

    }
}
