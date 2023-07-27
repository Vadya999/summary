using UnityEngine;

[CreateAssetMenu(menuName = "ConfigurationsCreate MapPlayerConfiguration", fileName = "MapPlayerConfiguration", order = 0)]
public class MapPlayerConfiguration : ScriptableObject
{
    [SerializeField] private int amountCharacterPerCar = 3;
    [SerializeField] private int amountMoney = 10;

    public int AmountCharacterPerCar => amountCharacterPerCar;

    public int AmountMoney => amountMoney;
}