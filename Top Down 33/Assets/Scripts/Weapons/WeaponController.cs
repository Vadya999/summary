using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    public UnityEvent _action;

    public static Vector3 hitPos;
    
    private Vector2 _mousePos;
    private Vector2 _rbPosVec2;
    
    private RaycastHit _hit;
    private void Update()
    {
        Rotate();

        OnShoot();
    }
    private void Rotate()
    {
        Vector3 mouse = Input.mousePosition;
        
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (Physics.Raycast(castPoint, out _hit, Mathf.Infinity));
        transform.LookAt(_hit.point);

        hitPos = _hit.point;
    }

    private void OnShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
             _action?.Invoke();
        }
    }
}
