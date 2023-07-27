using Cinemachine;
using Kuhpik;
using System;
using UnityEngine;

public delegate void TutorialStepID(int id, bool relatively);

[Serializable]
public abstract class TutorialStep
{
    [field: SerializeField] public bool hasLabel { get; private set; }
    [field: SerializeField] public string label { get; private set; }
    [field: SerializeField] public string sdkName { get; private set; }

    public event Action Completed;
    public event TutorialStepID ChangeStepID;

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    protected GameConfig config => Bootstrap.Instance.gameConfig;
    protected GameData gameData => Bootstrap.Instance.GameData;
    private CinemachineVirtualCamera playerCamera => gameData.activeCamera;

    public bool hasSDKName => !string.IsNullOrEmpty(sdkName);

    protected void ForceStepID(int stepID, bool relatively)
    {
        ChangeStepID?.Invoke(stepID, relatively);
    }

    protected void Complete()
    {
        Completed?.Invoke();
    }

    protected void SetPointerTarget(Transform target)
    {
        GetSystem<TargetPointerSystem>().SetTarget(target);
    }

    protected void SetPointerTarget(Component component)
    {
        GetSystem<TargetPointerSystem>().SetTarget(component.transform);
    }

    protected T GetSystem<T>() where T : GameSystem
    {
        return Bootstrap.Instance.GetSystem<T>();
    }

    protected T GetScreen<T>() where T : UIScreen
    {
        return UIManager.GetUIScreen<T>();
    }

    protected void MoveCameraToPoint(Transform point, Action onComplete)
    {
        GetSystem<TrapTipSystem>().MoveCameraToPoint(point, onComplete);
    }

    protected void ShowTutorialPoint(Transform point)
    {
        GetSystem<TrapTipSystem>().MoveCameraToPoint(point);
    }

    public void AddUIPointer(Component component)
    {
        AddUIPointer(component.GetComponent<RectTransform>());
    }

    protected void AddUIPointer(RectTransform transform)
    {
        if (!HasUIPointer(transform))
        {
            GameObject.Instantiate(config.tutorialUIPointer, transform);
        }
    }

    protected void RemoveUIPointer(Component component)
    {
        RemoveUIPointer(component.GetComponent<RectTransform>());
    }

    protected void RemoveUIPointer(RectTransform transform)
    {
        if (TryGetUIPointer(transform, out var pointer))
        {
            GameObject.Destroy(pointer.gameObject);
        }
    }

    private bool TryGetUIPointer(RectTransform transform, out TutorialPointerComponent pointer)
    {
        pointer = transform.GetComponentInChildren<TutorialPointerComponent>();
        return pointer != null;
    }

    private bool HasUIPointer(RectTransform transform)
    {
        return transform.gameObject.GetComponentInChildren<TutorialPointerComponent>() != null;
    }
}
