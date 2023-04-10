using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Torso Item", menuName = "Inventory/New Torso Item")]
[Serializable]
public class TorsoItem : ItemScriptableObject
{
    public string name;
    public int protection;
    public float weight;
}