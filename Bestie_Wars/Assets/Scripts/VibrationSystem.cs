using Kuhpik;
using MoreMountains.NiceVibrations;

public class VibrationSystem : GameSystem
{
    public override void OnInit()
    {
        MMVibrationManager.SetHapticsActive(player.IsVibrationActivate);
    }

    public static void PlayVibration()
    {
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
    }
}