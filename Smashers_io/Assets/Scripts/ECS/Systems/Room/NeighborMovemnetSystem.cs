using Kuhpik;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NeighborMovemnetSystem : GameSystem
{
    private RoomComponent activeRoom => game.activeRoom;
    private PlayerComponent playerComponent => game.player;

    private NeighborConfig _config;

    private LevelLoadingSystem _levelLoadingSystem;
    private WardrobeEnterSystem _wardrobeSystem;
    private TrapTipSystem _trapTipSystem;

    public bool isShowingTrapAnimation { get; private set; }

    public override void OnInit()
    {
        _levelLoadingSystem = GetSystem<LevelLoadingSystem>();
        _wardrobeSystem = GetSystem<WardrobeEnterSystem>();
        _trapTipSystem = GetSystem<TrapTipSystem>();
    }

    public override void OnUpdate()
    {
        foreach (var neighbor in activeRoom.neighbors)
        {
            Produce(neighbor);
        }
    }

    private void Produce(NeighborComponent neighbor)
    {
        TryCatchPlayer(neighbor);
        if (neighbor.nodeLock || _trapTipSystem.isShowingTip) return;
        neighbor.animation.isWalking = !neighbor.nodeLock;
        if (neighbor.canSeePlayer && !_wardrobeSystem.inWardrobe)
        {
            ChasePlayer(neighbor);
        }
        else
        {
            FollowPath(neighbor);
        }
    }

    private void ChasePlayer(NeighborComponent neighbor)
    {
        MoveTo(neighbor, playerComponent.transform.position);
    }

    private void TryCatchPlayer(NeighborComponent neighbor)
    {
        var distance = Vector3.Distance(playerComponent.transform.position, neighbor.transform.position);
        if (distance < _config.catchRadius)
        {
            ChangeGameState(GameStateID.Lose);
        }
    }

    private void FollowPath(NeighborComponent neighbor)
    {
        if (HasReachNode(neighbor))
        {
            if (neighbor.currentNode.HasTrap(out var trap) && trap.canUse)
            {
                GetSystem<TrapCameraSystem>().ShowNeighbor(neighbor);
                StartCoroutine(TrapUseRoutine(neighbor, trap.Use(neighbor)));
            }
            else
            {
                StartCoroutine(NodeInteractionRoutine(neighbor, neighbor.currentNode.OnEnterPoint(neighbor)));
            }
        }
        else
        {
            MoveToNode(neighbor);
        }
    }

    private IEnumerator TrapUseRoutine(NeighborComponent neighbor, IEnumerator routine)
    {
        neighbor.nodeLock = true;
        isShowingTrapAnimation = true;
        yield return routine;
        isShowingTrapAnimation = false;
        neighbor.nodeLock = false;
        neighbor.currentNodeID++;
    }

    private IEnumerator NodeInteractionRoutine(NeighborComponent neighbor, IEnumerator routine)
    {
        neighbor.nodeLock = true;
        neighbor.currentNode.inUse = true;
        yield return routine;
        neighbor.currentNode.inUse = false;
        neighbor.nodeLock = false;
        neighbor.currentNodeID++;
    }

    private void MoveTo(NeighborComponent neighbor, Vector3 point)
    {
        var direction = neighbor.GetDirectionTo(point);
        var delta = direction * _config.movementSpeed * Time.deltaTime;
        neighbor.transform.position += delta;

        if (direction != Vector3.zero)
        {
            var targetDirection = Quaternion.LookRotation(direction.normalized, Vector3.up);
            var finalDirection = Quaternion.RotateTowards(neighbor.transform.rotation, targetDirection, _config.rotationSpeed * Time.deltaTime);
            neighbor.transform.rotation = finalDirection;
        }
    }

    private void MoveToNode(NeighborComponent neighbor)
    {
        MoveTo(neighbor, neighbor.currentNode.enterPoint);
    }

    private bool HasReachNode(NeighborComponent neighbor)
    {
        var currnetNode = neighbor.currentNode;
        var distance = Vector3.Distance(currnetNode.enterPoint, neighbor.transform.position);
        return distance <= _config.nodeReachDistance;
    }
}
