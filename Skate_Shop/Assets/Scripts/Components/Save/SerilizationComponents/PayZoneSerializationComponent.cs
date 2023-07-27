using System.Collections.Generic;
using System.Linq;
using UnityTools.Extentions;

public class PayZoneSerializationComponent : SerializationSegment
{
    public override void Load(SaveData saveData)
    {
        var zones = GetAllPayZones();
        var savedZones = saveData.level.payZoneStates;

        zones.ForEach((x, i) =>
        {
            if (!savedZones.Contains(x.Key))
            {
                x.Value.ForceUnlock();
            }
        });
    }

    public override void Save(SaveData saveData)
    {
        var zones = GetAllPayZones();
        var savedZones = saveData.level.payZoneStates;
        savedZones.Clear();
        zones.ForEach(x => savedZones.Add(x.Key));
    }

    private Dictionary<int, UnlockPayZoneComponent> GetAllPayZones()
    {
        var objs = FindObjectsOfType<UnlockPayZoneComponent>(true);
        return objs.ToDictionary(x => x.id);
    }
}