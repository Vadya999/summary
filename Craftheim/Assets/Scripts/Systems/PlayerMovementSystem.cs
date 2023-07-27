 using Kuhpik;
 using NaughtyAttributes;
 using Source.Scripts.UI;
 using UnityEngine;

 namespace Source.Scripts.Systems
 {
     public class PlayerMovementSystem : GameSystemWithScreen<GameUIScreen>
     {
         [SerializeField] private float baseMoveSpeed = 6f;
         [SerializeField] private bool needRotateTowards;
         [SerializeField] [ShowIf("needRotateTowards")] private float rotateSpeed = 6f;

         private static readonly int Speed = Animator.StringToHash("Speed");
         private float speed;

         public override void OnInit()
         {
             game.playerComponent.PlayerDragItemComponent.OnChanged += CheckAnimation;
         }

         public override void OnFixedUpdate()
         {
             if (!game.canMove)
             {
                 speed = 0;
                 game.playerComponent.Rigidbody.velocity = Vector3.zero;
                 return;
             }

             var joystickDirection = screen.Joystick.Direction;
             
             if (joystickDirection == Vector2.zero)
             {
                 speed = 0;
                 game.playerComponent.Rigidbody.velocity = Vector3.zero;
                 return;
             }
             
             Move(joystickDirection, Time.fixedDeltaTime);
         }
         
         public override void OnLateUpdate()
         {
             UpdateAnimation(game.canMove);
         }

         private void UpdateAnimation(bool canMove)
         {
             game.playerComponent.Animator.SetFloat(Speed, canMove ? screen.Joystick.Direction.magnitude : 0);
         }

         private void CheckAnimation(bool isFull)
         {
             game.playerComponent.Animator.SetLayerWeight(1, isFull ? 1 : 0);
             game.playerComponent.Animator.transform.localRotation = new Quaternion(0,isFull ? 180 : 0,0, 0);
         }

         private void Move(Vector2 joystickDirection, float deltaTime)
         {
             var rb = game.playerComponent.Rigidbody;
             var charForward = rb.transform.forward;

             //Current speed
             speed = baseMoveSpeed * joystickDirection.magnitude;

             //Move
             var velocity = charForward * speed;
             rb.velocity = velocity;

             //Rotate
             var direction = new Vector3(joystickDirection.x, 0, joystickDirection.y).normalized;

             if (needRotateTowards)
             {
                 var singleStep = rotateSpeed * deltaTime;
                 Vector3 newDirection = Vector3.RotateTowards(charForward, direction, singleStep, 0.0f);
                 rb.transform.rotation = Quaternion.LookRotation(newDirection);
             }
             else
             {
                 rb.transform.rotation = Quaternion.LookRotation(direction);
             }
         }
     }
 }