using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorDestroyer : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed*Time.deltaTime));
    }
    
}
