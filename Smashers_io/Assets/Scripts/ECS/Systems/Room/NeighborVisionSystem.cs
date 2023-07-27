using Kuhpik;
using UnityEngine;
using UnityTools.Extentions;

public class NeighborVisionSystem : GameSystem
{
    private PlayerComponent playerComponent => game.player;
    private RoomComponent activeRoom => game.activeRoom;

    private NeighborConfig _config;

    public override void OnUpdate()
    {
        foreach (var neighbor in activeRoom.neighbors)
        {
            neighbor.canSeePlayer = CanSeePlayer(neighbor);
        }
    }

    private bool CanSeePlayer(NeighborComponent neighbor)
    {
        var canSee = InRange(neighbor) && InAngle(neighbor) && CanRayHit(neighbor);
        Debug.DrawLine(neighbor.transform.position + Vector3.up, playerComponent.transform.position, canSee ? Color.green : Color.red);
        return canSee;
    }

    private bool CanRayHit(NeighborComponent neighbor)
    {
        var origin = neighbor.transform.position + Vector3.up;
        var toPlayer = GetToPlayerDirection(neighbor);
        if (Physics.Raycast(origin, toPlayer, out var hit, _config.seeRange))
        {
            var result = hit.collider.transform.IsChildOf(playerComponent.transform);
            return result;
        }
        return false;
    }

    private bool InRange(NeighborComponent neighbor)
    {
        var distance = Vector3.Distance(playerComponent.transform.position, neighbor.transform.position);
        return distance <= _config.seeRange;
    }

    private bool InAngle(NeighborComponent neighbor)
    {
        var forward = neighbor.transform.forward;
        var toPlayer = GetToPlayerDirection(neighbor);
        var normalizedAngleRange = _config.seeAngle.Remap(0, 360, 1, -1);
        var dotProduct = Vector3.Dot(forward, toPlayer);
        return dotProduct > normalizedAngleRange;
    }

    private Vector3 GetToPlayerDirection(NeighborComponent neighbor)
    {
        var result = (playerComponent.transform.position - neighbor.transform.position);
        result.y = 0;
        return result.normalized;
    }
}
