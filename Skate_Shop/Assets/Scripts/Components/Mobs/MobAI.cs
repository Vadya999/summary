using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityTools.Extentions;

[SelectionBase]
[RequireComponent(typeof(NavigationComponent))]
public class MobAI : MonoBehaviour
{
    [field: SerializeField] public GameObject windParticle { get; private set; }
    [field: SerializeField] public GameObject visualRoot { get; private set; }
    [field: SerializeField] public NavMeshObstacle obstacle { get; private set; }

    [SerializeField] private Transform skatePosInRun;
    [SerializeField] private Transform skatePointInMob;

    [SerializeField] private ParticleSystem emojiStart;
    [SerializeField] private ParticleSystem emojiToGoHome;
    [SerializeField] private Transform emojiSpawnPos;

    [SerializeField] private MobAnimationComponent _animation;

    public MobsSpawnerComponent linkedSpawner { get; set; }

    public MobAnimationComponent animation => _animation;
    public NavigationComponent navigation => _navigation;

    public SkateComponent skate => _activeSkate;

    private SkateComponent _activeSkate;
    private NavigationComponent _navigation;

    public CashRegisterComponent cashRegister { get; set; }

    private SkateShelfComponent _targetShelf;

    public bool inQueue { get; private set; }

    public int moneyPayValue { get; set; }

    public bool isIdle
    {
        set
        {
            navigation.agent.enabled = false;
            obstacle.enabled = false;
            if (value) obstacle.enabled = true;
            else navigation.agent.enabled = true;
        }
    }

    private void Awake()
    {
        _navigation = GetComponent<NavigationComponent>();
    }

    public void SetTargetShelf(SkateShelfComponent shelf)
    {
        _targetShelf = shelf;
        StartCoroutine(DoMoveToShelf());
    }

    public void OnSkateFilledInShelf(SkateShelfComponent shelf)
    {
        isIdle = false;
        Instantiate(emojiStart, emojiSpawnPos.position, Quaternion.identity, transform);
        shelf.queue.ReturnPoint(this);
        StartCoroutine(SkateRoutine(shelf));
    }

    private IEnumerator SkateRoutine(SkateShelfComponent shelf)
    {
        yield return DoMoveToSkateShelf(shelf);
        yield return DoPickupSkate(shelf);
        yield return DoMoveToStart(shelf.linkedRail);
        yield return SkateboardingOfRamp(shelf.linkedRail);
        yield return ShopPath();
        PayMoneyInShop();
    }

    private IEnumerator DoMoveToSkateShelf(SkateShelfComponent shelf)
    {
        _animation.ShowWalking();
        yield return DoNavigationMove(shelf.shakePickupPoint.position, 0.2f);
    }

    private IEnumerator DoMoveToShelf()
    {
        isIdle = false;
        var position = _targetShelf.queue.GetPoint(this);
        _targetShelf.mobsInQueue.Add(this);
        yield return DoNavigationMove(position);
        inQueue = true;
        isIdle = true;
    }

    private IEnumerator DoPickupSkate(SkateShelfComponent shelf)
    {
        _animation.ShowStop();
        _activeSkate = shelf.TakeSkate();
        moneyPayValue = Mathf.RoundToInt(shelf.payAmmount + ((shelf.payAmmount * 0.5f) * _activeSkate.level));
        yield return _activeSkate.transform.DoPickup(skatePosInRun, 0.5f);
    }

    private IEnumerator DoMoveToStart(RailComponent railToSlide)
    {
        _animation.ShowWalking();
        yield return DoNavigationMove(railToSlide.entryPoint, 0.2f);
    }

    private IEnumerator SkateboardingOfRamp(RailComponent rail)
    {
        yield return _activeSkate.transform.DoPickup(skatePointInMob, 0.5f);
        yield return rail.ShowRailAnimation(this);
        _activeSkate.transform.parent = skatePosInRun;
        _activeSkate.transform.localScale = Vector3.one;
        _activeSkate.transform.localRotation = Quaternion.identity;
        _activeSkate.transform.localPosition = Vector3.zero;
    }

    private IEnumerator ShopPath()
    {
        _animation.ShowWalking();
        var position = cashRegister.queue.GetPoint(transform);
        yield return DoNavigationMove(position);
    }

    public IEnumerator DoNavigationMove(Transform point, float minDist = 0.1f)
    {
        yield return DoNavigationMove(point.position, minDist);
    }

    public IEnumerator DoNavigationMove(Vector3 point, float minDist = 0.2f)
    {
        animation.ShowWalking();
        yield return _navigation.MoveToPointRoutine(point, minDist);
        animation.ShowStop();
    }

    private IEnumerator HomeRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        _animation.ShowWalking();
        Instantiate(emojiToGoHome, emojiSpawnPos.position, Quaternion.identity, transform);
        _navigation.SetExitSpeed();
        yield return DoNavigationMove(linkedSpawner.exit, 0.35f);
        linkedSpawner.RemoveMob(this);
    }

    private void PayMoneyInShop()
    {
        _animation.ShowStop();
        cashRegister.AddMoney(moneyPayValue, this);
        cashRegister.queue.ReturnPoint(transform);
        StartCoroutine(HomeRoutine());
    }

    public IEnumerator DoMove(Vector3 postion)
    {
        animation.ShowWalking();
        navigation.agent.enabled = false;
        var duration = Vector3.Distance(postion, transform.position) / 5;
        yield return transform.DOMove(postion, duration).WaitForCompletion();
        navigation.agent.enabled = true;
        animation.ShowStop();
        navigation.Warp(postion);
    }
}
