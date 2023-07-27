using Kuhpik;

public class WalletSerilizationComponent : SerializationSegment
{
    public override void Load(SaveData saveData)
    {
        GameData.walletModel.moneyCount = saveData.moneyCount;
    }

    public override void Save(SaveData saveData)
    {
        saveData.moneyCount = GameData.walletModel.moneyCount;
    }
}
