public class UpgradeSerilizationSgement : SerializationSegment
{
    public override void Save(SaveData saveData)
    {
        var upgrades = bootstrap.GameData.upgrades;
        saveData.upgrades = new SerializableUpgades(upgrades.speedUpgrade.level, upgrades.capacityUpgrade.level);
    }

    public override void Load(SaveData saveData) { }
}
