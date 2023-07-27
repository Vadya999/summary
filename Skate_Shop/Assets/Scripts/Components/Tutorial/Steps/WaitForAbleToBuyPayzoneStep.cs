using Kuhpik;
using System;
using UnityEngine;

[Serializable]
public class WaitForAbleToBuyPayzoneStep : TutorialStep
{
    [SerializeField] private UnlockPayZoneComponent _payZoneToBuy;

    private WalletModel wallet => GameData.walletModel;

    public override void Enter()
    {
        wallet.WalletChanged.AddListener(OnWalletChanged);
        OnWalletChanged(GameData.walletModel);
    }

    public override void Exit()
    {
        wallet.WalletChanged.RemoveListener(OnWalletChanged);
    }

    private void OnWalletChanged(WalletModel wallet)
    {
        if (wallet.moneyCount >= _payZoneToBuy.cost)
        {
            Complete();
        }
    }
}
