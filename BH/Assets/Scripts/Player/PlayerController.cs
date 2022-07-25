using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Animator _animator;

    private Rigidbody _rigidbody;

    private Vector3 _direction;

    private UnityEvent _action;

    public float score;

    public int health;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasAuthority)
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            _direction.x = hor;
            _direction.y = ver;

            _direction = new Vector3(hor, 0, ver);

            if (_direction.magnitude > Mathf.Abs(0.1f))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction),
                    Time.deltaTime * _rotationSpeed);
            }

            _animator.SetFloat("speed", _direction.normalized.magnitude);
        }
    }

    private void FixedUpdate()
    {
        if (hasAuthority)
        {
            _rigidbody.velocity = _direction.normalized * _speed;
        }
    }

    [SyncVar(hook = nameof(SyncHealth))] 
    int _SyncHealth;


    void SyncHealth(int oldValue, int newValue)  
    {
        health = newValue;
    }

    [Server]
    public void ChangeHealthValue(int newValue)
    {
        _SyncHealth = newValue;
    }
    
    [Command] 
    public void CmdChangeHealth(int newValue) 
    {
        ChangeHealthValue(newValue); 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _action?.Invoke();

            score++;
        }
    }
    
    public void OnChangeHp()
    {
        if (isServer)
            ChangeHealthValue(health - 1);
        else
            CmdChangeHealth(health - 1);
    }
}
