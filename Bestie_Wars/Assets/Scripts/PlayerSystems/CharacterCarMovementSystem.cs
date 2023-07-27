using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CharacterCarMovementSystem : GameSystem
{
    [SerializeField] private Transform car;
    [SerializeField] private List<ParticleSystem> clouds;
    [SerializeField] private Transform cran;
    [SerializeField] private Transform cranAttachPosition;
    [SerializeField] private CharacterConfiguration configuration;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody playerRigidBody;

    [SerializeField] private Vector3 rotateInverce;
    [SerializeField] private Vector3 rotateInverceMove;

    private CameraSystem cameraSystem;
    private Vector3 startScale;
    private bool isShakle = false;
    private bool isInState;
    private bool isStopped;

    private Dictionary<PlayerSpeedModification, float> modifications = new Dictionary<PlayerSpeedModification, float>();

    public Rigidbody Rigidbody => playerRigidBody;
    public Transform CranAttachPosition => cranAttachPosition;
    public Transform Cran => cran;

    private void Awake()
    {
        startScale = playerRigidBody.transform.localScale;
    }

    public override void OnInit()
    {
        cameraSystem = Bootstrap.Instance.GetSystem<CameraSystem>();
    }

    private void FixedUpdate()
    {
        if (isInState == false) return;
        Move(joystick.Direction);
        Rotate(joystick.Direction, Time.fixedDeltaTime);
        CheckShake();
        CheckInverce(joystick.Direction);
    }

    public void CheckInverce(Vector2 direction)
    {
        if (direction == Vector2.zero && isStopped == false)
        {
            var last = car.rotation;
            isStopped = true;
            var sequcene = DOTween.Sequence();
            sequcene.Append(car.transform.DOLocalRotate(rotateInverce, 0.2f));
            sequcene.Append(car.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.6f));
        }

        if (isStopped && direction != Vector2.zero)
        {
            car.DOKill();
            isStopped = false;
            car.transform.DOLocalRotate(rotateInverceMove, 0.2f);
        }
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        isInState = true;
        Rigidbody.isKinematic = false;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        isInState = false;
        Rigidbody.isKinematic = true;
    }

    private void CheckShake()
    {
        if (joystick.Direction == Vector2.zero && isShakle == false)
        {
            if (isShakle == false && modifications.All(t => t.Value != 0))
            {
                isShakle = true;
                StartCoroutine(Shackle());
                cameraSystem.ChangeCarCamera(CameraType.Move);
                foreach (var cloud in clouds)
                {
                    cloud.Play();
                }
            }
        }
        else
        {
            if (joystick.Direction != Vector2.zero || modifications.All(t => t.Value != 0) == false)
            {
                StopAllCoroutines();

                cameraSystem.ChangeCarCamera(CameraType.Idle);
                isShakle = false;
                playerRigidBody.transform.DOKill();
                playerRigidBody.transform.localScale = startScale;
                foreach (var cloud in clouds)
                {
                    cloud.Stop();
                }
            }
        }
    }

    public void AddModification(PlayerSpeedModification playerSpeedModification, float param)
    {
        if (modifications.ContainsKey(playerSpeedModification) == false)
        {
            modifications.Add(playerSpeedModification, 0);
        }

        modifications[playerSpeedModification] = param;
    }

    private IEnumerator Shackle()
    {
        while (true)
        {
            playerRigidBody.transform.DOShakeScale(0.3f, 0.05f)
                .OnComplete(() => playerRigidBody.transform.localScale = Vector3.one);
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void Rotate(Vector2 joystickDirection, float deltaTime)
    {
        var rb = playerRigidBody;
        var charForward = rb.transform.forward;
        var direction = new Vector3(joystickDirection.x, 0, joystickDirection.y).normalized;
        var speed = configuration.RotateSpeed * deltaTime;
        foreach (var modification in modifications)
        {
            speed *= modification.Value;
        }

        var newDirection = Vector3.RotateTowards(charForward, direction, speed, 0.0f);
        rb.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), 1);
    }

    private void Move(Vector2 joystickDirection)
    {
        var rb = playerRigidBody;
        var charForward = rb.transform.forward;
        var speed = configuration.Speed;
        foreach (var modification in modifications)
        {
            speed *= modification.Value;
        }

        speed *= joystickDirection.magnitude;
        var velocity = charForward * (speed * Time.deltaTime);
        rb.velocity = velocity;
    }
}