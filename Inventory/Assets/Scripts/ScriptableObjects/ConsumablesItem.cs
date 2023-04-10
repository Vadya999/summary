using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum consumablesType
{
  typeOne,
  typeTwo
}
[CreateAssetMenu(fileName = "Consumables Item", menuName = "Inventory/New Consumables Item")]
[Serializable]
public class ConsumablesItem : ItemScriptableObject
{
    public float consumablesWeight;
    public consumablesType consumablesType;
    public string consumablesName;
}
