using UnityEngine;

public class LadderZoneComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovementComponent player))
        {
            player.isOnLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovementComponent player))
        {
            player.isOnLadder = false;
        }
    }
}
