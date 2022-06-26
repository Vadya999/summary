using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerBilder : MonoBehaviour
{
    [SerializeField] private float _towerSize;
    [SerializeField] private Transform _bildPoint;
    [SerializeField] private Block _block;

    private List<Block> _blocks;
    public List<Block> Build()
    {
        _blocks = new List<Block>();
        Transform currentPoint = _bildPoint;

        for (int i = 0; i < _towerSize; i++)
        {
            Block newBlock = BuildBlock(currentPoint);
            _blocks.Add(newBlock);
            currentPoint = newBlock.transform;
        }

        return _blocks;
    }

    private Block BuildBlock(Transform currentBuildPint)
    {
        return Instantiate(_block, GetBuildPoint(currentBuildPint), Quaternion.identity, _bildPoint);
    }

    private Vector3 GetBuildPoint(Transform currentSegment)
    {
        return new Vector3(_bildPoint.position.x,currentSegment.position.y + currentSegment.localScale.y /2 + _block.transform.localScale.y/2,_bildPoint.position.z);
    }
}
