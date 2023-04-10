using System;
using UnityEngine;
using UnityEngine.UI;

public enum ItemsType
{
  Consumables,
  Weapon,
  Torso,
  Head
}

[Serializable]
public class ItemScriptableObject : ScriptableObject
{
    public ItemsType itemType;
    public Sprite itemImage;
    public int maxAmount;
    public bool dragAndDrop;
}