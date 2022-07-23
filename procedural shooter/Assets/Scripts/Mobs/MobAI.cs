using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MobAI : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    
    private NavMeshAgent _agent;

    private float _hp = 5;
    private float _damage = 1;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(_targetTransform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnDamage()
    {
        _hp -= _damage;
        if (_hp == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
