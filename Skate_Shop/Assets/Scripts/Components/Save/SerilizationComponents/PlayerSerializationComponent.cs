using Kuhpik;
using UnityEngine;

public class PlayerSerializationComponent : SerializationSegment
{
    [SerializeField] private StackSerilizationComponent _stackSerilizationComponent;

    private PlayerComponent _player => GameData.player;

    public override void Load(SaveData data)
    {
        _player.transform.position = data.playerTransform.position;
        _stackSerilizationComponent.Deserialize(_player.skatesRoot, data.skateStack);
        _stackSerilizationComponent.Deserialize(_player.boxRoot, data.boxStack);
    }

    public override void Save(SaveData data)
    {
        data.playerTransform = new SerializableTransform(_player.transform);
        data.skateStack = _stackSerilizationComponent.Serialize(_player.skatesRoot);
        data.boxStack = _stackSerilizationComponent.Serialize(_player.boxRoot);
    }
}
