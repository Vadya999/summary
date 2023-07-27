using System;
using System.Collections;
using UnityEngine;

public abstract class TaskQuest : ScriptableObject
{
    [SerializeField] private int amountCoin;
    [SerializeField] private bool isCycle;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;

    public string Description => description;

    public bool IsCycle => isCycle;

    public Action ProcessUpdated;
    public Sprite Sprite => sprite;

    public int AmountCoin => amountCoin;

    public abstract float ProcessDescription();
    public abstract void EnableTask();
    public abstract void DisableTask();
    public abstract bool IsTaskCompleted();
}