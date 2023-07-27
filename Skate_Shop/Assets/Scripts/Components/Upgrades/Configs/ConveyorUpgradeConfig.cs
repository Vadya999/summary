using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Conveyor")]
public class ConveyorUpgradeConfig : ScriptableObject
{
    [field: SerializeField] public int[] prices { get; private set; }
    [field: SerializeField] public float upgradeValue { get; private set; }
}
