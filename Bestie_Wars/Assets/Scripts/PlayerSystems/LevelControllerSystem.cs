using System;
using System.Collections.Generic;
using EventBusSystem;
using Kuhpik;
using UnityEngine;

public class LevelControllerSystem : GameSystem,IRecalculateAllShopPanelSignal
{
    [SerializeField] private CarUpgradeConfiguration carUpgradeConfiguration;
    [SerializeField] private AttachCarQueueController attach;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public override void OnInit()
    {
        attach.Initialize();
        if (player.UpgadeLevel == null)
        {
            player.UpgadeLevel = new Dictionary<UpgradeType, int>();
            foreach (UpgradeType upgradeType in Enum.GetValues(typeof(UpgradeType)))
            {
                player.UpgadeLevel.Add(upgradeType, 1);
            }
        }

        attach.SetMaxLevel(player.UpgadeLevel[UpgradeType.CarLevel] * carUpgradeConfiguration.AddedToStackPerLvl);
    }

    public void Recalculate()
    {
        attach.SetMaxLevel(player.UpgadeLevel[UpgradeType.CarLevel] * carUpgradeConfiguration.AddedToStackPerLvl);
    }
}