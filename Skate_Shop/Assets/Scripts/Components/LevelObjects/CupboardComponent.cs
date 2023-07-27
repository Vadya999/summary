using Kuhpik;
using UnityEngine;

[SelectionBase]
public class CupboardComponent : InteractableLevelObject
{
    [SerializeField] private BoxComponent stackBoxPrefab;
    [SerializeField] private Transform instansPositionStackBox;

    private BoxStackComponent boxStack => GameData.player.boxRoot;

    protected override bool CanInteract()
    {
        return !boxStack.isFull;
    }

    protected override void Interact()
    {
        GiveBoxToPlayer();
    }

    private void GiveBoxToPlayer()
    {
        var stackBox = Instantiate(stackBoxPrefab, instansPositionStackBox.position, Quaternion.identity);
        stackBox.outline.UpdateMaterialProperties();
        boxStack.AddToStack(stackBox);
    }
}
