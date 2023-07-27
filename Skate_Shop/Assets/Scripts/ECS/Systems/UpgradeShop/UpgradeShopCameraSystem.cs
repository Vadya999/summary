using Kuhpik;

public class UpgradeShopCameraSystem : GameSystem
{
    private PlayerCameraComponent camera => GameData.player.playersCamera;

    public override void OnStateEnter()
    {
        camera.mainVirtualCamera.enabled = false;
        game.currentUpgradeShop.virtualCamera.enabled = true;
    }

    public override void OnStateExit()
    {
        game.currentUpgradeShop.virtualCamera.enabled = false;
        camera.mainVirtualCamera.enabled = true;

        game.currentUpgradeShop = null;
    }
}
