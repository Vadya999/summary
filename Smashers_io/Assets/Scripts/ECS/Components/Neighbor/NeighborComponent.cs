using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityTools;
using UnityTools.Extentions;

[SelectionBase]
public class NeighborComponent : MonoBehaviour
{
    [field: SerializeField] public NeighborAnimationComponent animation { get; private set; }
    [field: SerializeField] public GameObject aleart { get; private set; }
    [field: SerializeField] public GameObject idle { get; private set; }

    [SerializeField] private List<GameObject> _nodesObjects;

    private List<INeighborPathNode> _nodes;

    public INeighborPathNode currentNode => _nodes[currentNodeID];

    private readonly Timer _lastPlayerSeen = new Timer(0.5f);
    public bool canSeePlayer
    {
        get => !_lastPlayerSeen.isReady;
        set
        {
            if (value)
            {
                _lastPlayerSeen.Reset();
            }
        }
    }

    private int _currentNodeID;
    public int currentNodeID
    {
        get => _currentNodeID;
        set
        {
            _currentNodeID = value;
            if (_currentNodeID >= _nodesObjects.Count) _currentNodeID = 0;
            if (_currentNodeID < 0) _currentNodeID = _nodesObjects.Count - 1;
        }
    }

    public bool nodeLock { get; set; }

    private readonly Timer _angryTimer = new Timer(5f);

    private void Awake()
    {
        _angryTimer.End();
        _nodes = _nodesObjects
            .Select(x => x.GetComponent<INeighborPathNode>())
            .ToList();
    }


    public void ShowAngry()
    {
        _angryTimer.Reset();
    }

    public Vector3 GetDirectionTo(Vector3 point)
    {
        var direction = point - transform.position;
        direction.y = 0;
        direction = direction.normalized;
        return direction;
    }

    private void Update()
    {
        _lastPlayerSeen.UpdateTimer();
        aleart.SetActive(canSeePlayer);
        _angryTimer.UpdateTimer();
        animation.SetAngry(!_angryTimer.isReady);
    }

    private void OnDrawGizmos()
    {
        if (_nodesObjects.Any(x => x == null) || _nodesObjects.Count() < 2) return;

        var nodes = _nodesObjects
            .Select(x => x.GetComponent<INeighborPathNode>())
            .ToList();

        Gizmos.color = Color.magenta;
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            Gizmos.DrawLine(nodes[i].exitPoint, nodes[i + 1].enterPoint);
        }
        Gizmos.DrawLine(nodes.Last().exitPoint, nodes.First().enterPoint);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(SetRandomSkin))]
    private void SetRandomSkin()
    {
        var allSkins = AssetDatabaseExtentions.LoadAllPrefabsWithComponent<NeighborAnimationComponent>();
        var skipPrefab = allSkins.GetRandom();
        SetSkin(skipPrefab);
        AssetDatabaseExtentions.MarkPrefabDirty();
    }

    private void SetSkin(NeighborAnimationComponent skinPrefab)
    {
        if (animation != null) DestroyImmediate(animation.gameObject);
        animation = PrefabUtility.InstantiatePrefab(skinPrefab, transform) as NeighborAnimationComponent;
    }

    [ContextMenu(nameof(ReverceLoopPath))]
    private void ReverceLoopPath()
    {
        var reverced = _nodesObjects.Skip(1).Reverse().Skip(1).ToArray();
        for (int i = 0; i < reverced.Length; i++)
        {
            if (reverced[i].TryGetComponent<DoorComponent>(out var door))
            {
                reverced[i] = door.otherDoor.gameObject;
            }
        }
        _nodesObjects = _nodesObjects
            .Concat(reverced)
            .ToList();
    }
#endif
}