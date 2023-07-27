using Kuhpik;
using UnityEngine;

public class ItemPickupSystem : GameSystem
{
    private PlayerComponent playerCompoent => game.player;

    private WardrobeEnterSystem _wardrobeEnterSystem;

    public override void OnInit()
    {
        _wardrobeEnterSystem = GetSystem<WardrobeEnterSystem>();
    }

    public override void OnStateEnter()
    {
        playerCompoent.pickupTrigger.TriggerEntered.AddListener(OnItemEntered);
    }

    public override void OnStateExit()
    {
        playerCompoent.pickupTrigger.TriggerEntered.RemoveListener(OnItemEntered);
    }

    public override void OnUpdate()
    {
        if (game.player.activeItem != null)
        {
            game.player.activeItem.gameObject.SetActive(!_wardrobeEnterSystem.inWardrobe);
        }
        for (int i = 0; i < game.activeRoom.items.Length; i++)
        {
            var item = game.activeRoom.items[i];
            if (item != null)
            {
                item.itemBuble.gameObject.SetActive(!game.player.hasItem);
            }
        }
    }

    private void OnItemEntered(Transform self, Transform other)
    {
        if (other.TryGetComponent(out ItemComponent item) && !item.pickuped && playerCompoent.activeItem == null)
        {
            PickupItem(item);
        }
    }

    private void PickupItem(ItemComponent item)
    {
        item.SetPickupState(true);
        item.transform.parent = playerCompoent.itemRoot.transform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(0, 0, 0);
        playerCompoent.activeItem = item;
    }
}
