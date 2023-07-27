using System;
using FactoryPool;
using UnityEngine;

[Serializable]
public class EffectConfiguration
{
    [SerializeField] private EffectType effectType;
    [SerializeField] private TemporaryMonoPooled effect;

    public EffectType EffectType => effectType;

    public TemporaryMonoPooled Effect => effect;
}