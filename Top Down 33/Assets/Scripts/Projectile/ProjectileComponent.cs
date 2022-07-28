using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public float scorePlayer;
    
    [SerializeField] private bool _isShootGun;
    [SerializeField] private bool _isGrenadge;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTimeShootGun;

    [SerializeField] private Rigidbody _rigidbody;
    private void FixedUpdate()
    {
        _rigidbody.AddForce(WeaponController.hitPos * _speed);
        
        if (_isShootGun)
        {
            Destroy(gameObject,_lifeTimeShootGun);
        }

        else if (_isGrenadge)
        {
            Destroy(gameObject,_lifeTimeShootGun);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.TryGetComponent(out EnemyHealthAndScore enemyHealth);

            enemyHealth.health -= _damage;
            if (enemyHealth.health < 1f)
            {
                scorePlayer += enemyHealth.score;
                
                Destroy(other.gameObject);
            }
            
            Destroy(gameObject);
        }
    }
}
