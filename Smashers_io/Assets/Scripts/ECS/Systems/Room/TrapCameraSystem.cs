using Kuhpik;
using System.Collections;
using UnityEngine;

public class TrapCameraSystem : GameSystem
{
    public void ShowNeighbor(NeighborComponent neighbor)
    {
        StartCoroutine(ShowNeighborRoutine(neighbor));
    }

    private IEnumerator ShowNeighborRoutine(NeighborComponent neighbor)
    {
        game.SetCameraTarget(neighbor.transform);
        yield return new WaitForSeconds(5);
        game.SetPlayerAsCameraTarget();
    }
}
