using Kuhpik;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class GameUIScreen : UIScreen
    {
        [field: SerializeField] public Joystick Joystick { get; private set; }
        [field: SerializeField] public TextMeshProUGUI MoneyText { get; private set; }

        public void UpdateMoneyText(int value)
        {
            MoneyText.text = value.ToString();
        }
    }
}