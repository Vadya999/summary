using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/PlayerNew")]
public class UpgradeData : ScriptableObject
{
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public string displayName { get; private set; }
    [field: SerializeField] public int maxLevel { get; private set; }
    [field: SerializeField] public int[] costList { get; private set; }
    [field: SerializeField] public int upgradedValuePerLevel { get; private set; }
}
