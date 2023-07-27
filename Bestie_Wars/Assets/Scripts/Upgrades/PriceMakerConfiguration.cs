using System;
using UnityEngine;

[Serializable]
public class PriceMakerConfiguration
{
    [SerializeField] private int startPrice;
    [SerializeField] private int addPerLevel;
    [SerializeField] private int maxLevel;

    public int MAXLevel => maxLevel;

    public int StartPrice => startPrice;

    public int AddPerLevel => addPerLevel;
}