using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class TargetPointerSystem : GameSystem
{
    private Transform _target;
    private float _targetBounds;

    private Transform pointer => game.player.targetPointer.transform;

    public bool hasTarget => _target != null;

    public override void OnInit()
    {
        game.player.targetPointer.transform.SetParent(null);
    }

    public override void OnAlwaysUpdate()
    {
        game.player.targetPointer.SetActive(_target != null);
        if (_target != null)
        {
            UpdatePointer();
        }
    }

    private void UpdatePointer()
    {
        if (TryGetTarget(out var target, out var bounds))
        {
            var targetPosition = target.position;
            var playerPosition = game.player.transform.position;

            var distance = Vector3.Distance(targetPosition, playerPosition);
            var toTargetDirection = targetPosition - playerPosition;

            var farPosition = playerPosition + Vector3.up + (toTargetDirection.normalized / 2);
            var nerPosition = targetPosition + Vector3.up + Vector3.up * bounds;

            var lerpT = Mathf.Clamp(distance, 1.5f, 2.5f).Remap(1.5f, 2.5f, 0, 1);
            pointer.position = Vector3.Lerp(nerPosition, farPosition, lerpT);

            pointer.LookAt(targetPosition);
        }
    }

    private bool TryGetTarget(out Transform target, out float bounds)
    {
        if (game.activeRoom != null)
        {
            return TryGetTargetInRoom(out target, out bounds);
        }
        else
        {
            target = _target;
            bounds = _targetBounds;
            return true;
        }
    }

    private bool TryGetTargetInRoom(out Transform target, out float bounds)
    {
        var itemFloor = GetFloor(_target);
        var playerFloor = GetFloor(game.player.transform);
        target = _target;
        bounds = _targetBounds;
        if (itemFloor > playerFloor && TryGetClosestDoor(playerFloor, true, out var doorUp))
        {
            target = doorUp.transform;
            bounds = GetBound(doorUp.gameObject);
        }
        if (itemFloor < playerFloor && TryGetClosestDoor(playerFloor, false, out var doorDown))
        {
            target = doorDown.transform;
            bounds = GetBound(doorDown.gameObject);
        }
        return target != null;
    }

    private bool TryGetClosestDoor(int floor, bool up, out DoorComponent door)
    {
        door = null;
        var doors = game.activeRoom.doors;
        for (int i = 0; i < doors.Length; i++)
        {
            var currentDoor = doors[i];
            if (currentDoor.isChangingFloor && currentDoor.isUp == up && GetFloor(currentDoor.transform) == floor)
            {
                door = currentDoor;
                return true;
            }
        }
        return door != null;
    }

    private int GetFloor(Transform transform)
    {
        var yPosition = transform.position.y;
        var floor = (int)(yPosition / 3.1f);
        return floor;
    }

    private float GetBound(GameObject go)
    {
        var renderer = go.GetComponent<Renderer>();
        if (renderer == null) renderer = go.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.y;
        }
        else
        {
            return 1;
        }
    }

    public void SetTarget(Component component)
    {
        SetTarget(component.transform);
    }

    public void SetTarget(Transform target)
    {
        if (target != _target)
        {
            _target = target;
            if (target != null)
            {
                _targetBounds = GetBound(_target.gameObject);
            }
        }
    }
}
