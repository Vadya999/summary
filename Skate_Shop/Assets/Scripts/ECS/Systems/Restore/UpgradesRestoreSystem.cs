using Kuhpik;

public class UpgradesRestoreSystem : GameSystem
{
    private UpgradeShopConfig _config;

    private SkateStackComponent skateStack => GameData.player.skatesRoot;
    private BoxStackComponent boxStack => GameData.player.boxRoot;

    public override void OnStateEnter()
    {
        if (game.upgrades != null)
        {
            ApplyUpgrades();
            return;
        }
        if (game.shouldRestore)
        {
            LoadUpgradeData();
        }
        else
        {
            GenerateUpgradeData();
        }
    }

    private void ApplyUpgrades()
    {
        skateStack.capacity = 3 + game.upgrades.capacityUpgrade.totalValue;
        boxStack.capacity = 3 + game.upgrades.capacityUpgrade.totalValue;
    }

    private void LoadUpgradeData()
    {
        var upgradeSave = game.saveData.upgrades;
        var speedUpgrade = new Upgrade(_config.speedUpgrade, upgradeSave.speedLevel);
        var capacityUpgrade = new Upgrade(_config.capacityUpgrade, upgradeSave.capacityLevel);

        game.upgrades = new PlayerUpgradesModel(speedUpgrade, capacityUpgrade);

        GameData.playerSpeed += speedUpgrade.level * speedUpgrade.upgradeValue;
        skateStack.capacity += capacityUpgrade.totalValue;
        boxStack.capacity += capacityUpgrade.totalValue;
    }

    private void GenerateUpgradeData()
    {
        var speedUpgrade = new Upgrade(_config.speedUpgrade);
        var capacityUpgrade = new Upgrade(_config.capacityUpgrade);

        game.upgrades = new PlayerUpgradesModel(speedUpgrade, capacityUpgrade);
    }
}