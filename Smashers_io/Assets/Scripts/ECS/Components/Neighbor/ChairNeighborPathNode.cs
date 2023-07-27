using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

public class ChairNeighborPathNode : MonoBehaviour, INeighborPathNode
{
    [SerializeField] private float _sitTime;
    [SerializeField] private Transform _sitPoint;

    [SerializeField] private bool _hasTrap;
    [SerializeField, ShowIf(nameof(_hasTrap))] private TrapComponent _chair;

    public Vector3 enterPoint => _chair.transform.position;
    public Vector3 exitPoint => _chair.transform.position;

    public TrapComponent trap => _hasTrap ? _chair : null;

    public bool inUse { get; set; }

    public IEnumerator OnEnterPoint(NeighborComponent neighbor)
    {
        neighbor.animation.isManual = true;
        yield return neighbor.transform.DoTransform(_sitPoint, 0.25f).WaitForCompletion();
        neighbor.animation.ShowSit();
        yield return new WaitForSeconds(_sitTime);
        neighbor.animation.ShowStandUp();
        yield return new WaitForSeconds(0.5f);
        neighbor.animation.isManual = false;
    }
}