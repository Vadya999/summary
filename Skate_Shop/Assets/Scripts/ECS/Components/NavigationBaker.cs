using UnityEngine;
using UnityEngine.AI;
using UnityTools.Extentions;

public class NavigationBaker : MonoBehaviour
{
    private NavMeshSurface[] _surfaces;

    private int _navMeshIgnoreLayer;
    private int _defaultLayer;

    private void Awake()
    {
        _surfaces = GetComponentsInChildren<NavMeshSurface>();
        _defaultLayer = LayerMask.NameToLayer("Default");
        _navMeshIgnoreLayer = LayerMask.NameToLayer("NavMeshIgnore");
    }

    public void Build()
    {
        var bots = FindObjectsOfType<MobAI>();
        var player = FindObjectOfType<PlayerComponent>().gameObject;
        var defaultPlayerLayer = player.layer;
        player.layer = _navMeshIgnoreLayer;
        bots.ForEach(x => x.gameObject.layer = _navMeshIgnoreLayer);
        _surfaces.ForEach(x => x.BuildNavMesh());
        bots.ForEach(x => x.gameObject.layer = defaultPlayerLayer);
        player.layer = defaultPlayerLayer;
    }
}