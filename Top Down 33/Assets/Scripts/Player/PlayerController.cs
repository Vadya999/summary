using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private float _speed = 4f; 

    private Vector3 _direction;

    private RaycastHit _hit;

    private bool _gameOver;
    private bool _loseSpeed;
    private void Update()
    {
        CalculateDirection();
    }

    private void FixedUpdate()
    {
        if (_loseSpeed)
        {
            _rigidbody.velocity = _direction * _speed * 0.6f;

            _loseSpeed = false;
        }
        else
        {
            _rigidbody.velocity = _direction * _speed;
        }
        
    }

    public void Rotate()
    {
        StartCoroutine("RotateCor");
    }
    private IEnumerator RotateCor()
    {
        yield return new WaitForSeconds(1f);
        
        Vector3 mouse = Input.mousePosition;
        
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        if (Physics.Raycast(castPoint, out _hit, Mathf.Infinity));
        transform.LookAt(_hit.point);
        
        yield break;
    }

    private void CalculateDirection()
    {
        float ver = Input.GetAxis("Horizontal");
        float hor = Input.GetAxis("Vertical");
        
        _direction = new Vector3(ver,0,hor).normalized;
    }

    public void IsGameOver()
    {
        _gameOver = true;
    }

    public void LoseSpeed()
    {
        _loseSpeed = true;
    }
    
}
