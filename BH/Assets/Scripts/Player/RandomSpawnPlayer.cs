using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _player.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
    }
}
