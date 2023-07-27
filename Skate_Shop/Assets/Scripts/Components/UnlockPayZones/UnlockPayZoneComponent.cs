using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityTools;
using UnityTools.Extentions;

[SelectionBase]
public class UnlockPayZoneComponent : MonoBehaviour
{
    [SerializeField] private int priceToUnlock;

    [SerializeField] private TextMeshProUGUI textUnlock;
    [SerializeField] private UnlockPayZoneAction _unlockAction;

    private Timer _payDelay = new Timer(0.1f);

    private int _fullPrise;

    private WalletModel walletModel => GameData.walletModel;

    private bool _isUnlocked = false;

    public readonly UnityEvent Unlocked = new UnityEvent();

    public int cost => _fullPrise;

    private int _id = -1;
    public int id
    {
        get
        {
            if (_id == -1) _id = transform.GetSiblingIndex();
            return _id;
        }
    }

    private bool _inited;

    private void Awake()
    {
        _fullPrise = priceToUnlock;
    }

    public void Init()
    {
        if (!_inited)
        {
            _inited = true;
            _unlockAction.Init();
        }
    }

    private void Start()
    {
        textUnlock.text = $"{priceToUnlock}";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.HasComponent<PlayerComponent>() && _payDelay.isReady)
        {
            MoneyTranslate();
        }
    }

    private void Update()
    {
        _payDelay.UpdateTimer();
    }

    private void MoneyTranslate()
    {
        if (walletModel.moneyCount < priceToUnlock) return;
        var paySegment = walletModel.CalculateMoneySegment(_fullPrise, priceToUnlock, walletModel.moneyCount);
        if (paySegment > 0)
        {
            priceToUnlock -= paySegment;
            walletModel.moneyCount -= paySegment;
            textUnlock.text = $"{priceToUnlock}";
            GameData.player.walletComponent.ShowMoneyTransition(transform);
            _payDelay.Reset();
        }
        if (priceToUnlock <= 0 && !_isUnlocked)
        {
            _isUnlocked = true;
            UnlockZone();
        }
    }

    public void ForceUnlock()
    {
        Init();
        _isUnlocked = true;
        UnlockZone();
    }

    private void UnlockZone()
    {
        Unlocked?.Invoke();
        _unlockAction.Invoke(this);
    }
}
