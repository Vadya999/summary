using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityTools;
using UnityTools.Extentions;

public class CupboardUpgrade : MonoBehaviour
{
    [SerializeField] private CupboardUpgradeConfig _upgradeConfig;
    [SerializeField] private LevelObjectUpgradeUI _upgradeUI;
    [SerializeField] private CupboardComponent _linkedCupboard;

    [SerializeField] private int maxValueToUpgrade;

    [SerializeField] private GameObject UINameUpgrade;

    private int[] priceUpgrades => _upgradeConfig.prices;

    private Timer _payDelay = new Timer(0.1f);

    private int[] _fullPriceUpgrades;

    private int _level;
    private int level
    {
        get => _level;
        set
        {
            _level = value;
            UpdateUI();
        }
    }

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

    private WalletModel walletModel => GameData.walletModel;
    private PlayerWalletComponent walletComponent => GameData.player.walletComponent;

    private void Awake()
    {
        _upgradeConfig = Instantiate(_upgradeConfig);
    }

    private void Start()
    {
        _fullPriceUpgrades = priceUpgrades.ToArray();
        UINameUpgrade.SetActive(false);
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
                PayMoneyCoroutine();
            }

        }
    }

    private void Update()
    {
        _payDelay.UpdateTimer();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.HasComponent<PlayerComponent>())
        {
            UINameUpgrade.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.HasComponent<PlayerComponent>())
        {
            UINameUpgrade.SetActive(true);
        }
    }

    private void PayMoneyCoroutine()
    {
        if (walletModel.moneyCount >= currentUpgradePrice)
        {
            var paySegment = walletModel.CalculateMoneySegment(currentUpgradeFullPrice, currentUpgradePrice, walletModel.moneyCount);
            walletModel.moneyCount -= paySegment;
            currentUpgradePrice -= paySegment;

            if (paySegment > 0)
            {
                walletComponent.ShowMoneyTransition(transform);
                _payDelay.Reset();
            }

            if (currentUpgradePrice == 0)
            {
                var timer = _linkedCupboard.useTimer;
               //CupboardComponent.stackBoxFillingDelay -= _upgradeConfig.upgradeValue;
                level++;
            }
        }
    }
}
