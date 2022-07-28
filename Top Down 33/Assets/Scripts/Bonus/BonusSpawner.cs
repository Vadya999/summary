using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonus;

    [SerializeField] private Transform[] _bonusPos;

    private float _nextActionTime = 0.0f;
    private float _period = 10f;

    private void Update()
    {
        if (Time.time > _nextActionTime)
        {
            _nextActionTime += _period;
            SpawBounus();
        }
    }

    private void SpawBounus()
    {
        Instantiate(_bonus[Random.Range(0, _bonus.Length)], _bonusPos[Random.Range(0, _bonusPos.Length)].position,
            Quaternion.identity);
    }
}
