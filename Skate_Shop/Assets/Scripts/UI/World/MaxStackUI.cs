using Kuhpik;
using UnityEngine;

public class MaxStackUI : MonoBehaviour
{
    [SerializeField] private Transform _textRoot;
    [SerializeField] private float _offsetPreStackElement;
    [SerializeField] private PlayerComponent _linkedPlayer;

    private SkateStackComponent skateStack => _linkedPlayer.skatesRoot;
    private BoxStackComponent boxStack => _linkedPlayer.boxRoot;

    private void Start()
    {
        OnStackSizeChanged();
    }

    private void OnEnable()
    {
        skateStack.Changed.AddListener(OnStackSizeChanged);
        boxStack.Changed.AddListener(OnStackSizeChanged);
    }

    private void OnDisable()
    {
        skateStack.Changed.RemoveListener(OnStackSizeChanged);
        boxStack.Changed.RemoveListener(OnStackSizeChanged);
    }

    private void OnStackSizeChanged()
    {
        _textRoot.localPosition = Vector3.up * boxStack.capacity * _offsetPreStackElement;
        _textRoot.gameObject.SetActive(boxStack.isFull && skateStack.isFull);
    }
}