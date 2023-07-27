using UnityEngine;
using UnityTools.Extentions;

public class PayZoneLoader : MonoBehaviour
{
    private void Start()
    {
        var payZones = GetComponentsInChildren<UnlockPayZoneComponent>();
        payZones.ForEach(x => x.Init());
    }
}