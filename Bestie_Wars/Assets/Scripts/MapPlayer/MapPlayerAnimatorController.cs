using UnityEngine;

public class MapPlayerAnimatorController
{
    private Animator animator;

    public MapPlayerAnimatorController(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayAnimation(MapPlayerAnimationType mapPlayerAnimationType, bool value)
    {
        animator.SetBool(mapPlayerAnimationType.ToString(), value);
    }
}