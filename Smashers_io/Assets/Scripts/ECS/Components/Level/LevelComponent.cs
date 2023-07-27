using Cinemachine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SelectionBase]
public class LevelComponent : MonoBehaviour
{
    [field: SerializeField] public List<RoomComponent> rooms { get; private set; }
    [field: SerializeField] public ElevatorComponent elevator { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera virtaulCamera { get; private set; }


#if UNITY_EDITOR
    [Button]
    private void SetUp()
    {
        virtaulCamera.enabled = true;
        foreach (var room in rooms)
        {
            room.virtualCamera.enabled = false;
        }
    }

    [Button]
    private void ValidateLevel()
    {
        foreach (var room in rooms)
        {
            ValidateRoom(room);
        }
    }

    private void ValidateRoom(RoomComponent room)
    {
        Debug.Log($"ROOM: {room.name}");
        var nodes = room.GetComponentsInChildren<INeighborPathNode>();
        var traps = room.GetComponentsInChildren<TrapComponent>();
        var doors = room.GetComponentsInChildren<DoorComponent>();
        var items = room.GetComponentsInChildren<ItemComponent>();
        var wardrobes = room.GetComponentsInChildren<WardrobeComponent>();
        var neighbor = room.GetComponentsInChildren<NeighborComponent>();
        foreach (var door in doors)
        {
            if (door.otherDoor == null) Debug.Log($"Incomplete door: {door.name}", door);
        }
        foreach (var item in items)
        {
            if (!traps.Any(x => x.requiredItem == item))
            {
                Debug.Log($"No traps nas this item required: {item.name}", item);
            }
        }
        foreach (var trap in traps)
        {
            if (trap.requiredItem == null)
            {
                Debug.Log($"No required item set to trap: {trap.name}", trap);
            }
            if (!nodes.Any(x => x.HasTrap() && x.trap == trap))
            {
                Debug.Log($"Trap has no nodes: {trap.name}", trap);
            }
        }
        if (wardrobes.Count() <= 0) Debug.Log("No wardrobes");
        if (items.Count() <= 0) Debug.Log("No items");
        if (traps.Count() <= 0) Debug.Log("No traps");
        if (nodes.Count() <= 0) Debug.Log("No nav nodes");
        if (doors.Count() <= 0) Debug.Log("No doors");
        if (neighbor.Count() <= 0) Debug.Log("No neighbor");
    }
#endif
}