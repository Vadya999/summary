using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Configurations/Create CarUpgradeConfiguration", fileName = "CarUpgradeConfiguration", order = 0)]
public class CarUpgradeConfiguration : ScriptableObject
{
    [SerializeField] private int addedToStackPerLvl;
    [Header("Where 0.1 = 10% || 1 = 100%")]
    [Range(0f,1f)][SerializeField] private float addMoneyPerLvlPercent;

    public int AddedToStackPerLvl => addedToStackPerLvl;

    public float AddMoneyPerLvl => addMoneyPerLvlPercent;
}