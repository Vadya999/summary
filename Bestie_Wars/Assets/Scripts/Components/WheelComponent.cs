using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelComponent : MonoBehaviour
{
    [SerializeField] private float X;
    [SerializeField] private float Y;
    [SerializeField] private float Z;

    private Vector3 lastPosition;

    private void Awake()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        var currentDelta = lastPosition - transform.position;
        
        transform.Rotate(new Vector3(currentDelta.sqrMagnitude*X, currentDelta.sqrMagnitude*Y, currentDelta.sqrMagnitude*Z));
        lastPosition = transform.position;
    }
}
