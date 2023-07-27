using Kuhpik;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RoomCompletionSystem : GameSystemWithScreen<RoomProgressScreen>
{
    private RoomComponent activeRoom => game.activeRoom;

    private bool _seenLastFrame;

    public override void OnUpdate()
    {
        screen.SetStarCount(GetStarCount(), 1 - GetProgress());
        ProdoceNeighbors();
        CheckCompletion();
    }

    private void ProdoceNeighbors()
    {
        foreach (var neighbor in activeRoom.neighbors)
        {
            UpdateStars(neighbor);
        }
    }

    private void CheckCompletion()
    {
        if (IsCompleted())
        {
            GetSystem<WardrobeEnterSystem>().ForceExitWardrobe();
            game.player.transform.forward = -Vector3.forward;
            game.player.movement.SetPhysicsState(false);
            ChangeGameState(GameStateID.Win);
        }
    }

    public float GetProgress()
    {
        float total = config.seeToStar.Sum();
        float current = game.seenCount;
        return current / total;
    }

    public int GetStarCount()
    {
        var result = 3;
        for (int i = 0; i < config.seeToStar.Length; i++)
        {
            if (game.seenCount >= config.seeToStar[i])
            {
                result--;
            }
        }
        return result;
    }

    private bool IsCompleted()
    {
        foreach (var trap in activeRoom.traps)
        {
            if (!trap.usedByNeighbor) return false;
        }
        return true;
    }

    private void UpdateStars(NeighborComponent neighbor)
    {
        if (!_seenLastFrame && neighbor.canSeePlayer)
        {
            game.seenCount++;
        }
        _seenLastFrame = neighbor.canSeePlayer;
    }
}
