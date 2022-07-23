using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private Transform[] _bonusPos;

    [SerializeField] private GameObject[] _bonus;

    private int _numOfObstacles;
    private int _numBonus = 2;

    private void Start()
    {
        SpawnObstacles();
        
        SpawnBonus();
    }

    private void SpawnObstacles()
    {
        _numOfObstacles = Random.Range(0, _obstacles.Length);
        
                for (int i = 0; i < _numOfObstacles; i++)
                {
                    Destroy(_obstacles[i]);
                }
                
    }

    private void SpawnBonus()
    {
        for (int i = 0; i < _numBonus; i++)
        {
            Instantiate(_bonus[Random.Range(0,_bonus.Length)], _bonusPos[Random.Range(0,_bonusPos.Length)].position, Quaternion.identity);
        }
    }
}
