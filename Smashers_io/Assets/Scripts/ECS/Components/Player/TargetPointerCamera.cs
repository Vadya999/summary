using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TargetPointerCamera : MonoBehaviour
{
    [SerializeField] private Camera _originalCamera;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera.fieldOfView = _originalCamera.fieldOfView;
        _camera.orthographic = _originalCamera.orthographic;
        _camera.orthographicSize = _originalCamera.orthographicSize;
    }
}
