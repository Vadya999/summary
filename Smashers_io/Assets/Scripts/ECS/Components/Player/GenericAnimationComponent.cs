using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GenericAnimationComponent : MonoBehaviour
{
    private Animator _animator;

    private readonly int _doorOpenTriggerID = Animator.StringToHash("DoorOpen");
    private readonly int _isWalkingBoolID = Animator.StringToHash("IsWalking");

    public bool isWalking { set => SetBool(_isWalkingBoolID, value); }
    public bool isManual { get; set; }

    public Animator animator => _animator;

    private GameObject _root;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowManualAnimation(IEnumerator routine)
    {
        if (!isManual)
        {
            StartCoroutine(ShowManualAnimationRoutine(routine));
        }
    }

    public void BeginRoomMotion()
    {
        animator.applyRootMotion = true;
    }

    public void StopRootMotion(GameObject root)
    {
        var position = animator.transform.position;
        root.transform.position = position;
        animator.transform.localPosition = Vector3.zero;
        animator.applyRootMotion = false;
    }

    public void ShowDoorOpen()
    {
        _animator.SetTrigger(_doorOpenTriggerID);
    }

    public void ForceWalking(bool value)
    {
        _animator.SetBool(_isWalkingBoolID, value);
    }

    private IEnumerator ShowManualAnimationRoutine(IEnumerator routine)
    {
        isManual = true;
        yield return routine;
        isManual = false;
    }

    public void SetBool(int id, bool value)
    {
        if (!isManual)
        {
            _animator.SetBool(id, value);
        }
    }
}
