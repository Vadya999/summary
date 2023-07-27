using Kuhpik;
using UnityEngine;

public class SkateShelfPayZoneAction : DefualtUnlockPayZoneAction
{
    [SerializeField] private SkateShelfComponent _shelf;

    public override void Invoke(UnlockPayZoneComponent unlockZone)
    {
        if (Bootstrap.Instance.GetCurrentGamestateID() == GameStateID.Game) SDKEvents.unlock.UnlockShelf(_shelf);
        _shelf.linkedRail.gameObject.SetActive(true);
        if (_shelf.hasLevelZone) _shelf.zone.Show();
        base.Invoke(unlockZone);
    }

    public override void Init()
    {
        _shelf.linkedRail.gameObject.SetActive(false);
        if (_shelf.hasLevelZone) _shelf.zone.Hide();
        base.Init();
    }
}