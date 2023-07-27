using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class SkateShelfSerilizationComponent : SerializationSegment
{
    [SerializeField] private StackSerilizationComponent _stackSerilizationComponent;

    public override void Save(SaveData saveData)
    {
        var shelfs = FindObjectsOfType<SkateShelfComponent>();
        foreach (var shelf in shelfs)
        {
            saveData.level.shelfs.Add(Serialize(shelf, shelf.transform.GetSiblingIndex()));
        }
    }

    public override void Load(SaveData saveData)
    {
        var shelfs = FindObjectsOfType<SkateShelfComponent>();
        foreach (var shelf in saveData.level.shelfs)
        {
            var shelfGO = shelfs.FirstOrDefault(x => x.transform.GetSiblingIndex() == shelf.id);
            Deserialize(shelfGO, shelf);
        }
    }

    private SerializablSkateShelf Serialize(SkateShelfComponent skateShelf, int id)
    {
        return new SerializablSkateShelf(_stackSerilizationComponent.Serialize(skateShelf.skateListInSheif), id);
    }

    private void Deserialize(SkateShelfComponent skateShelf, SerializablSkateShelf shateShelf)
    {
        var skates = shateShelf.stack.items.Select(x => _stackSerilizationComponent.SpawnSkate(x.id, x.skateLevel)).ToList();
        skateShelf.ForceStack(skates);
    }
}
