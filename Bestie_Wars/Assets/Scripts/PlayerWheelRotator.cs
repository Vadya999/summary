using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheelRotator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Joystick Joystick;

    private float GetEulerAnglesFromJoystick()
    {
        return Mathf.Atan2(Joystick.Horizontal, Joystick.Vertical) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        var y = GetEulerAnglesFromJoystick();
        if (y == 0) return;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,y,transform.rotation.eulerAngles.z)),speed);
    }
}