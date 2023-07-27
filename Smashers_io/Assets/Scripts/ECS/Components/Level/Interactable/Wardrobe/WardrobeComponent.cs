using UnityEngine;

[SelectionBase]
public class WardrobeComponent : MonoBehaviour
{
    [field: SerializeField] public WardrobeTrigger trigger { get; private set; }
    [field: SerializeField] public Transform inPoint { get; private set; }
    [field: SerializeField] public Transform exitPoint { get; private set; }
    [field: SerializeField] public Outline outline { get; private set; }

    [ContextMenu(nameof(SetOutline))]
    private void SetOutline()
    {
        outline = GetComponentInChildren<Outline>() ?? GetComponent<Outline>();
    }
}
