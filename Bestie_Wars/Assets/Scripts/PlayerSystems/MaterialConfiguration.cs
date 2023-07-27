using System;
using UnityEngine;

[Serializable]
public class MaterialConfiguration
{
    [SerializeField] private Material casual;
    [SerializeField] private Material destroy;
    [SerializeField] private Material special;

    public Material Casual => casual;

    public Material Destroy => destroy;

    public Material Special => special;
}