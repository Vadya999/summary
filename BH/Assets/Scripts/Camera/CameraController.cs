using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    
    private float _xRotation;

    private void Update()
    {
        MouseMove();
    }

    private void MouseMove()
    {
        _xRotation += Input.GetAxis("Mouse X");
        
        transform.rotation = Quaternion.Euler(0f, _xRotation * _rotateSpeed,0f);
    }
}
