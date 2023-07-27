using Kuhpik;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TutorialStep
{
    [field: SerializeField] public string discription { get; private set; }
    [field: SerializeField] public Transform target { get; private set; }
    [field: SerializeField] public List<MonoBehaviour> objectsToDisableOnStep { get; private set; }

    public event Action Completed;

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    protected void SetPointerTarget(Transform target)
    {
        GameData.player.pointer.SetTarget(target);
    }

    protected void Complete()
    {
        Completed?.Invoke();
    }
}
