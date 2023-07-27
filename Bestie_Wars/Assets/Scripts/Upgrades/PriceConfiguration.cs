using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create PriceConfiguration", fileName = "PriceConfiguration", order = 0)]
public class PriceConfiguration : ScriptableObject
{
    [SerializeField] private PriceMakerConfiguration carLevelConfigurations;
    [SerializeField] private PriceMakerConfiguration carIncomeConfigurations;
    [SerializeField] private PriceMakerConfiguration carCarAttachLevelConfigurations;

    public PriceMakerConfiguration CarCarAttachLevelConfigurations => carCarAttachLevelConfigurations;

    public PriceMakerConfiguration CarLevelConfigurations => carLevelConfigurations;

    public PriceMakerConfiguration CarIncomeConfigurations => carIncomeConfigurations;
}