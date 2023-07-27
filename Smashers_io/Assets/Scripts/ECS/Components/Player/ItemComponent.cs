using UnityEngine;

[SelectionBase]
public class ItemComponent : MonoBehaviour
{
    [field: SerializeField] public ItemType itemType { get; private set; }

    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _interactionRoot;

    public bool pickuped { get; private set; }
    public ItemBubble itemBuble { get; private set; }

    private void Awake()
    {
        itemBuble = GetComponentInChildren<ItemBubble>();
    }

    private void Update()
    {
        _interactionRoot.SetActive(!pickuped);
    }

    public void SetPickupState(bool state)
    {
        pickuped = state;
        _collider.enabled = !pickuped;
    }
}
