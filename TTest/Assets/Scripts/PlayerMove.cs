using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private Transform _playerCamera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        Rotate();
        Move();
    }
    
    private void Move()
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, _moveDirection.y,
            Input.GetAxis("Vertical") * speed);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(0,   Input.GetAxis("Mouse X") * rotateSpeed, 0);
        _playerCamera.Rotate(-Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);

        if (_playerCamera.localRotation.eulerAngles.y != 0)
        {
            _playerCamera.Rotate(Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);
        }
    }
}
