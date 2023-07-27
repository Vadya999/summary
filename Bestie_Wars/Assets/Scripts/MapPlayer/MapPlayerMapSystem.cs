using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapPlayerMapSystem : GameSystem
{
    [SerializeField] private MapPlayerConfiguration m_mapPlayerConfiguration;
    [SerializeField] private Podium Podium;
    [SerializeField] private List<Transform> spawnPosition;
    [SerializeField] private List<Transform> stopPosition;
    [SerializeField] private List<Transform> exitPositions;
    [SerializeField] private List<MapPlayerController> mapPayerController;

    private List<MapPlayerController> spawnMap = new List<MapPlayerController>();

    private void Awake()
    {
        StartCoroutine(SpawnerMapPlayer());
    }

    private IEnumerator SpawnerMapPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (spawnMap.Count < Podium.AmountPodium * m_mapPlayerConfiguration.AmountCharacterPerCar)
            {
                var spawnPos = spawnPosition[Random.Range(0, spawnPosition.Count)];
                var exitPos = exitPositions[Random.Range(0, exitPositions.Count)];
                var stopPos = stopPosition[Random.Range(0, stopPosition.Count)];
                var randomPlayer = mapPayerController[Random.Range(0, mapPayerController.Count)];

                var player = Instantiate(randomPlayer);
                player.transform.position = spawnPos.position;
                player.SetTarget(stopPos, exitPos,Podium);
                player.mapPlayerDestroy += PlayerDead;
                spawnMap.Add(player);
            }
        }
    }

    private void PlayerDead()
    {
        for (int i = spawnMap.Count - 1; i >= 0; i--)
        {
            if (spawnMap[i] != null)
            {
            }
            else
            {
                spawnMap.RemoveAt(i);
            }
        }
    }
}