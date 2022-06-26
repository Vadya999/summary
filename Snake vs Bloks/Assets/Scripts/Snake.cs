using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSppringness;//скорочть стягивания
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _tailSize;
    
    private SnakeInput _snakeInput; 
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;

    public event UnityAction<int> SizeUpdated; 

    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _tail = _tailGenerator.Generate(_tailSize);
        _snakeInput = GetComponent<SnakeInput>();
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up  * _speed );//передаем след позицию

        _head.transform.up = _snakeInput.GetDistanceToClick(_head.transform.position);
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position;//пред позиция

        foreach (var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSppringness * Time.deltaTime);
            previousPosition = tempPosition;
        }

        _head.Move(nextPosition);    
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollided += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollided -= OnBonusCollected;
    }

    private void OnBlockCollided()
    {
        Segment delitedSegment = _tail[_tail.Count - 1];
        _tail.Remove(delitedSegment);
        Destroy(delitedSegment.gameObject);
        
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
