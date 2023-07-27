using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools;
using UnityTools.Extentions;

public class MobsSpawnerComponent : MonoBehaviour
{
    [field: SerializeField] public int segmentID { get; private set; }
    [field: SerializeField] public Transform enter { get; private set; }
    [field: SerializeField] public Transform exit { get; private set; }

    [field: SerializeField] public Transform visualEnter { get; private set; }
    [field: SerializeField] public Transform visualExit { get; private set; }

    [SerializeField] private Timer _spawnDelay;

    [SerializeField] private MobAI[] _mobs;
    [SerializeField] private int _mobsLimitPerRail;

    public List<MobAI> mobsOnLevel { get; private set; } = new List<MobAI>();

    private LevelSegmentComponent _levelSegment;
    private SkateShelfComponent[] _shelfs;

    private IEnumerable<SkateShelfComponent> activeShelfs => _shelfs.Where(x => x.segmentID == segmentID && x.gameObject.activeInHierarchy);
    private int activeShelfCount => activeShelfs.Count();

    private int currentMaxMobCount => activeShelfCount * _mobsLimitPerRail;

    private void Awake()
    {
        _levelSegment = GetComponentInParent<LevelSegmentComponent>();
    }

    private void Start()
    {
        _shelfs = FindObjectsOfType<SkateShelfComponent>(true);
    }

    private void Update()
    {
        _spawnDelay.UpdateTimer();
        if (_spawnDelay.isReady && mobsOnLevel.Count < currentMaxMobCount)
        {
            StartCoroutine(MobSpawnRoutine());
            _spawnDelay.Reset();
        }
    }

    private IEnumerator MobSpawnRoutine()
    {
        var mob = SpawnMob();
        yield return MobEnterRoutine(mob);
        var shelf = GetRandomShelf();
        Debug.DrawLine(transform.position, shelf.transform.position, Color.red, 999999);
        mob.SetTargetShelf(shelf);
    }

    private MobAI SpawnMob()
    {
        var mob = Instantiate(_mobs.GetRandom(), enter.position, Quaternion.identity, transform);
        mob.linkedSpawner = this;
        mob.cashRegister = GetComponentInParent<LevelSegmentComponent>().cashRegister;
        mobsOnLevel.Add(mob);
        return mob;
    }

    private SkateShelfComponent GetRandomShelf()
    {
        return activeShelfs.Where(x => x.gameObject.activeInHierarchy && !x.isFull).GetRandom();
    }

    public void RemoveMob(MobAI mob)
    {
        StartCoroutine(MobExitRoutine(mob));
    }

    private IEnumerator MobExitRoutine(MobAI mob)
    {
        mob.animation.ShowWalking();
        var duration = Vector3.Distance(visualExit.position, exit.position) / 5;
        mob.navigation.enabled = false;
        mob.transform.LookAt(visualExit);
        yield return mob.transform.DOMove(visualExit.position, duration).WaitForCompletion();
        mob.navigation.enabled = true;
        mobsOnLevel.Remove(mob);
        Destroy(mob.gameObject);
    }

    private IEnumerator MobEnterRoutine(MobAI mob)
    {
        mob.transform.position = visualEnter.position;
        yield return mob.DoMove(enter.position);
        mob.animation.ShowWalking();
    }
}
