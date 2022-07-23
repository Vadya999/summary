using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private TextMeshProUGUI _tmp;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    
    private Rigidbody _rigidbody;

    private Vector3 _direction;

    private float _hpBonus = 10f;
    private float _speedBonus = 2f;

    public float hp = 10;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        
        _direction = new Vector3(hor,0,ver);

         if (_direction.magnitude > Mathf.Abs(0.1f))
         {
             transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction),
                          Time.deltaTime * _rotationSpeed);
         }

         _animator.SetFloat("speed", _direction.normalized.magnitude);

         _tmp.text = hp.ToString();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction.normalized * _speed;
    }

    public void HpBonus()
    {
        hp += _hpBonus;
    }

    public void SpeedBonus()
    {
        _speed += _speedBonus;
    }
}
