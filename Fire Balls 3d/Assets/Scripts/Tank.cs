using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tank : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Bull _bulletTemplate;
    [SerializeField] private float _delayBetweenShots;
    [SerializeField] private float _recoilDistance;

    private float _timeAfterShoot;

    private void Update()
    {
        _timeAfterShoot += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (_timeAfterShoot > _delayBetweenShots)
            {
                Shoot();
                transform.DOMoveZ(transform.position.z - _recoilDistance, _delayBetweenShots / 2).SetLoops(2,LoopType.Yoyo);//длительность время меж выстералми /2
                _timeAfterShoot = 0;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletTemplate, _shotPoint.transform.position, Quaternion.identity);
    }
}
