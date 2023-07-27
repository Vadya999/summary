using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class DefaultNeighborPathNode : MonoBehaviour, INeighborPathNode
{
    [SerializeField] private bool _hasTrap;
    [SerializeField, ShowIf(nameof(_hasTrap))] private TrapComponent _trap;

    [SerializeField] private bool _shouldWait;
    [SerializeField, ShowIf(nameof(_shouldWait))] private float _waitDuration;

    public Vector3 enterPoint => transform.position;
    public Vector3 exitPoint => transform.position;

    public TrapComponent trap => _hasTrap ? _trap : null;

    public bool inUse { get; set; }

    public IEnumerator OnEnterPoint(NeighborComponent neighbor)
    {
        if (_shouldWait)
        {
            neighbor.animation.isWalking = false;
            neighbor.animation.isManual = true;
            yield return new WaitForSeconds(_waitDuration);
            neighbor.animation.isManual = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_hasTrap && _trap != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _trap.transform.position);
        }
    }
}
