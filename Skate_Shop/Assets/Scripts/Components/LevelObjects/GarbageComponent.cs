using DG.Tweening;
using Kuhpik;
using UnityEngine;

[SelectionBase]
public class GarbageComponent : InteractableLevelObject
{
    [SerializeField] private float _destroyDuration;
    [SerializeField] private Transform _destroyPoint;

    private SkateStackComponent skateStack => GameData.player.skatesRoot;
    private BoxStackComponent boxStack => GameData.player.boxRoot;

    protected override void Interact()
    {
        ClearStacks();
    }

    protected override bool CanInteract()
    {
        return boxStack.hasItem || skateStack.hasItem;
    }

    private void ClearStacks()
    {
        if (boxStack.hasItem)
        {
            DestroyItem(boxStack.TakeItem().gameObject);
        }
        if (skateStack.hasItem)
        {
            DestroyItem(skateStack.TakeItem().gameObject);
        }
    }

    private void DestroyItem(GameObject obj)
    {
        var transform = obj.transform;
        transform.SetParent(_destroyPoint);
        transform.DOScale(Vector3.one * 0.2f, _destroyDuration * 0.95f);
        transform.DOLocalJump(Vector3.zero, 1, 1, _destroyDuration)
            .OnComplete(() => Destroy(obj));
    }
}
