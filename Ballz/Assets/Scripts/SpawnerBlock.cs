using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerBlock : MonoBehaviour
{
    public Block blockPrefab;
    private int playWidth = 8;
    private float distanceBetweenBlock = 0.7f;
    private int rowsSpawned;

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnRowOfBlock();
        }
    }

    private void SpawnRowOfBlock()
    {
        for (int i = 0; i < playWidth; i++)
        {
            if (UnityEngine.Random.Range(0,100) <= 30)
            {
                var block = Instantiate(blockPrefab, GetPosition(i), quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 3) + rowsSpawned;
                block.SetHits(hits);
            }
        }

        rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distanceBetweenBlock;
        return position;
    }
}
