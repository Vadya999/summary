using Kuhpik;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class PlayerComponent : MonoBehaviour
    { 
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CollisionListener CollisionListener { get; private set; }
        [field: SerializeField] public CollisionListener ScannerListener { get; private set; }
        [field: SerializeField] public PlayerDragItemComponent PlayerDragItemComponent { get; private set; }
    }
}