using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using HomaGames.HomaBelly;
using Kuhpik;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour, IRecalculateAllShopPanelSignal
{
    [SerializeField] private PriceConfiguration priceConfiguration;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private List<PanelConfiguration> panels;

    private PlayerData playerData;
    private int price;
    private float currentPric;
    private PriceMakerConfiguration priceMakerConfiguration;

    private void OnEnable()
    {
        playerData ??= Bootstrap.Instance.PlayerData;
        Recalculate();
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void Recalculate()
    {
        var currentLevel = playerData.UpgadeLevel[upgradeType];
        priceMakerConfiguration = GetPriceMage();
        price = priceMakerConfiguration.StartPrice + (priceMakerConfiguration.AddPerLevel * (currentLevel - 1));
        SetStatus(playerData.Money >= price && currentLevel < priceMakerConfiguration.MAXLevel);
        foreach (var panel in panels)
        {
            var param = currentLevel >= priceMakerConfiguration.MAXLevel ? "Max" : price.ToString();
            panel.SetParam(currentLevel.ToString(), param);
        }
    }

    public PriceMakerConfiguration GetPriceMage()
    {
        switch (upgradeType)
        {
            case UpgradeType.CarLevel:
                return priceConfiguration.CarLevelConfigurations;
                break;
            case UpgradeType.IncomeLevel:
                return priceConfiguration.CarIncomeConfigurations;
                break;
            case UpgradeType.CarAttachLevel:
                return priceConfiguration.CarCarAttachLevelConfigurations;

                break;
            default:
                return priceConfiguration.CarIncomeConfigurations;
        }
    }

    private void SetStatus(bool isActivate)
    {
        foreach (var panel in panels)
        {
            panel.SetActivate(isActivate);
        }
    }

    public void TryToBuy()
    {
        if (price > playerData.Money || playerData.UpgadeLevel[upgradeType] == priceMakerConfiguration.MAXLevel) return;
        playerData.Money -= price;
        playerData.UpgadeLevel[upgradeType]++;
        EventBus.RaiseEvent<IRecalculateAllShopPanelSignal>(t => t.Recalculate());
        Bootstrap.Instance.SaveGame();
        EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
        VibrationSystem.PlayVibration();
        switch (upgradeType)
        {
            case UpgradeType.CarLevel:
                HomaBelly.Instance.TrackDesignEvent($"upgrade_stack_lvl{playerData.UpgadeLevel[upgradeType]}");
                break;
            case UpgradeType.IncomeLevel:
                HomaBelly.Instance.TrackDesignEvent($"upgrade_income_lvl{playerData.UpgadeLevel[upgradeType]}");
                break;
            case UpgradeType.CarAttachLevel:
                HomaBelly.Instance.TrackDesignEvent($"upgrade_weight_lvl{playerData.UpgadeLevel[upgradeType]}");
                break;
        }
    }
}