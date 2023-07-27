using Kuhpik;
using System;

[Serializable]
public class CollectMoneyTutorialStep : TutorialStep
{
    private WalletModel wallet => GameData.walletModel;

    private int _currentMoneyCount;

    public override void Enter()
    {
        wallet.WalletChanged.AddListener(OnWalletChanged);
        _currentMoneyCount = wallet.moneyCount;
    }

    public override void Exit()
    {
        wallet.WalletChanged.RemoveListener(OnWalletChanged);
    }

    private void OnWalletChanged(WalletModel wallet)
    {
        if (wallet.moneyCount > _currentMoneyCount)
        {
            Complete();
        }
    }
}