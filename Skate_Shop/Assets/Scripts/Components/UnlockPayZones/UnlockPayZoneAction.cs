using UnityEngine;

public abstract class UnlockPayZoneAction : MonoBehaviour
{
    public abstract void Init();
    public abstract void Invoke(UnlockPayZoneComponent zone);
}
