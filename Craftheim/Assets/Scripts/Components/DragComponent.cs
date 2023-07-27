using UnityEngine;

namespace Source.Scripts.Components
{
    public class DragComponent : MonoBehaviour
    {
        [field:SerializeField] public float PosY { get; private set; }
        [field:SerializeField] public float PosZ { get; private set; }
        [field:SerializeField] public Vector3 Rotation { get; private set; }
        [field:SerializeField] public Collider Coll { get; private set; }
    }
}