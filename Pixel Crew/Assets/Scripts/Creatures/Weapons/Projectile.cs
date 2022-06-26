using System;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody2D;
        private int _direction;

        public void Start()
        {
            _direction = transform.lossyScale.x > 0 ? 1 : -1;//true scale 
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var position = _rigidbody2D.position;
            position.x += _direction * _speed;
            _rigidbody2D.MovePosition(position);
        }
    }
}