using NaughtyAttributes;
using Navigation.Nodes.Actions;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class GenericNeighborPathNode : MonoBehaviour, INeighborPathNode
{
    [SerializeReference, SubclassSelector] private NodeAction _aciton = new NoneAction();

    [SerializeField] private bool _hasTrap;
    [SerializeField, ShowIf(nameof(_hasTrap))] private TrapComponent _chair;

    public Vector3 enterPoint => transform.position;
    public Vector3 exitPoint => transform.position;

    public TrapComponent trap => _hasTrap ? _chair : null;

    public bool inUse { get; set; }

    public IEnumerator OnEnterPoint(NeighborComponent neighbor)
    {
        yield return _aciton.OnEnterPoint(neighbor);
    }
}
