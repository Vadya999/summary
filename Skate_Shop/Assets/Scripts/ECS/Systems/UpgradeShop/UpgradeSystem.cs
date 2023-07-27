using CW.Common;
using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class UpgradeSystem : GameSystemWithScreen<UpgradeScreen>
{
    private UpgradeShopConfig _config;

    private Upgrade speedUpgrade => game.upgrades.speedUpgrade;
    private Upgrade capacityUpgrade => game.upgrades.capacityUpgrade;

    private SkateStackComponent skateStack => GameData.player.skatesRoot;
    private BoxStackComponent boxStack => GameData.player.boxRoot;

    private WalletModel wallet => GameData.walletModel;

    public override void OnStateEnter()
    {
        screen.exitButton.onClick.AddListener(CloseMenuUpgradeUI);

        screen.speedUpgrade.upgradeButton.onClick.AddListener(OnSpeedUpgradeButtonClicked);
        screen.stackUpgrade.upgradeButton.onClick.AddListener(OnStackUpgradeButtonClicked);

        UpdateUI();
    }

    public override void OnStateExit()
    {
        screen.exitButton.onClick.RemoveListener(CloseMenuUpgradeUI);

        screen.speedUpgrade.upgradeButton.onClick.RemoveListener(OnSpeedUpgradeButtonClicked);
        screen.stackUpgrade.upgradeButton.onClick.RemoveListener(OnStackUpgradeButtonClicked);
    }

    private void CloseMenuUpgradeUI()
    {
        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
    }

    private void OnSpeedUpgradeButtonClicked()
    {
        if (speedUpgrade.canUpgrade && wallet.TryBuy(speedUpgrade.currentCost))
        {
            SDKEvents.upgradeModule.SpeedUpgrade(speedUpgrade);
            speedUpgrade.IncreaseLevel();
            GameData.playerSpeed += speedUpgrade.upgradeValue;
            UpdateUI();
            SpawnEffect();
            DoPlayerPunch();
        }
    }

    private void OnStackUpgradeButtonClicked()
    {
        if (capacityUpgrade.canUpgrade && wallet.TryBuy(capacityUpgrade.currentCost))
        {
            SDKEvents.upgradeModule.CapacityUpgrade(capacityUpgrade);
            capacityUpgrade.IncreaseLevel();
            boxStack.capacity += capacityUpgrade.upgradeValue;
            skateStack.capacity += capacityUpgrade.upgradeValue;
            UpdateUI();
            SpawnEffect();
            DoPlayerPunch();
        }
    }

    private void UpdateUI()
    {
        screen.speedUpgrade.Redraw(speedUpgrade, wallet);
        screen.stackUpgrade.Redraw(capacityUpgrade, wallet);
    }

    private void SpawnEffect()
    {
        var clone = Instantiate(_config.upgradeEffect, GameData.player.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        var scale = clone.transform.localScale * 0.8f;
        clone.transform.localScale = scale;
    }

    private void DoPlayerPunch()
    {
        if (GameData.settings.hapticEnabled) MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        GameData.player.transform.DOPunchScale(Vector3.one * 0.1f, 0.25f, 0, 1);
    }
}
