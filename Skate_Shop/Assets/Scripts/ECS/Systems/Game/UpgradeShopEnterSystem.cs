using Kuhpik;
using System.Collections.Generic;
using UnityTools.Extentions;

public class UpgradeShopEnterSystem : GameSystem
{
    private IEnumerable<UpgradeShopComponent> _shops;

    public override void OnInit()
    {
        _shops = FindObjectsOfType<UpgradeShopComponent>(true);
    }

    public override void OnStateEnter()
    {
        _shops.ForEach(x => x.Entered.AddListener(OnShopEntered));
    }

    public override void OnStateExit()
    {
        _shops.ForEach(x => x.Entered.RemoveListener(OnShopEntered));
    }

    private void OnShopEntered(UpgradeShopComponent upgradeShop)
    {
        game.currentUpgradeShop = upgradeShop;
        ChangeGameState(GameStateID.Shop);
    }
}
