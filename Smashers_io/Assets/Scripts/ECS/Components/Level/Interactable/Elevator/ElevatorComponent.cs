using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    [field: SerializeField] public ElevatorTrigger trigger { get; private set; }
    [field: SerializeField] public TMP_Text progress { get; private set; }
    [field: SerializeField] public Transform starPoint { get; private set; }

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _enterRoot;
    [SerializeField] private Transform _animationStartPoint;

    private readonly int _closeTriggerID = Animator.StringToHash("Close");
    private readonly int _openTriggerID = Animator.StringToHash("Open");

    private bool opened { set => _animator.SetTrigger(value ? _openTriggerID : _closeTriggerID); }

    private bool _state;

    public void SetState(bool state)
    {
        _enterRoot.gameObject.SetActive(!state);
        if (_state == state) return;
        _state = state;
        opened = state;
    }

    public IEnumerator ShowAnimation(PlayerComponent player)
    {
        trigger.gameObject.SetActive(false);
        opened = true;
        player.transform.position = _animationStartPoint.position;
        player.transform.forward = _animationStartPoint.forward;
        player.aniamtion.ForceWalking(true);
        yield return player.transform.DOMove(starPoint.transform.position, 0.5f).WaitForCompletion();
        player.aniamtion.ForceWalking(false);
        opened = false;
        trigger.gameObject.SetActive(true);
    }
}