using Kuhpik;
using UnityEngine;

[SelectionBase]
public class PlayerComponent : MonoBehaviour
{
    [field: SerializeField] public PlayerAnimationComponent aniamtion { get; private set; }
    [field: SerializeField] public PlayerMovementComponent movement { get; private set; }
    [field: SerializeField] public Transform itemRoot { get; private set; }
    [field: SerializeField] public CollisionListener pickupTrigger { get; private set; }
    [field: SerializeField] public GameObject targetPointer { get; private set; }
    [field: SerializeField] public Transform cameraTarget { get; private set; }

    public ItemComponent activeItem { get; set; }

    public bool hasItem => activeItem != null;
}