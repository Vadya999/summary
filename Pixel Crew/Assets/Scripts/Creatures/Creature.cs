using System.Collections;
using System.Collections.Generic;
using PixelCrew.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace PixelCrew.Creatures
{
   public class Creature : MonoBehaviour
   {
       [Header("Params")] [SerializeField] private bool _inverScale;
       [SerializeField] private float _speed;
       [SerializeField] protected float _jumpSpeed;
       [SerializeField] private float _damageVelocity;
       [SerializeField] private int _damage;

       [Header("Checkers")] 
       [SerializeField] protected LayerMask _groundLayer;
       [SerializeField] private LayerCheck _groundCheck;
       [SerializeField] private CheckCircleOverlap _attackRange;
       [SerializeField] protected SpawnListComponent _particles;

       protected Rigidbody2D rigidbody;
       protected Vector2 _direction;
       protected Animator animator;
       protected bool isGrounded;
       private bool _isJumping;
       
       private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
       private static readonly int IsRunning = Animator.StringToHash("is-running");
       private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
       private static readonly int Hit = Animator.StringToHash("hit");
       private static readonly int AttackKey = Animator.StringToHash("attack");
       

       protected virtual void Awake()
       {
           rigidbody = GetComponent<Rigidbody2D>();
           animator = GetComponent<Animator>();
       }

       public void SetDirection(Vector2 direction)
       {
           this._direction = direction;
       }

       protected virtual void Update()
       {
           isGrounded = _groundCheck.IsTouchingLayer;
       }

       private void FixedUpdate()
       {
           var xVelocity = _direction.x * _speed;
           var yVelocity = CalculateYVelocity();
           rigidbody.velocity = new Vector2(xVelocity,yVelocity);
            
           animator.SetBool(IsGroundKey, isGrounded);
           animator.SetBool(IsRunning,_direction.x != 0);
           animator.SetFloat(VerticalVelocity, rigidbody.velocity.y);
            
           UpdateSpriteDirection();
       }

       protected virtual float CalculateYVelocity()
       {
           var yVelocity = rigidbody.velocity.y;
           var isJumpPressing = _direction.y > 0;

           if (isGrounded)
           {
               _isJumping = false;
           }

           if (isJumpPressing)
           {
               _isJumping = true;
               
               var isFalling = rigidbody.velocity.y <= 0.001f;
               yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
           }
           else if (rigidbody.velocity.y > 0 && _isJumping)
           {
               yVelocity *= 0.5f;
           }

           return yVelocity;
       }

       protected virtual float CalculateJumpVelocity(float yVelocity)
       {
           if (isGrounded)
           {
               yVelocity = _jumpSpeed;
               _particles.Spawn("Jump");
           }

           return yVelocity;
       }

       private void UpdateSpriteDirection()
       {
           var multipLier = _inverScale ? -1 : 1;
           if (_direction.x > 0 )
           {
               transform.localScale = new Vector3(multipLier,1,1);
           }
           else if (_direction.x < 0 )
           {
               transform.localScale = new Vector3(-1 * multipLier,1,1);
           }
       }

       public virtual void TakeDamage()
       {
           _isJumping = false;
           animator.SetTrigger(Hit);
           rigidbody.velocity = new Vector2(rigidbody.velocity.x,_damageVelocity);
       }
 
       public virtual void Attack()
       {
           animator.SetTrigger(AttackKey);
       }

       public void OnDOAttack()
       {
           _attackRange.Check();
       }
   }
}