using UnityEngine;

[CreateAssetMenu(menuName = "Config/System/Neighbor")]
public class NeighborConfig : ScriptableObject
{
    [field: Header("Detection")]
    [field: SerializeField] public float seeRange { get; private set; }
    [field: SerializeField] public float seeAngle { get; private set; }
    [field: SerializeField] public float catchRadius { get; private set; }

    [field: Header("Navigation")]
    [field: SerializeField] public float nodeReachDistance { get; private set; }

    [field: Header("Movement")]
    [field: SerializeField] public float rotationSpeed { get; private set; }
    [field: SerializeField] public float movementSpeed { get; private set; }
}