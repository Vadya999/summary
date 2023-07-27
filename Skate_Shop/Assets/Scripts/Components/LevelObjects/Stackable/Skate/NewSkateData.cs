using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(NewSkateData))]
public class NewSkateData : ScriptableObject
{
    [field: SerializeField] public int cost { get; private set; }

    [field: SerializeField] public float height { get; private set; }

    [field: SerializeField] public SkateSkinComponent prefab { get; private set; }

    [field: SerializeField, ShowAssetPreview] public Sprite conveyourSprite { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite skateShelfSprite { get; private set; }
}
