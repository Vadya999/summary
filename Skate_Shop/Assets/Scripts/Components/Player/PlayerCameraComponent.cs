using Cinemachine;
using UnityEngine;

public class PlayerCameraComponent : MonoBehaviour
{
    [field: SerializeField] public Camera camera { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera mainVirtualCamera { get; private set; }

    private void Awake()
    {
        transform.parent = null;
    }
}