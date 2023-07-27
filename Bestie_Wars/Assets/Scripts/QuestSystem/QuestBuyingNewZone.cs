using System;
using Kuhpik;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Quest/BuyingNewZone", fileName = "BuyingNewZone", order = 0)]
public class QuestBuyingNewZone : TaskQuest
{
    [SerializeField] private float neededBuyingZone;
    [SerializeField] private string processDescription;

    private PlayerData playerData;

    public override float ProcessDescription()
    {
        return Bootstrap.Instance.PlayerData.unlockMapZone.Count/neededBuyingZone; 
    }

    public override void EnableTask()
    {
    
    }

    public override void DisableTask()
    {
    }

    public override bool IsTaskCompleted()
    {
        if (Bootstrap.Instance.PlayerData.unlockMapZone.Count >= neededBuyingZone)
        {
            ProcessUpdated?.Invoke();
        }

        return Bootstrap.Instance.PlayerData.unlockMapZone.Count >= neededBuyingZone;
    }
}