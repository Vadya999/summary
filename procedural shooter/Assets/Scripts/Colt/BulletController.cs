using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _lifeTime;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(Vector3.forward * _bulletSpeed);
        
        Destroy(this.gameObject, _lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.TryGetComponent(out MobAI mobAi);
            mobAi.OnDamage();

        }
    }
}
