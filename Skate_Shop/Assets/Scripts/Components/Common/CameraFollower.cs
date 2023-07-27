using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private bool _inverse;

    private Transform _camera;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        var toCameraDirection = _camera.position - transform.position;
        transform.forward = _inverse ? -toCameraDirection : toCameraDirection;
    }
}
