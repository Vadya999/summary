using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using System.Collections;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] private PlayerAnimationComponent _animation;
    [SerializeField] private float floatt;

    private Joystick _joystick;

    private Joystick joystick
    {
        get => _joystick ??= FindObjectOfType<Joystick>();
    }

    public bool isOnLadder { get; set; }

    private Rigidbody _rigidbody;
    private float stickAngle;
    private float joystickInput;

    private bool _isMovementLocked;

    private bool isMovementLocked
    {
        get => _isMovementLocked;
        set
        {
            _isMovementLocked = value;
            _rigidbody.isKinematic = value;
            if (value) _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isMovementLocked) return;
        _animation.onLadder = isOnLadder;
        _animation.hasStack = GameData.player.skatesRoot.hasItem;
        PrepareToMove();
    }

    private void PrepareToMove()
    {
        const float gravity = 5f;
        if (isMovementLocked) return;
        var isJoystickActivated = IsJoystickActivated();

        if (isOnLadder && isJoystickActivated == false)
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y - (gravity * Time.deltaTime), 0);
        }
        else
        {
            _rigidbody.velocity = GetMoveVelocity();
        }

        UpdateAnimator();
    }

    private Vector3 GetMoveVelocity()
    {
        stickAngle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
        stickAngle += floatt;

        if (IsJoystickActivated())
        {
            transform.rotation = Quaternion.AngleAxis(stickAngle, new Vector3(0, 1, 0));
        }

        joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical).normalized.magnitude;
        var velocity = transform.forward * joystickInput * GameData.playerSpeed;
        return new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    private bool IsJoystickActivated()
    {
        return joystick.Horizontal != 0 || joystick.Vertical != 0;
    }

    private void UpdateAnimator()
    {
        _animation.isWalking = joystick.Direction.normalized.magnitude != 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayersRailComponent rail))
        {
            StartCoroutine(RailRoutine(rail));
        }
    }

    private IEnumerator RailRoutine(PlayersRailComponent rail)
    {
        isMovementLocked = true;
        GameData.player.skatesRoot.gameObject.SetActive(false);
        _animation.hasStack = false;
        _animation.onRamp = true;
        if (GameData.settings.hapticEnabled) MMVibrationManager.ContinuousHaptic(0.1f, 0.1f, 20);

        yield return DoRail(rail);

        if (GameData.settings.hapticEnabled) MMVibrationManager.StopContinuousHaptic();
        GameData.player.skatesRoot.gameObject.SetActive(true);
        _animation.onRamp = false;
        isMovementLocked = false;
    }

    private IEnumerator DoRail(PlayersRailComponent rail)
    {
        for (int i = 0; i < rail.onRailPath.path.Length - 1; i++)
        {
            var currentPoint = rail.onRailPath.path[i] - Vector3.up * 0.2f;
            var nextPoint = rail.onRailPath.path[i + 1] - Vector3.up * 0.2f;
            var duration = Vector3.Distance(currentPoint, nextPoint) / 7.5f;

            var lookDirection = nextPoint - currentPoint;
            lookDirection.y = 0;
            var rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(Vector3.up * -90);

            transform.DORotate(rotation.eulerAngles, duration);

            yield return transform
                .DOMove(nextPoint, duration)
                .SetEase(Ease.Linear)
                .WaitForCompletion();
        }
    }
}