using System.Collections;
using UnityEngine;

public class DoorAnimationComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _openTriggerID = Animator.StringToHash("Open");

    public void Open()
    {
        FindObjectOfType<PlayerAnimationComponent>().StartCoroutine(Openx());
        _animator.SetBool(_openTriggerID, true);
    }

    private IEnumerator Openx()
    {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool(_openTriggerID, true);
    }
}
