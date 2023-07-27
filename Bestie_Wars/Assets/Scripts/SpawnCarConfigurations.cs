using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class SpawnCarConfigurations
{
    [SerializeField] private int maxCar = 5;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private List<AttachCarController> carPrefabs;

    public int MAXCar => maxCar;

    public List<Transform> SpawnPositions => spawnPositions;

    public List<AttachCarController> CarPrefabs => carPrefabs;

    private int maxAmountSpawnCar = 0;
    private Dictionary<Transform, AttachCarController> spawnedCar = new Dictionary<Transform, AttachCarController>();

    public int MAXAmountSpawnCar
    {
        get => maxAmountSpawnCar;
        set => maxAmountSpawnCar = value;
    }

    public Dictionary<Transform, AttachCarController> SpawnedCar
    {
        get => spawnedCar;
        set => spawnedCar = value;
    }
    
}