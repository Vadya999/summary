using System.Collections;
using System.Collections.Generic;
using Snippets.Tutorial;
using UnityEngine;

public abstract class MoveToTargetStep : TutorialStep
{
    protected virtual Transform player { get; set; }
    protected virtual Transform target { get; set; }
    protected virtual float distance { get; set; } = 4f;

    public override void OnUpdate()
    {
        if (Vector3.Distance(player.transform.position, target.transform.position) < distance)
        {
            Complete();
        }
    }

    protected abstract void SetTarget();

    protected override void OnBegin()
    {
        SetTarget();
        TutorialArrow.Instance.SetTarget(target);
        TutorialArrow.Instance.ShowArrow();
    }

    protected override void OnComplete()
    {
        TutorialArrow.Instance.SetTarget(null);
        TutorialArrow.Instance.DisableArrow();
    }
}