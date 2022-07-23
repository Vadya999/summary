using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float _levelHeight;
    [SerializeField] private float _levelWidth;

    [SerializeField] private GameObject _planePrefab;
    [SerializeField] private GameObject _wallPrefab;
    
    private GameObject _plane;
    
    private void Start()
    {
        InstantiatePlane();
    }
    
    private void InstantiatePlane()
    {
        _plane = Instantiate(_planePrefab, Vector3.zero, Quaternion.identity);
        
        Vector3 transformLocalScale = _plane.transform.localScale;
        transformLocalScale.x = _levelWidth;
        transformLocalScale.y = _plane.transform.localScale.y;
        transformLocalScale.z = _levelHeight;
        _plane.transform.localScale = transformLocalScale;
    }
}
