 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Random = UnityEngine.Random;

 public class DangerZonesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private GameObject _loswSpeedZone;

    [SerializeField] private Transform[] _spawnPos;

    private float _numDeadZone = 2;
    private float _numLosSpeedZone = 3;

    private void Start()
    {
        for (int i = 0; i < _numDeadZone; i++)
        {
            Instantiate(_deadZone, _spawnPos[Random.Range(0, _spawnPos.Length)].position, Quaternion.identity);
        }

        for (int i = 0; i < _numLosSpeedZone; i++)
        {
            Instantiate(_loswSpeedZone, _spawnPos[Random.Range(0, _spawnPos.Length)].position, Quaternion.identity);
        }
    }
}
