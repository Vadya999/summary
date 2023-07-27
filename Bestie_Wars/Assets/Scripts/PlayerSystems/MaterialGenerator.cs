using System;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaterialGenerator : GameSystem
{
    [SerializeField] private List<MaterialTypeConfiguration> materialConfiguration;

    public MaterialConfiguration GetMaterial(CarMaterialType carMaterialType)
    {
        foreach (var matType in materialConfiguration)
        {
            if (matType.CarMaterialType == carMaterialType)
            {
                return matType.MaterialConfigurations[Random.Range(0, matType.MaterialConfigurations.Count)];
            }
        }
        return null;
    }
}

public enum CarMaterialType
{
    CasualCar,
    Lambo,
    ChildBus,
    Track
}

[Serializable]
public class MaterialTypeConfiguration
{
    [SerializeField] private CarMaterialType carMaterialType;
    [SerializeField] private List<MaterialConfiguration> materialConfigurations;

    public CarMaterialType CarMaterialType => carMaterialType;

    public List<MaterialConfiguration> MaterialConfigurations => materialConfigurations;
}