using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChairAnimationComponent : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowBroken()
    {
        _animator.SetTrigger("Broke");
    }

    public void Fix()
    {
        _animator.SetTrigger("Fix");
    }
}
