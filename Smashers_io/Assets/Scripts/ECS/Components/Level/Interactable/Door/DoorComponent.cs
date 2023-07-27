using Door.Animations;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class DoorComponent : MonoBehaviour, INeighborPathNode
{
    [field: SerializeField] public DoorTrigger trigger { get; private set; }
    [field: SerializeField] public DoorComponent otherDoor { get; private set; }
    [field: SerializeField] public Transform spawnPoint { get; private set; }
    [field: SerializeField] public DoorAnimationComponent animations { get; private set; }
    [field: SerializeField] public Transform animationSpawnPoint { get; private set; }

    [SerializeReference, SubclassSelector] private DoorAnimation _openAnimation;

    public Vector3 enterPoint => spawnPoint.position;
    public Vector3 exitPoint => otherDoor.spawnPoint.position;

    public bool  isChangingFloor => _openAnimation is WallDoorAnimation;
    public bool isDown => otherDoor.transform.position.y < transform.position.y && isChangingFloor;
    public bool isUp => otherDoor.transform.position.y > transform.position.y && isChangingFloor;

    public TrapComponent trap => null;

    public bool enterLock { get; set; }
    public bool inUse { get; set; }

    public IEnumerator OnEnterPoint(NeighborComponent neighbor)
    {
        yield return MoveRoutine(neighbor.transform, neighbor.animation);
    }

    public IEnumerator MoveRoutine(Transform targetRoot, GenericAnimationComponent targetAnimation)
    {
        yield return _openAnimation.OpenRoutine(this, new TargetInfo(targetRoot, targetAnimation));
    }

    private void OnDrawGizmos()
    {
        if (otherDoor != null && spawnPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(spawnPoint.position, otherDoor.spawnPoint.position);
        }
    }

    [Button]
    private void Link()
    {
        otherDoor.otherDoor = this;
    }
}
