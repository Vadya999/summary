using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadLengh = _pathCreator.path.length;//размер дороги
        float distanceBetweenTower = roadLengh / _humanTowerCount;//дистанция меж башнами

        float distanceTraveled = 0;//сколько дистанций прогенерировали
        Vector3 spawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTraveled += distanceBetweenTower;//
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);

            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
        }
    }
}
