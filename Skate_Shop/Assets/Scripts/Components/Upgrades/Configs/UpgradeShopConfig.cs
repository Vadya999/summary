using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Player")]
public class UpgradeShopConfig : ScriptableObject
{
    [field: SerializeField] public UpgradeData speedUpgrade { get; private set; }
    [field: SerializeField] public UpgradeData capacityUpgrade { get; private set; }
    [field: SerializeField] public ParticleSystem upgradeEffect { get; private set; }
}