using System;
using UnityEngine;

[Serializable]
public class SkateShelfUnlockTutorialStep : TutorialStep
{
    [SerializeField] private UnlockPayZoneComponent _skatePayZoneComponent;

    public override void Enter()
    {
        _skatePayZoneComponent.Unlocked.AddListener(Complete);
    }

    public override void Exit()
    {
        _skatePayZoneComponent.Unlocked.RemoveListener(Complete);
    }
}
