using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveuor : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _endPoint;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, _endPoint.transform.position,
            _speed * Time.deltaTime);
    }
}
