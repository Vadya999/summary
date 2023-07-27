using UnityEngine;

[RequireComponent(typeof(Outline))]
public class SkateSkinComponent : MonoBehaviour
{
    public Outline outline { get; private set; }

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
}
