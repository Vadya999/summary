using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementComponent : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Transform _targetTransform;

    [SerializeField] private PlayerController _player;

    private Vector3 _direction;

    private void FixedUpdate()
    {
        _direction = _targetTransform.position - transform.position;

        _rigidbody.velocity = _direction.normalized * _moveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.IsGameOver();
        }
    }
}
