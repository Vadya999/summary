using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColtController : MonoBehaviour
{
    [SerializeField] private GameObject _colt1;
    [SerializeField] private GameObject _colt2;

    [SerializeField] private Transform _bulletPos;

    [SerializeField] private GameObject _bulletPrefab;

    private bool _colt1or2;

    private void Update()
    {
       SetColt();
       
       Shot();
    }

    private void SetColt()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _colt1or2 = !_colt1or2;
        }

        if (_colt1or2)
        {
            _colt1.SetActive(true);
            _colt2.SetActive(false);
        }
        else
        {
            _colt2.SetActive(true);
            _colt1.SetActive(true);
        }
    }

    private void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Instantiate(_bulletPrefab, _bulletPos.position, Quaternion.identity);
        }
    }
}
