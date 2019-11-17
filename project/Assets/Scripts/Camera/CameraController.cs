using UnityEngine;

public class CameraController : MonoBehaviour
    {

    private CameraModel _cameraModel;
    private CameraView _cameraView;
    private GameObject _player;

    private float _zoom;
    private Vector3 _cameraOffset;
 
    // Start is called before the first frame update
    void Start()
    {
        _cameraModel = GetComponent<CameraModel>();
        _cameraView = GetComponent<CameraView>();

        _player = GameObject.FindGameObjectsWithTag("Player")[0];
        _zoom = Camera.main.fieldOfView;
        _cameraOffset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _cameraView.HandlePosition(_cameraModel.rotationSpeed, ref _cameraOffset, _cameraModel.cameraYOffset, _cameraModel.smoothFactor, _player.transform.position);
        _cameraView.HandleZoom(ref _zoom, _cameraModel.zoomMin, _cameraModel.zoomMax, _cameraModel.zoomSpeed, _cameraModel.zoomRoughness);
        //mainCamera.Rota(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

    }

}
