using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(SkateGroup))]
public class SkateGroup : ScriptableObject
{
    [field: SerializeField] public List<NewSkateData> skates { get; private set; }
}