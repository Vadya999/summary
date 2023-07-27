using System;

[Serializable]
public class Upgrade
{
    public UpgradeData data { get; private set; }
    public int level { get; private set; }

    public int maxLevel => data.maxLevel;
    public int currentCost => data.costList[level];
    public int upgradeValue => data.upgradedValuePerLevel;
    public bool canUpgrade => level < maxLevel;
    public int totalValue => level * upgradeValue;

    public Upgrade(UpgradeData data)
    {
        this.data = data;
    }

    public Upgrade(UpgradeData data, int level)
    {
        this.data = data;
        this.level = level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void IncreaseLevel()
    {
        if (canUpgrade) level++;
    }
}
