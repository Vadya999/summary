using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIElement : MonoBehaviour
{
    [field: SerializeField] public Button upgradeButton { get; private set; }
    [field: SerializeField] public TMP_Text upgradeLevel { get; private set; }
    [field: SerializeField] public TMP_Text cost { get; private set; }

    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _buttonInactiveSprite;
    [SerializeField] private Sprite _buttonActiveSprite;

    public void Redraw(Upgrade upgrade, WalletModel wallet)
    {
        if (upgrade.canUpgrade)
        {
            upgradeLevel.text = $"LVL{upgrade.level + 1}";
            cost.text = upgrade.currentCost.ToString();
            UpdateButtonUI(wallet.CanBuy(upgrade.currentCost));
        }
        else
        {
            cost.text = "MAX";
            upgradeLevel.text = "MAX";
            upgradeButton.enabled = false;
            _buttonImage.sprite = _buttonInactiveSprite;
        }
    }

    private void UpdateButtonUI(bool canBuy)
    {
        upgradeButton.enabled = canBuy;
        _buttonImage.sprite = canBuy ? _buttonActiveSprite : _buttonInactiveSprite;
    }
}
