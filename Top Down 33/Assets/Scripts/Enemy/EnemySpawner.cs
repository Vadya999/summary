using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy1;
    [SerializeField] private GameObject _enemy2;
    [SerializeField] private GameObject _enemy3;

    [SerializeField] private float _enemy1SpawnChance;
    [SerializeField] private float _enemy2SpawnChance;
    [SerializeField] private float _enemy3SpawnChance;

    [SerializeField] private Transform[] _spawnPositions;

    private float _nextActionTime = 0.0f;
    private float _period = 2f;
    private float _nextActionTime2 = 0.0f;
    private float _period2 = 10f;

    private bool isMinValue;

    private float _randomValue;

    private void Update()
    {
        if (Time.time > _nextActionTime)
        {
            _nextActionTime += _period;

            SpawnEnemy();
        }

        if (Time.time > _nextActionTime2)
        {
            _nextActionTime2 += _period2;

            if (!isMinValue)
            {
                _period -= 0.1f;
            }
            
            if (_period == 0.5f)
            {
                isMinValue = true;
            }
        }
    }

    private void SpawnEnemy()
    {
        _randomValue = Random.Range(1, 100);

        if (_randomValue < _enemy1SpawnChance)
        {
            Instantiate(_enemy1, _spawnPositions[Random.Range(0, _spawnPositions.Length)].position, Quaternion.identity);
        }
        
        else if (_randomValue < _enemy2SpawnChance)
        {
            Instantiate(_enemy2, _spawnPositions[Random.Range(0, _spawnPositions.Length)].position, Quaternion.identity);
        }
        
        else if (_randomValue < _enemy3SpawnChance)
        {
            Instantiate(_enemy3, _spawnPositions[Random.Range(0, _spawnPositions.Length)].position, Quaternion.identity);
        }
    }
}
