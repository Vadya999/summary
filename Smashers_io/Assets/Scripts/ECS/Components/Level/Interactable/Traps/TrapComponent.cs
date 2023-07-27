using NaughtyAttributes;
using System.Collections;
using Traps.Actions;
using UnityEngine;
using UnityTools.UI;

[SelectionBase]
public class TrapComponent : MonoBehaviour
{
    [field: SerializeField] public ItemComponent requiredItem { get; private set; }
    [field: SerializeField] public TrapTrigger trigger { get; private set; }
    [field: SerializeField] public ProgressBar interactionProgress { get; private set; }
    [field: SerializeField] public Outline outline { get; private set; }
    [field: SerializeField] public PlayerTrapID trapAnimation { get; private set; }

    public ItemType itemType { get; set; }

    [SerializeReference, SubclassSelector] private TrapAction _trapAction;

    [SerializeField] private GameObject _defaultRoot;
    [SerializeField] private GameObject _preparedRoot;

    public bool preparedByPlayer { get; private set; }
    public bool usedByNeighbor { get; private set; }

    public ItemBubble interactionBubble { get; private set; }

    private NeighborComponent _activeNeighbor;

    public bool canUse => preparedByPlayer && !usedByNeighbor;

    private void Awake()
    {
        itemType = requiredItem.itemType;
        interactionProgress.SetProgress(0);
        interactionBubble = GetComponentInChildren<ItemBubble>(true);
    }

    public IEnumerator Use(NeighborComponent neighbor)
    {
        _activeNeighbor = neighbor;
        _activeNeighbor.animation.isManual = true;
        _trapAction.SetNeighbor(neighbor);
        yield return _trapAction.UseRoutine();
        neighbor.ShowAngry();
        Use();
        _activeNeighbor.animation.isManual = false;
    }

    private void Use()
    {
        usedByNeighbor = true;
        outline.enabled = false;
        UpdateState(preparedByPlayer);
    }

    public void Prepare()
    {
        preparedByPlayer = true;
        UpdateState(preparedByPlayer);
    }

    private void UpdateState(bool state)
    {
        _defaultRoot.SetActive(!state);
        _preparedRoot.SetActive(state);
    }

    private void OnDrawGizmos()
    {
        if (requiredItem != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(requiredItem.transform.position, transform.position);
        }
    }

    [ContextMenu(nameof(SetOutline))]
    private void SetOutline()
    {
        outline = GetComponentInChildren<Outline>();
    }

    [Button]
    private void ShowPrepared()
    {
        UpdateState(true);
    }

    [Button]
    private void ShowNormal()
    {
        UpdateState(false);
    }
}
