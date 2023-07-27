using GameAnalyticsSDK;

public class UpgradeModule
{
    public void SpeedUpgrade(Upgrade upgrade)
    {
        GameAnalytics.NewDesignEvent($"upgrade_speed_lvl{upgrade.level + 2}");
    }

    public void CapacityUpgrade(Upgrade upgrade)
    {
        GameAnalytics.NewDesignEvent($"upgrade_capacity_lvl{upgrade.level + 2}");
    }

    public void ConveyorUpgrade(int zoneID, int levelID, int conveyorID)
    {
        GameAnalytics.NewDesignEvent($"upgrade_conveyor{conveyorID + 1}_zone{zoneID + 1}_lvl{levelID + 1}");
    }
}