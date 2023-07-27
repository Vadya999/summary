using Kuhpik;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCountScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI textCounter;
    [SerializeField] private Button _moneyCheatButton;

    private WalletModel walletModel => GameData.walletModel;

    public override void Subscribe()
    {
        OnChangeMoneyValue(walletModel);
    }

    private void OnEnable()
    {
        walletModel.WalletChanged.AddListener(OnChangeMoneyValue);
        _moneyCheatButton.onClick.AddListener(AddMoney);
    }

    private void OnDisable()
    {
        walletModel.WalletChanged.RemoveListener(OnChangeMoneyValue);
        _moneyCheatButton.onClick.RemoveListener(AddMoney);
    }

    private void OnChangeMoneyValue(WalletModel walletModel)
    {
        textCounter.text = walletModel.moneyCount.ToString();
    }

    private void AddMoney()
    {
        Add();
    }

    [Conditional("Debug")]
    private void Add()
    {
        GameData.walletModel.moneyCount += 100;
    }
}
