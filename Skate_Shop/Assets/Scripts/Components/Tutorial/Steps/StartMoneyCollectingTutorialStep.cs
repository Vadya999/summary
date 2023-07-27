using System;
using UnityEngine;

[Serializable]
public class StartMoneyCollectingTutorialStep : TutorialStep
{
    [SerializeField] private StartMoneySpawnComponent _startMoney;

    public override void Update()
    {
        if (_startMoney == null) Complete();
    }
}
