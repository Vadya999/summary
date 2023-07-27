using GameAnalyticsSDK;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class UnlockModule
{
    public void UnlockConveyour(int conveyorID, int zoneID)
    {
        GameAnalytics.NewDesignEvent($"unlock_converyor{conveyorID + 1}_zone{zoneID + 1}");
    }

    //TODO: пересобрать систему уровней и сохранения, чтобы такую жесть не нужно было городить :(
    public void UnlockShelf(SkateShelfComponent shelf)
    {
        var id = GameObject.FindObjectsOfType<SkateShelfComponent>(true)
            .Where(x => shelf.isRamp == x.isRamp && shelf.segmentID == x.segmentID)
            .OrderBy(x => x.transform.GetGlobalSiblingIndexFast())
            .ToList()
            .IndexOf(shelf);

        if (shelf.isRamp)
        {
            GameAnalytics.NewDesignEvent($"unlock_ramp{id + 1}_zone{shelf.segmentID + 1}");
        }
        else
        {
            GameAnalytics.NewDesignEvent($"unlock_slide{id + 1}_zone{shelf.segmentID + 1}");
        }
    }
}
