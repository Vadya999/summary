using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create ZoneUnlockConfigurations", fileName = "ZoneUnlockConfigurations", order = 0)]
public class ZoneUnlockConfigurations : ScriptableObject
{
    [SerializeField] private int price;

    public int Price => price;
}