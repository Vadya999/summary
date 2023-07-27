using UnityEngine;

public class MobAnimationComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private ParticleSystem _skatingParticles;

    private readonly int _isWalkingBool = Animator.StringToHash("is-walking");
    private readonly int _isSkatingBool = Animator.StringToHash("is-skating");

    private Vector3 _lastFraimPosition;

    public void ShowSkating()
    {
        SetWalking(false);
        SetSkating(true);
    }

    public void ShowStop()
    {
        SetWalking(false);
        SetSkating(false);
    }

    public void ShowWalking()
    {
        SetWalking(true);
        SetSkating(false);
    }

    private void Update()
    {
        var direction = transform.position - _lastFraimPosition;
        direction.y = 0;
        if (direction == Vector3.zero) return;
        var targetRotation = Quaternion.LookRotation(direction);
        var smoothRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        transform.rotation = smoothRotation;

        _lastFraimPosition = transform.position;
    }

    private void SetWalking(bool value)
    {
        _animator.SetBool(_isWalkingBool, value);
    }

    private void SetSkating(bool value)
    {
        _animator.SetBool(_isSkatingBool, value);
        _skatingParticles.gameObject.SetActive(value);
    }
}
