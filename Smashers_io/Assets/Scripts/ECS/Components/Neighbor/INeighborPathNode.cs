using System.Collections;
using UnityEngine;

public interface INeighborPathNode
{
    public Vector3 enterPoint { get; }
    public Vector3 exitPoint { get; }

    public TrapComponent trap { get; }
    public bool inUse { get; set; }

    public IEnumerator OnEnterPoint(NeighborComponent neighbor);
}
