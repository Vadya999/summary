using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [field: SerializeField] public Rigidbody rb { get; private set; }
    [field: SerializeField] public Collider collider { get; private set; }

    public void SetPhysicsState(bool state)
    {
        rb.isKinematic = !state;
        collider.enabled = state;
    }
}
