using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerBilder : MonoBehaviour
{
    [SerializeField] private int _levelCount;//размер уровня по у вниз
    [SerializeField] private GameObject _beam;
    [SerializeField] private float additionalScale;//кончик башни
    [SerializeField] private Platform[] _platform;
    [SerializeField] private SpawnPlatform _spawnPlatform;
    [SerializeField] private FinishPlatform _finishPlatform;
    private float _startAndFinishAdditionalScale = 0.5f;

    public float BeamScaleY => _levelCount / 2f + _startAndFinishAdditionalScale + additionalScale / 2f;

    private void Awake()
    {
        Build();
    }

    private void Build()
    {
        GameObject beam = Instantiate(_beam, transform);
        beam.transform.localScale = new Vector3(1,BeamScaleY,1);//на каждый левел прибавляем 0,5. т.к при изменении локал скейла на 1 он увеличивается в 2 раза

        Vector3 spawnPosition = beam.transform.position;
        spawnPosition.y += beam.transform.localScale.y - additionalScale;//переместились в самое начало башни
        
        SpawnPlatform(_spawnPlatform,ref spawnPosition,beam.transform);

        for (int i = 0; i < _levelCount; i++)
        {
            SpawnPlatform(_platform[Random.Range(0,_platform.Length)],ref spawnPosition,beam.transform);
        }
        
        SpawnPlatform(_finishPlatform,ref spawnPosition,beam.transform);
    }

    private void SpawnPlatform(Platform platform, ref Vector3 spawnPosition, Transform parent)
    {
        Instantiate(platform, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), parent);
        spawnPosition.y -= 1;
    }
}
