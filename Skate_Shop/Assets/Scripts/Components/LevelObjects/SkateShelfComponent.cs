using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTools;
using UnityTools.Extentions;

[SelectionBase]
public class SkateShelfComponent : MonoBehaviour
{
    [field: SerializeField] public int segmentID { get; private set; }
    [field: SerializeField] public int payAmmount { get; private set; }
    [field: SerializeField] public Transform shakePickupPoint { get; private set; }
    [field: SerializeField] public RailComponent linkedRail { get; private set; }
    [field: SerializeField] public PseudoQueue queue { get; private set; }
    [field: SerializeField] public int maxBotCount { get; private set; }
    [field: SerializeField] public bool hasLevelZone { get; private set; }
    [field: SerializeField, ShowIf(nameof(hasLevelZone))] public LevelZoneComponent zone { get; private set; }

    [SerializeField] private float _translateSpeedSkate;
    [SerializeField] private float _speedSheif;
    [SerializeField] private Transform[] _endPosSkate;

    [SerializeField] private Timer _pickupDelay;

    [SerializeField] private SkateData _requireSkate;

    public SkateData requireSkate => _requireSkate;

    public event Action<int> RequiredSkateCountChanged;
    public event Action<SkateData, int> RequireSkateChanged;

    public readonly List<MobAI> mobsInQueue = new List<MobAI>();

    public List<SkateComponent> skateListInSheif { get; private set; } = new List<SkateComponent>();

    private bool isValidColor => IsValidColor();
    private SkateStackComponent stack => player.skatesRoot;
    private PlayerComponent player => GameData.player;
    public bool isRamp => linkedRail.isRamp;

    private int _level;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            UpdateSkateIcon();
        }
    }

    private bool _isStartTranslate;

    public bool isFull => mobsInQueue.Count >= maxBotCount;

    private int _skatersWaitingSkate;
    public int skatersWaitingSkate
    {
        get => _skatersWaitingSkate;
        set
        {
            _skatersWaitingSkate = value;
            RequiredSkateCountChanged?.Invoke(value);
        }
    }


    private void Awake()
    {
        _pickupDelay.Reset();
    }

    private void Start()
    {
        UpdateSkateIcon();
    }

    public void UpdateSkateIcon()
    {
        RequireSkateChanged?.Invoke(_requireSkate, level);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.HasComponent<PlayerComponent>() && !_isStartTranslate && isValidColor && skateListInSheif.Count < maxBotCount)
        {
            StartCoroutine(TranslateSkateToSheif());
        }
    }

    public SkateComponent TakeSkate()
    {
        var skate = skateListInSheif.LastOrDefault();
        skateListInSheif.Remove(skate);
        return skate;
    }

    public void ForceStack(List<SkateComponent> skates)
    {
        skateListInSheif = skates;
        skateListInSheif.ForEach((x, i) =>
        {
            var point = _endPosSkate[i];
            x.transform.parent = point;
            x.transform.localPosition = Vector3.zero;
            x.transform.localRotation = Quaternion.Euler(Vector3.zero);
            x.transform.localScale = Vector3.one;
        });
    }

    private IEnumerator TranslateSkateToSheif()
    {
        _isStartTranslate = true;
        yield return new WaitForSeconds(_speedSheif);
        if (stack.hasItem && skateListInSheif.Count <= stack.capacity + 1)
        {
            var point = _endPosSkate[skateListInSheif.Count];
            var skate = player.skatesRoot.MoveToPoint(point, out var tween);
            skateListInSheif.Add(skate);
            yield return tween.WaitForCompletion();
        }

        StopCoroutine(TranslateSkateToSheif());
        _isStartTranslate = false;
    }

    private void Update()
    {
        if (skateListInSheif.Count > 0) _pickupDelay.UpdateTimer();
        if (skateListInSheif.Count > 0 && mobsInQueue.Count > 0 && _pickupDelay.isReady)
        {
            var mob = mobsInQueue.FirstOrDefault(x => x.inQueue);
            if (mob != null)
            {
                mobsInQueue.Remove(mob);
                mob.OnSkateFilledInShelf(this);
                _pickupDelay.Reset();
            }
        }
        skatersWaitingSkate = mobsInQueue.Where(x => x.inQueue).Count();
    }

    private bool IsValidColor()
    {
        if (!stack.hasItem) return false;
        var skate = stack.stack.First();
        var validData = skate.data == _requireSkate;
        var validLevel = skate.level == level;
        return validData && validLevel;
    }
}
