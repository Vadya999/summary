using Cinemachine;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    [field: SerializeField] public RoomEnterTrigger enterTrigger { get; private set; }
    [field: SerializeField] public RoomEnterTrigger exitTrigger { get; private set; }
    [field: SerializeField] public Transform startPoint { get; private set; }
    [field: SerializeField] public Transform endPoint { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera virtualCamera { get; private set; }
    [field: SerializeField] public RoomProgressUI progress { get; private set; }

    public WardrobeComponent[] wardrobes { get; private set; }
    public DoorComponent[] doors { get; private set; }
    public TrapComponent[] traps { get; private set; }
    public NeighborComponent[] neighbors { get; private set; }
    public ItemComponent[] items { get; private set; }

    private void Awake()
    {
        wardrobes = GetComponentsInChildren<WardrobeComponent>();
        doors = GetComponentsInChildren<DoorComponent>();
        traps = GetComponentsInChildren<TrapComponent>();
        neighbors = GetComponentsInChildren<NeighborComponent>();
        items = GetComponentsInChildren<ItemComponent>();
    }
}
