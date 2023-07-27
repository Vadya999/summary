using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EventBusSystem;
using Kuhpik;
using NaughtyAttributes;
using StateMachine;
using StateMachine.Conditions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using StateMachine = StateMachine.StateMachine;

public class AttachCarController : MonoBehaviour, IQueuing, IRecalculateAllShopPanelSignal, IBoughtNewZone
{
    [SerializeField] private GameObject minimapIcon;
    [SerializeField] private bool isHeavyCar;
    [SerializeField] private GameObject heavyText;
    [SerializeField] private CarMaterialType carMaterial = CarMaterialType.CasualCar;
    [SerializeField] private bool isCanBeVip;
    [SerializeField] private int id;
    [SerializeField] private Material legacyMaterial;
    [SerializeField] private int carLvl;
    [SerializeField] private List<GameObject> wheels;
    [SerializeField] public Sprite carLegacySprite;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Transform car;
    [SerializeField] private AttachCarConfigurations attachCarConfigurations;
    [SerializeField] private Transform positionToAttachNextCar;

    [SerializeField] private GameObject leveling;
    [SerializeField] private TMP_Text levelingText;
    [SerializeField] private GameObject attachImage;
    [SerializeField] private Image image;
    [SerializeField] private Image heavyIdentity;
    public Transform Car => car;
    public Transform TransformObject => transform;
    public Transform PositionBehind => positionToAttachNextCar;

    public SkinnedMeshRenderer MeshRenderer => meshRenderer;

    public bool IsCanBeVip => isCanBeVip;

    public bool IsCanBeAttach => currentAttachTransform == null && stateMachine.CurrentState != attachStartState &&
                                 IsSelling == false && isPause == false &&
                                 playerData.UpgadeLevel[UpgradeType.CarAttachLevel] >= carLvl && IsUnlockHeavyCar();

    public bool IsHeavyCar => isHeavyCar;
    public int ID => id;
    public bool IsWasAttach;
    public bool IsAttach { get; private set; }
    public bool IsCanBeDestroy { get; private set; }
    public bool IsCanBeSave { get; private set; }
    public bool IsSelling = false;
    public MiniPlayer MiniPlayer => m_player;
    public AttachCarConfigurations AttachCarConfigurations => attachCarConfigurations;
    private MaterialConfiguration materialConfiguration;
    private MiniPlayer m_player;
    private bool isPause = false;
    private List<Outline> outLine;
    private Transform currentAttachTransform;
    private Vector3 lastPos;
    private global::StateMachine.StateMachine stateMachine;
    private PlayerData playerData;

    private Outline.Mode currentMode = Outline.Mode.OutlineVisible;
    private Color currentColor;


    private AttachStartState attachStartState;
    private AttachStartStateWithoutAnimation attachStartStateWithout;

    private void Awake()
    {
        playerData = Bootstrap.Instance.PlayerData;
        SetLayer(7);
        if (isCanBeVip == false)
        {
            materialConfiguration = Bootstrap.Instance.GetSystem<MaterialGenerator>().GetMaterial(carMaterial);
            meshRenderer.materials = new[] {new Material(materialConfiguration.Casual)};
        }

        m_player = GetComponentInChildren<MiniPlayer>();
        m_player.gameObject.SetActive(false);
        InitializeStateMachine();
        outLine = GetComponentsInChildren<Outline>().ToList();

        DisableAttach();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private bool IsUnlockHeavyCar()
    {
        if (isHeavyCar == false) return true;
        return Bootstrap.Instance.PlayerData.unlockZone.Contains(77);
    }

    public void FailedAttach()
    {
        if (isHeavyCar && Bootstrap.Instance.PlayerData.unlockZone.Contains(77) == false)
        {
            StopAllCoroutines();
            StartCoroutine(IsHeavy());
        }
    }

    private IEnumerator IsHeavy()
    {
        heavyText.SetActive(true);
        yield return new WaitForSeconds(2f);
        heavyText.SetActive(false);
    }

    private void InitializeStateMachine()
    {
        ActivateLeveling();
        var idleState = new IdleState();
        attachStartState = new AttachStartState(this, this);
        attachStartStateWithout = new AttachStartStateWithoutAnimation(this);

        attachStartState.AddTransition(new StateTransition(idleState, new FuncCondition(() =>
        {
            if (!attachStartState.IsReadyToLeave)
            {
                return false;
            }

            return true;
        })));

        attachStartStateWithout.AddTransition(new StateTransition(idleState, new FuncCondition(() =>
        {
            if (!attachStartStateWithout.IsReadyToLeave)
            {
                return false;
            }

            return true;
        })));

        stateMachine = new global::StateMachine.StateMachine(idleState);
    }

    public void Attach(Transform attachPos)
    {
        IsAttach = true;
        if (IsWasAttach == false)
        {
            minimapIcon.SetActive(false);
        }
        IsWasAttach = true;
        currentColor = Color.white;
        SetLayer(9);
        currentAttachTransform = attachPos;
    }

    public void ShowAttach(AttachCarQueueController carAttacher)
    {
        attachImage.SetActive(true);
        image.enabled = true;
        currentColor = Color.green;
        image.DOFillAmount(1, 1f).OnComplete(() =>
        {
            DisableAttach();
            AttachToNewQueue(carAttacher);
            VibrationSystem.PlayVibration();
        });
    }

    private void ActivateLeveling()
    {
        if (isHeavyCar)
        {
            var result = Bootstrap.Instance.PlayerData.unlockZone.Contains(77);
            heavyIdentity.enabled = result == false;
            if (result == false)
            {
                leveling.SetActive(false);
                return;
            }
        }

        levelingText.text = "lvl " + carLvl.ToString();
        leveling.SetActive(playerData.UpgadeLevel[UpgradeType.CarAttachLevel] < carLvl);
    }

    public void DisableAttach()
    {
        image.enabled = false;
        attachImage.SetActive(false);
        image.DOKill();
        currentColor = Color.white;
        image.fillAmount = 0;
    }

    public void SpawnEffecT()
    {
        EventBus.RaiseEvent<ISpawnEffectSignal>(t => t.SpawnEffect(EffectType.BlackSoft, Car, true));
    }

    private void SetLayer(int layer)
    {
        gameObject.layer = layer;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = layer;
        }
    }

    public void SetOutline(bool isActivate)
    {
        Debug.Log("1234");
        foreach (var outline in outLine)
        {
            outline.OutlineColor = currentColor;
            outline.OutlineMode = currentMode;
            outline.enabled = isActivate;
        }
    }

    public void ToSave()
    {
        meshRenderer.materials = new[] {new Material(legacyMaterial)};
        SetOutline(false);
        attachImage.SetActive(false);
        leveling.SetActive(false);
        IsCanBeSave = true;
        minimapIcon.SetActive(false);
    }

    public void ToDestroy()
    {
        transform.DOShakeScale(0.3f, 0.4f);
        meshRenderer.materials = new[] {new Material(materialConfiguration.Destroy)};
        IsCanBeDestroy = true;
        var amountWheel = Random.Range(0, 2);
        for (int i = 0; i < amountWheel; i++)
        {
            DestroyWheel();
        }
    }

    [Button("WheelDestroy")]
    public void DestroyWheel()
    {
        var wheel = wheels[Random.Range(0, wheels.Count)];
        wheel.GetComponent<Collider>().isTrigger = false;
        var rb = wheel.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        wheel.transform.parent = null;
        wheels.Remove(wheel);
        rb.AddForce(wheel.transform.forward * 200, ForceMode.Force);
        Destroy(wheel, 5f);
        outLine = GetComponentsInChildren<Outline>().ToList();
    }

    public void AttachToNewQueue(IQueue queuing, bool isPlayingAnimation = true)
    {
        SetOutline(false);
        if (isPlayingAnimation)
        {
            attachStartState.SetAttachCarQueueController(queuing);
            stateMachine.SetState(attachStartState);
        }
        else
        {
            attachStartStateWithout.SetAttachCarQueueController(queuing);
            stateMachine.SetState(attachStartStateWithout);
        }
    }

    public void AttachPause(float pauseTime = 3f)
    {
        StartCoroutine(Pause(pauseTime));
    }

    private IEnumerator Pause(float PauseTime)
    {
        isPause = true;
        yield return new WaitForSeconds(PauseTime);
        isPause = false;
    }

    public void Detach()
    {
        IsAttach = false;
        SetLayer(7);
        currentAttachTransform = null;
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        if (currentAttachTransform != null && currentAttachTransform.position != lastPos)
        {
            // Move();
            // Rotate();
            // lastPos = currentAttachTransform.position;
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(currentAttachTransform.position, transform.position,
            attachCarConfigurations.SpeedLerp);
    }

    private void Rotate()
    {
        var lookPos = currentAttachTransform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
            attachCarConfigurations.SpeedRotate * Time.fixedDeltaTime);
    }

    public void Recalculate()
    {
        ActivateLeveling();
    }

    public void BoughtZone()
    {
        if (isHeavyCar)
        {
            heavyIdentity.enabled = Bootstrap.Instance.PlayerData.unlockZone.Contains(77) == false;
            Recalculate();
        }
    }
}