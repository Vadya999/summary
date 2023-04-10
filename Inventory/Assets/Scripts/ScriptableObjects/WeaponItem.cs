using System;
using UnityEngine;

public enum weaponType
{
    Automatic,
    Pistol
}

public enum cartridgesType
{
    TypeOne,
    TypeTwo
}

[CreateAssetMenu(fileName = "Weapon Item", menuName = "Inventory/New Weapon Item")]
[Serializable]
public class WeaponItem : ItemScriptableObject
{
    public weaponType weaponType;
    public consumablesType consumablesType;
    public int damageCount;
    public int weaponWeight;
}