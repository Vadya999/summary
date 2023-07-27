using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using StateMachine;
using StateMachine.Conditions;
using UnityEngine;

public class CarZone : MonoBehaviour
{
    [SerializeField] private int zoneId;
    [SerializeField] private bool isUnlock;
    [SerializeField] private Transform posZone;
    [SerializeField] private TimerConfiguration timer;
    [SerializeField] private GameObject sellImage;
    [SerializeField] private GameObject destroyImage;
    [SerializeField] private GameObject legacy;
    [SerializeField] private List<GameObject> trash;

    private global::StateMachine.StateMachine stateMachine;
    private CarZoneDestroyState carZoneDestroyState;
    private CarZoneLegacyState carZoneLegacyState;
    private CarZoneSellState carZoneSellState;
    private CarZoneWaitingState carZoneWaitingState;
    private Podium podium;
    private IdleState idleState;
    private bool isZoneUnlock;
    private AttachCarController attachCarController;
    public bool IsCanBeAttach => stateMachine.CurrentState != carZoneWaitingState && stateMachine.CurrentState != carZoneSellState && isZoneUnlock;
    public bool IsAttachEmpty => attachCarController == null;
    public int ZoneId => zoneId;

    private void Awake()
    {
        podium = FindObjectOfType<Podium>();
        InitializeStateMachine();
        StartCoroutine(Initialize());
    }

    private void Update()
    {
        if (stateMachine.CurrentState == idleState) attachCarController = null;
        stateMachine.Tick();
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.2f);
        if (isUnlock)
        {
            UnlockZone();
        }
        else
        {
            if (Bootstrap.Instance.PlayerData.unlockZone.Contains(zoneId))
            {
                UnlockZone();
            }
        }
    }

    public AttachCarController TryToDetach()
    {
        return attachCarController != null ? attachCarController : null;
    }
    public void UnlockZone()
    {
        foreach (var trsh in trash)
        {
            trsh.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => Destroy(trsh));
        }

        isZoneUnlock = true;
        var playerData = Bootstrap.Instance.PlayerData;
        if (playerData.unlockZone.Contains(zoneId) == false)
        {
            playerData.unlockZone.Add(zoneId);
            Bootstrap.Instance.SaveGame();
        }
    }

    public void Attach(AttachCarController attachCarController)
    {
        this.attachCarController = attachCarController;
        attachCarController.IsSelling = true;
        carZoneSellState.AttachCarController(attachCarController);
        carZoneLegacyState.SetAttachCarController(attachCarController);
        carZoneWaitingState.SetAttach(attachCarController);
        carZoneDestroyState.SetAttachCarController(attachCarController);
        stateMachine.SetState(carZoneWaitingState);
    }

    private void InitializeStateMachine()
    {
        idleState = new IdleState();
        carZoneDestroyState = new CarZoneDestroyState(destroyImage);
        carZoneSellState = new CarZoneSellState(sellImage);
        carZoneLegacyState = new CarZoneLegacyState(legacy);
        carZoneWaitingState = new CarZoneWaitingState(timer, posZone);

        var randomTransition = new RandomStateTransition(carZoneSellState,
            new FuncCondition(() => carZoneWaitingState.IsReadyToLeave));
        randomTransition.AddNewState(carZoneDestroyState);
        //
        var legacyState = new StateTransition(carZoneLegacyState, new FuncCondition(() =>
        {
            if (carZoneWaitingState.IsReadyToLeave == false || attachCarController.IsCanBeVip == false)
            {
                return false;
            }
        
            var amount = Bootstrap.Instance.PlayerData.amounLegacy;
            if (amount == 2) amount = 10;
            if (amount == 3) amount = 5;
            if (amount == 4) amount = -1;
            if (amount == 5) amount = -1;
            if (amount == 0) amount = 40;
            if (amount == 1) amount = 20;
            if (amount != -1 && attachCarController.IsCanBeVip) amount = 100;
            if (Bootstrap.Instance.PlayerData.saveCar == null || Bootstrap.Instance.PlayerData.saveCar.Contains(attachCarController.ID)) amount = -1;
            Debug.Log(amount);
            return Random.Range(0, 100) <= amount;
        }));

        // var randomTransition = new RandomStateTransition(carZoneDestroyState,
        //     new FuncCondition(() => carZoneWaitingState.IsReadyToLeave));

        // var randomTransition = new RandomStateTransition(carZoneLegacyState,
        //     new FuncCondition(() => carZoneWaitingState.IsReadyToLeave));
        
        carZoneWaitingState.AddTransition(legacyState);
        carZoneWaitingState.AddTransition(new StateTransition(carZoneLegacyState,
            new FuncCondition(() => carZoneWaitingState.IsReadyToLeave && Bootstrap.Instance.GameData.IsLegacy)));
        carZoneWaitingState.AddTransition(new StateTransition(carZoneDestroyState,
            new FuncCondition(() => carZoneWaitingState.IsReadyToLeave && Bootstrap.Instance.GameData.IsDestroy)));
        carZoneWaitingState.AddTransition(new StateTransition(carZoneSellState,
            new FuncCondition(() =>
                (carZoneWaitingState.IsReadyToLeave && Bootstrap.Instance.GameData.IsSelling) ||
                (attachCarController.IsCanBeVip && carZoneWaitingState.IsReadyToLeave))));
        carZoneWaitingState.AddTransition(randomTransition);

        carZoneLegacyState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => attachCarController.IsAttach)));

        carZoneSellState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => carZoneSellState.IsReadyToLeave)));

        carZoneDestroyState.AddTransition(new StateTransition(idleState,
            new FuncCondition(() => attachCarController.IsAttach)));
        stateMachine = new global::StateMachine.StateMachine(idleState);
    }
}