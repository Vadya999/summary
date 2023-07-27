using UnityEngine;

public class BoxComponent : MonoBehaviour, IStackableObject
{
    [field: SerializeField] public float height { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public Outline outline { get; private set; }
}
