using DG.Tweening;
using NaughtyAttributes;
using Sirenix.OdinSerializer.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemsStackComponent<T> : MonoBehaviour where T : IStackableObject
{
    [SerializeField] private Transform _stackRoot;
    [SerializeField] private float _pickupDuration;
    [SerializeField] private bool _hasCapacity;
    [SerializeField, ShowIf(nameof(_hasCapacity))] private int _baseCapacity;

    public readonly UnityEvent Changed = new UnityEvent();

    public readonly Stack<T> stack = new Stack<T>();

    public int count => stack.Count;

    public int capacity { get; set; }
    public bool isFull => count >= capacity;
    public bool hasItem => count > 0;

    private void Awake()
    {
        capacity = _hasCapacity ? _baseCapacity : int.MaxValue;
    }

    public void ForceStack(IEnumerable<T> items)
    {
        items.ForEach(item =>
        {
            stack.Push(item);
            item.transform.parent = _stackRoot;
            item.transform.localPosition = GetLocalPosition();
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);
            item.transform.localScale = Vector3.one;
        });
    }

    public void AddToStack(T stackableObject)
    {
        var localPosition = GetLocalPosition();
        stackableObject.transform.parent = _stackRoot;
        stackableObject.transform.DOLocalJump(localPosition, 1, 1, _pickupDuration);
        stackableObject.transform.DOLocalRotate(Vector3.zero, _pickupDuration).WaitForCompletion();
        stack.Push(stackableObject);
        Changed?.Invoke();
    }

    public void MoveToAnotherStack(ItemsStackComponent<T> anotherStack)
    {
        var objectToStackRemove = stack.Pop();
        anotherStack.AddToStack(objectToStackRemove);
        Changed?.Invoke();
    }

    public T TakeItem()
    {
        var result = stack.Pop();
        Changed?.Invoke();
        return result;
    }

    public T MoveToPoint(Transform point, out Tween tween)
    {
        var item = TakeItem();
        item.transform.parent = point;
        item.transform.DOLocalRotate(Vector3.zero, _pickupDuration);
        tween = item.transform.DOLocalJump(Vector3.zero, 1, 1, _pickupDuration);
        return item;
    }

    private Vector3 GetLocalPosition()
    {
        return Vector3.up * stack.Select(x => x.height).Sum();
    }
}

