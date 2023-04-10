using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Head Item", menuName = "Inventory/New Head Item")]
[Serializable]
public class HeadItem : ItemScriptableObject
{
    public string name;
    public int protection;
    public float weight;
}