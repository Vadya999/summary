using Kuhpik;

public class ConveyourPayZoneAction : DefualtUnlockPayZoneAction
{
    public override void Invoke(UnlockPayZoneComponent zone)
    {
        var s = objectToUnlock.GetComponentInChildren<ConveyorController>();
        if (Bootstrap.Instance.GetCurrentGamestateID() == GameStateID.Game) SDKEvents.unlock.UnlockConveyour(s.data.id, s.segmentID);
        base.Invoke(zone);
    }
}
