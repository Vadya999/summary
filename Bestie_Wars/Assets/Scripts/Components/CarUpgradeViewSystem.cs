using System;
using System.Collections;
using System.Collections.Generic;
using EventBusSystem;
using Kuhpik;
using UnityEngine;

public class CarUpgradeViewSystem : GameSystem, IRecalculateAllShopPanelSignal
{
    [SerializeField] private List<CarUpgradeViewConfigurationActivation> carUpgradeViewConfigurationActivation;

    public override void OnInit()
    {
        Recalculate();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    public void Recalculate()
    {
        foreach (var carUpgradeViewConfigurationActivationCurrent in carUpgradeViewConfigurationActivation)
        {
            if (carUpgradeViewConfigurationActivationCurrent.Level == player.UpgadeLevel[UpgradeType.CarAttachLevel])
            {
                foreach (var activate in carUpgradeViewConfigurationActivationCurrent.Activate)
                {
                    activate.SetActive(true);
                }

                foreach (var disable in carUpgradeViewConfigurationActivationCurrent.Disable)
                {
                    disable.SetActive(false);
                }
            }
        }
    }
}


[Serializable]
public class CarUpgradeViewConfigurationMaterial
{
    [SerializeField] private int level;
    [SerializeField] private List<MeshRenderer> objects;
    [SerializeField] private Material enable;
    [SerializeField] private Material disable;

    public int Level => level;

    public List<MeshRenderer> Objects => objects;

    public Material Enable => enable;

    public Material Disable => disable;
}

[Serializable]
public class CarUpgradeViewConfigurationActivation
{
    [SerializeField] private int level;
    [SerializeField] private List<GameObject> activate;
    [SerializeField] private List<GameObject> disable;

    public int Level => level;

    public List<GameObject> Activate => activate;

    public List<GameObject> Disable => disable;
}