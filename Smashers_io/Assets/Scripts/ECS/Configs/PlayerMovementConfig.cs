using UnityEngine;

[CreateAssetMenu(menuName = "Config/System/PlayerMovement")]
public class PlayerMovementConfig : ScriptableObject
{
    [field: Header("Movement")]
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float rotationSpeed { get; private set; }
}
