using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class ConvyourSerializationSegment : SerializationSegment
{
    [SerializeField] private StackSerilizationComponent _stackSerilizationComponent;

    public override void Save(SaveData saveData)
    {
        var conveyours = FindObjectsOfType<ConveyorController>();
        foreach (var convyour in conveyours)
        {
            var boxStack = _stackSerilizationComponent.Serialize(convyour.boxStack);
            var skateStack = _stackSerilizationComponent.Serialize(convyour.EndConveyorLoad.skateStack);
            var id = convyour.transform.parent.GetSiblingIndex();
            var level = convyour.upgrade.level;
            var data = new SerilizableConvyor(boxStack, skateStack, level, id);
            saveData.level.conveyour.Add(data);
        }
    }

    public override void Load(SaveData saveData)
    {
        var goConveyours = FindObjectsOfType<ConveyorController>();
        foreach (var convyour in saveData.level.conveyour)
        {
            var goConveyor = goConveyours.FirstOrDefault(x => ValidID(x.transform, convyour));
            _stackSerilizationComponent.Deserialize(goConveyor.boxStack, convyour.boxStack);
            _stackSerilizationComponent.Deserialize(goConveyor.EndConveyorLoad.skateStack, convyour.skateStack);
            goConveyor.upgrade.level = convyour.upgradeLevel;
        }
    }

    private bool ValidID(Transform transform, SerilizableConvyor conveyour)
    {
        var index = transform.parent.GetSiblingIndex();
        return index == conveyour.id;
    }
}