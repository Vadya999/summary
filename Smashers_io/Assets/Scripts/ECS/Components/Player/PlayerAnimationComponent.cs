using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationComponent : GenericAnimationComponent
{
    private readonly int _isHoldingItemBoolID = Animator.StringToHash("IsHolding");

    private readonly int _loseTriggerID = Animator.StringToHash("Lose");

    private readonly int _winTriggerID = Animator.StringToHash("Win");
    private readonly int _winID = Animator.StringToHash("WinID");

    private readonly int _isTrappingBoolID = Animator.StringToHash("IsTrapping");
    private readonly int _trapID = Animator.StringToHash("TrapingID");

    public bool isHoldingItem { set => SetBool(_isHoldingItemBoolID, value); }

    public void ShowWin(int id)
    {
        animator.SetInteger(_winID, id);
        animator.SetTrigger(_winTriggerID);
    }

    public void ShowLose()
    {
        animator.SetTrigger(_loseTriggerID);
    }

    public void SetTrapping(bool trapping, PlayerTrapID id)
    {
        animator.SetInteger(_trapID, (int)id);
        SetBool(_isTrappingBoolID, trapping);
    }
}
