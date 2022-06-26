using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    private TowerBilder _towerBilder;
    private List<Block> _blocks;
    public event UnityAction<int> SizeUpdated;

    private void Start()
    {
        _towerBilder = GetComponent<TowerBilder>();
        _blocks = _towerBilder.Build();

        foreach (var block in _blocks)
        {
            block.BulletHit += OnBulletHit; //пишем каким методом будем обрабатывать событие +=
        }
        SizeUpdated?.Invoke(_blocks.Count);//старатовое значение счетчика
    }

    private void OnBulletHit(Block hitBlock)
    {
        hitBlock.BulletHit -= OnBulletHit;// теперь отписываемся от события -=

        _blocks.Remove(hitBlock);

        foreach (var block in _blocks)
        {
            block.transform.position = new Vector3(transform.position.x,block.transform.position.y - block.transform.localScale.y,transform.position.z);
        }
        
        SizeUpdated?.Invoke(_blocks.Count);
    }
}
