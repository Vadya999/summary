public class StartMoneySerilizationComponent : SerializationSegment
{
    public override void Load(SaveData saveData)
    {
        var x = FindObjectOfType<StartMoneySpawnComponent>();
        if (saveData.level.spawnMoneyCollected)
        {
            Destroy(x);
        }
    }

    public override void Save(SaveData saveData)
    {
        var x = FindObjectOfType<StartMoneySpawnComponent>();
        saveData.level.spawnMoneyCollected = x == null;
    }
}
