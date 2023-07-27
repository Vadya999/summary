using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorAnimationComponent : MonoBehaviour
{
    private Animator _animator;

    private int _openTriggerID = Animator.StringToHash("Open");
    private int _closeTriggerID = Animator.StringToHash("Close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetTrigger(_openTriggerID);
    }

    public void Close()
    {
        _animator.SetTrigger(_closeTriggerID);
    }
}
