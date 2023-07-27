using Kuhpik;
using UnityEngine;
using UnityEngine.Events;
using UnityTools.Extentions;

public abstract class GenericTrigger<This, Other> : MonoBehaviour
{
    [SerializeField] private CollisionListener _enterTrigger;
    [SerializeField] private This _root;

    public readonly UnityEvent<This> PlayerEntered = new UnityEvent<This>();
    public readonly UnityEvent<This> PlayerExited = new UnityEvent<This>();

    private void OnEnable()
    {
        _enterTrigger.TriggerEntered.AddListener(OnTriggerEntered);
        _enterTrigger.TriggetExited.AddListener(OnTriggerExited);
    }

    private void OnDisable()
    {
        _enterTrigger.TriggerEntered.RemoveListener(OnTriggerEntered);
        _enterTrigger.TriggetExited.RemoveListener(OnTriggerExited);
    }

    private void OnTriggerEntered(Transform self, Transform other)
    {
        if (other.parent.gameObject.HasComponent<Other>())
        {
            PlayerEntered?.Invoke(_root);
        }
    }

    private void OnTriggerExited(Transform self, Transform other)
    {
        if (other.parent.gameObject.HasComponent<Other>())
        {
            PlayerExited?.Invoke(_root);
        }
    }
}
