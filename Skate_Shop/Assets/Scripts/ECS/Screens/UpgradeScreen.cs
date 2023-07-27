using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : UIScreen
{
    [field: SerializeField] public UpgradeUIElement stackUpgrade { get; private set; }
    [field: SerializeField] public UpgradeUIElement speedUpgrade { get; private set; }
    [field: SerializeField] public Button exitButton { get; private set; }
}
