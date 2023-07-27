using System;
using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class ActivateTutorialObject : Singleton<ActivateTutorialObject>
{
    [SerializeField] private GameObject cranFirst;
    [SerializeField] private GameObject cranSecond;
    [SerializeField] private Transform unlockParking;
    [SerializeField] private Transform roadMarker;
    [SerializeField] private Transform enterRoadMarker;
    [SerializeField] private Transform destroyMarker;
    [SerializeField] private Transform legacyMarker;
    [SerializeField] private Transform upgradeMarker;
    [SerializeField] private Transform parkingZone;
    


    [SerializeField] private GameObject closeShop;
    [SerializeField] private GameObject shop;

    [SerializeField] private UpgradeTrigger upgradeTrigger;
    [SerializeField] private BuyLegacyZone buyLegacyZone;
    [SerializeField] public BuyDestroyZone BuyDestroyZone;

    public UpgradeTrigger UpgradeTrigger => upgradeTrigger;

    public Transform UpgradeMarker => upgradeMarker;

    public BuyLegacyZone BuyLegacyZone => buyLegacyZone;
    public GameObject CranFirst => cranFirst;

    public GameObject CranSecond => cranSecond;

    public Transform ParkingZone => parkingZone;

    public Transform EnterRoadMarker => enterRoadMarker;

    public Transform UnlockParking => unlockParking;

    public Transform RoadMarker => roadMarker;

    public Transform DestroyMarker => destroyMarker;

    public Transform LegacyMarker => legacyMarker;
    public GameObject CloseShop => closeShop;

    public GameObject Shop => shop;
}