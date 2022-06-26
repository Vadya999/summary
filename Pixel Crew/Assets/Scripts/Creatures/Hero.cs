using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrew.Components;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace PixelCrew.Creatures
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private float slamDownVelocity;
        [SerializeField] private int _damage;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private LayerCheck _wallCheck;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;

        [SerializeField] private CheckCircleOverlap _ineractionCheck; 

        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [SerializeField] private CheckCircleOverlap _attackRange;

        [Space] [Header("Particles")] [SerializeField]
        private SpawnComponent _footStepParticles;

        [SerializeField] private SpawnComponent _jumpParticles;
        [SerializeField] private SpawnComponent _slamDownParticles;
        [SerializeField] private ParticleSystem _hitParticles;

        private readonly Collider2D[] _interactionResult = new Collider2D[1];
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Animator _animator;
        private bool _isGrounded;
        private bool _allowDobleJump;
        private bool _isJumping;
        private bool _isOnWall;
        
        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");
        private static readonly int TrowKey = Animator.StringToHash("trow");

        private GameSession _session;
        private float _defaultGravityScale;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _defaultGravityScale = _rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = GetComponent<GameSession>();
            var health = GetComponent<HealthComponent>();
            
            //health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            _isGrounded = IsGrounded();

            if (_wallCheck.IsTouchingLayer && _direction.x == transform.localScale.x)
            {
                _isOnWall = true;
                _rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                _rigidbody.gravityScale = _defaultGravityScale;
            }
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity,yVelocity);
            
            _animator.SetBool(IsGroundKey,_isGrounded);
            _animator.SetBool(IsRunning,_direction.x !=0);
            _animator.SetFloat(VerticalVelocity,_rigidbody.velocity.y);

            UpdateSpriteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _allowDobleJump = true;
                _isJumping = false;
            }
            
            if (_isOnWall)
            {
                _allowDobleJump = true;
            }

            if (isJumpPressing)
            {
                _isJumping = true;
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_isOnWall)
            {
                yVelocity = 0;
            }
            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;
            if (_isGrounded)
            {
                yVelocity = _jumpSpeed;
                _jumpParticles.Spawn();
            }
            else if (_allowDobleJump)
            {
                yVelocity = _jumpSpeed;
                _jumpParticles.Spawn();
                _allowDobleJump = false;
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }

            if (_direction.x < 0 )
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }

        private bool IsGrounded()
        {
            var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta,_groundCheckRadius, Vector2.down, 0,
                _groundLayer);
            return hit.collider != null;
        }

        public void AddCoins(int coins)
        {
            _session.Data.Coins += coins;
        }

        public void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger(Hit);

            if (_session.Data.Coins > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);
            _session.Data.Coins -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0,burst);
            
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void Interact()
        {
            _ineractionCheck.Check();

            /*var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult,
                _interactionLayer);
            for (int i = 0; i < size; i++)
            {
                var interaceble = _interactionResult[i].GetComponent<InteractebleComponent>();
                if (interaceble != null)
                {
                    interaceble.Interact();
                }
            }*/
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y >= slamDownVelocity)
                {
                    _slamDownParticles.Spawn();
                }
            }
        }

        public void SpawnFootDust()
        {
            _footStepParticles.Spawn();
        }

        public void Attack()
        {
            //if (!_session.Data.IsArmed) return;
           
            _animator.SetTrigger(AttackKey);
            Debug.Log("aaa");
        }

        public void OnDoAttack()
        {
            _attackRange.Check();
        }
        
        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;
        }

        private void OnDoTrow()
        {
            //_perticles.SwordParticles;
        }
        public void Trow()
        {
            _animator.SetTrigger(TrowKey);
        }
    }
}

