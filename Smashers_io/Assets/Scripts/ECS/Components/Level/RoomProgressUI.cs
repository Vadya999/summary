using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class RoomProgressUI : MonoBehaviour
{
    [SerializeField] private List<StarComponent> _stars;

    public void SetProgress(int value)
    {
        _stars.ForEach(x => x.SetState(false));
        _stars.Take(value).ForEach(x => x.SetState(true));
    }
}
