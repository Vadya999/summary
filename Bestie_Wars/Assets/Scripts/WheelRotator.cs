using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    [field: SerializeField] public Transform[] FrontWheels { get; private set; }

    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    [SerializeField] [BoxGroup("params")] private float tiltAngleY;
    [SerializeField] [BoxGroup("params")] private float smooth = 5.0f;

    private void Update()
    {
        if (joystick.Direction != Vector2.zero)
        {
            SetFrontWheels(joystick);
        }
    }

    private void SetFrontWheels(Joystick joystick)
    {
        var tr = transform;
        var forward = tr.forward;
        var carVector = new Vector2(forward.x, forward.z);
        var joystickVector = joystick.Direction;

        var right = (carVector - joystickVector).sqrMagnitude > 0;
        var angle = Vector2.SignedAngle(carVector, joystickVector);

        float calculateAngle = Mathf.Clamp((right ? angle : -angle) * speed, -tiltAngleY, tiltAngleY);

        foreach (var wheel in FrontWheels)
        {
            var rotation = wheel.localRotation;
            Quaternion target = Quaternion.Euler(0, calculateAngle, 0);
            wheel.localRotation = Quaternion.RotateTowards(rotation, target, Time.deltaTime * smooth);
        }
    }
}