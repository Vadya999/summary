using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class BallJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForece;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlatformSegment platformSegment))
        {
            //_rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector3.up * _jumpForece);
        }
    }
}
