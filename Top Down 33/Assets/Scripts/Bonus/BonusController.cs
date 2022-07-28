using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusController : MonoBehaviour
{
    [SerializeField] private GameObject[] _guns;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < _guns.Length; i++)
            {
                _guns[i].SetActive(false);
            }
            
            _guns[Random.Range(0,_guns.Length)].SetActive(true); 
            
            Destroy(gameObject);
        }
    }
}
