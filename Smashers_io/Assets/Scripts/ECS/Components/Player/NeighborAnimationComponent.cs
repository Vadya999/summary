using NaughtyAttributes;
using System.Linq;
using Traps.Actions;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NeighborAnimationComponent : GenericAnimationComponent
{
    [SerializeField] private Material _redMaterial;

    [SerializeField, ReadOnly] private Renderer[] _redSkinRendereres;

    private readonly int _trapActiveTriggerID = Animator.StringToHash("TrapUse");
    private readonly int _trapActiveID = Animator.StringToHash("TrapID");

    private readonly int _sitTriggerID = Animator.StringToHash("Sit");
    private readonly int _standUpTriggerID = Animator.StringToHash("StandUp");

    private readonly int _winTriggerID = Animator.StringToHash("Win");
    private readonly int _loseTriggerID = Animator.StringToHash("Lose");

    private Material _origianMaterial;

    protected override void Awake()
    {
        base.Awake();
        if (_redSkinRendereres.Length > 0)
        {
            _origianMaterial = Instantiate(_redSkinRendereres.First().sharedMaterial);
        }
    }

    public void SetAngry(bool value)
    {
        foreach (var renderer in _redSkinRendereres)
        {
            var material = value ? _redMaterial : _origianMaterial;
            renderer.sharedMaterial = material;
            renderer.material = material;
        }
    }

    public void ShowTrapUse(TrapUseID id)
    {
        animator.SetInteger(_trapActiveID, (int)id);
        animator.SetTrigger(_trapActiveTriggerID);
    }

    public void ShowSit()
    {
        animator.SetTrigger(_sitTriggerID);
    }

    public void ShowStandUp()
    {
        animator.SetTrigger(_standUpTriggerID);
    }

    public void ShowLose()
    {
        animator.SetTrigger(_loseTriggerID);
    }

    public void ShowWin()
    {
        animator.SetTrigger(_winTriggerID);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(SetRenderers))]
    private void SetRenderers()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        _redSkinRendereres = renderers.Where(x => x.name == "Head").ToArray();
    }
#endif
}