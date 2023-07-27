using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkateData")]
public class SkateData : ScriptableObject
{
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public float height { get; private set; }
    [field: SerializeField] public SkateComponent skatePrefab { get; private set; }
    [field: SerializeField] public List<SkateSkinComponent> skateVariants { get; private set; }
    [field: SerializeField, ShowAssetPreview] public List<Sprite> spriteConveyour { get; private set; }
    [field: SerializeField, ShowAssetPreview] public List<Sprite> spriteSkateShelf { get; private set; }

    public SkateComponent InstanceSkate(int level)
    {
        var clone = Instantiate(skatePrefab);
        clone.SetSkin(this, level);
        return clone;
    }
}