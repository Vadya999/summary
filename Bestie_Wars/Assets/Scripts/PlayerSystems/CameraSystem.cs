using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class CameraSystem : GameSystem
{
    [SerializeField] private List<CameraConfiguration> cameraConfigurations;

    private CameraType currentCamera = CameraType.Idle;

    public void SetCamera(CameraType cameraType)
    {
        currentCamera = cameraType;
        foreach (var cameraConfiguration in cameraConfigurations)
        {
            cameraConfiguration.VirtualCamera.Priority = cameraConfiguration.CameraType == cameraType ? 1 : 0;
        }
    }

    public void ChangeCarCamera(CameraType cameraType)
    {
        if (currentCamera == CameraType.Idle || currentCamera == CameraType.Move)
        {
            SetCamera(cameraType);
        }
    }
}