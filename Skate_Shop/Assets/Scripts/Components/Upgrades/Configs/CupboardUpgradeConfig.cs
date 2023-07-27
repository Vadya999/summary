using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Cupboard")]
public class CupboardUpgradeConfig : ScriptableObject
{
    [field: SerializeField] public int[] prices { get; private set; }
    [field: SerializeField] public float upgradeValue { get; private set; }
}

