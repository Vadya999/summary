using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackSerilizationComponent : MonoBehaviour
{
    [SerializeField] private List<SkateData> _skateDatas;
    [SerializeField] private BoxComponent _box;

    public SerializableStack Serialize(List<SkateComponent> skateStack)
    {
        return new SerializableStack(skateStack.Select(x => new SerializableItem(_skateDatas.IndexOf(x.data), x.level)).ToList());
    }

    public SerializableStack Serialize(SkateStackComponent skateStack)
    {
        return new SerializableStack(skateStack.stack.Select(x => new SerializableItem(_skateDatas.IndexOf(x.data), x.level)).ToList());
    }

    public SerializableStack Serialize(BoxStackComponent boxStack)
    {
        return new SerializableStack(Enumerable.Range(0, boxStack.count).Select(x => new SerializableItem(x)).ToList());
    }

    public void Deserialize(SkateStackComponent skateStack, SerializableStack stack)
    {
        var skates = stack.items.Select(x => SpawnSkate(x.id, x.skateLevel)).ToList();
        skateStack.ForceStack(skates);
    }

    public void Deserialize(BoxStackComponent boxStack, SerializableStack stack)
    {
        var skates = Enumerable.Range(0, stack.items.Count).Select(x => Instantiate(_box)).ToList();
        boxStack.ForceStack(skates);
    }

    public SkateComponent SpawnSkate(int id, int level)
    {
        var data = _skateDatas[id];
        var skate = data.InstanceSkate(level);
        return skate;
    }
}