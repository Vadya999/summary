using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PathCreator _pathCreator;

    private float _distanceTravelled;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.MovePosition(_pathCreator.path.GetPointAtDistance(_distanceTravelled));//перемещаем игрока в начало пути, эта точка - начало пути
        
    }

    private void Update()
    {
        _distanceTravelled += Time.deltaTime * _speed;//сеняем точку в завис от времени и скорости 

        Vector3 nextPoint = _pathCreator.path.GetPointAtDistance(_distanceTravelled, EndOfPathInstruction.Loop);
        nextPoint.y = transform.position.y;
        
        transform.LookAt(nextPoint);
        _rigidbody.MovePosition(nextPoint);
    }
    
    
}
