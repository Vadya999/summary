using System;
using Cinemachine;
using UnityEngine;

[Serializable]
public class CameraConfiguration
{
    [SerializeField] private CameraType cameraType;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public CameraType CameraType => cameraType;

    public CinemachineVirtualCamera VirtualCamera => virtualCamera;
}