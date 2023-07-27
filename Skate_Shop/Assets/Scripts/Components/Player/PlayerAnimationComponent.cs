using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<GameObject> _rollers;

    private int _walkingFloatID = Animator.StringToHash("IsWalking");
    private int _hasStackBoolID = Animator.StringToHash("HasStack");
    private int _onLadderBoolID = Animator.StringToHash("OnLadder");
    private int _onRampBoolID = Animator.StringToHash("OnRamp");

    public bool isWalking { set => _animator.SetBool(_walkingFloatID, value); }
    public bool hasStack { set => _animator.SetBool(_hasStackBoolID, value); }
    public bool onRamp { set => _animator.SetBool(_onRampBoolID, value); }

    public bool onLadder
    {
        set
        {
            _animator.SetBool(_onLadderBoolID, value);
            _rollers.ForEach(x => x.SetActive(!value));
        }
    }
}
