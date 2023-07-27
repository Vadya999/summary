using DG.Tweening;
using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityTools;
using UnityTools.Extentions;

public class ConveyorUpgrade : MonoBehaviour
{
    [SerializeField] private ConveyorUpgradeConfig _upgradeConfig;
    [SerializeField] private LevelObjectUpgradeUI _upgradeUI;

    [SerializeField] private int maxValueToUpgrade;

    [SerializeField] private ConveyorController currentConveyorController;

    [SerializeField] private GameObject UIupgradeOnStay;
    [SerializeField] private Transform _upgradeParticlePoint;
    [SerializeField] private ParticleSystem _upgradeParticle;

    [SerializeField] private Timer _upgradeDelay;

    private int[] priceUpgrades => _upgradeConfig.prices;
    private Timer _payDelay = new Timer(0.1f);

    private int currentUpgradeFullPrice => _fullPriceUpgrades[level];
    private int currentUpgradePrice
    {
        get => priceUpgrades[level];
        set
        {
            priceUpgrades[level] = value;
            UpdateUI();
        }
    }

    private int _level;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            currentConveyorController.speedTranslateSkatesInConveyor = 1 + (_upgradeConfig.upgradeValue * _level);
            currentConveyorController.boxStack.capacity = 3 + _level;
            currentConveyorController.UpdateUI();
            UpdateUI();
            if (level > maxValueToUpgrade - 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private int[] _fullPriceUpgrades;

    private WalletModel walletModel => GameData.walletModel;

    private void Awake()
    {
        _upgradeConfig = Instantiate(_upgradeConfig);
        _fullPriceUpgrades = priceUpgrades.ToArray();
        UIupgradeOnStay.SetActive(false);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (level < priceUpgrades.Length)
        {
            _upgradeUI.cost.text = "" + currentUpgradePrice;
            _upgradeUI.SetLevel(level, priceUpgrades.Length);
        }
        else
        {
            _upgradeUI.cost.text = "MAX";
            _upgradeUI.SetLevel(level, priceUpgrades.Length);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.HasComponent<PlayerComponent>())
        {
            if (level > maxValueToUpgrade - 1)
            {
                Destroy(gameObject);
            }
            else if (_payDelay.isReady)
            {
                PayMoney();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.HasComponent<PlayerComponent>())
        {
            UIupgradeOnStay.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<PlayerComponent>())
        {
            UIupgradeOnStay.SetActive(true);
        }
    }

    private void Update()
    {
        _payDelay.UpdateTimer();
        _upgradeDelay.UpdateTimer();
    }

    private void PayMoney()
    {
        if (!_upgradeDelay.isReady) return;
        if (walletModel.moneyCount >= currentUpgradePrice)
        {
            var paySegment = walletModel.CalculateMoneySegment(currentUpgradeFullPrice, currentUpgradePrice, walletModel.moneyCount);

            walletModel.moneyCount -= paySegment;
            currentUpgradePrice -= paySegment;

            if (paySegment > 0)
            {
                GameData.player.walletComponent.ShowMoneyTransition(transform);
                _payDelay.Reset();
            }

            if (currentUpgradePrice == 0)
            {
                SDKEvents.upgradeModule.ConveyorUpgrade(currentConveyorController.segmentID, level,currentConveyorController.data.id);
                currentConveyorController.transform.parent.DOPunchScale(Vector3.one * 0.1f, 0.25f, 0, 1);
                Instantiate(_upgradeParticle, _upgradeParticlePoint.position, Quaternion.identity, null);
                level++;
                currentConveyorController.UpdateSkatesOnLevel();
                _upgradeDelay.Reset();
            }
        }
    }
}
