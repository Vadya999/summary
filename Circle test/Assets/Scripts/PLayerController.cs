using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    [SerializeField] private GameObject _deadScreen;
    [SerializeField] private GameObject _winScreen;
    
    private bool _isMoving;
    private bool _gameOver;

    private List<Vector3> _targetPositions = new List<Vector3>();
    private Vector3 _targetPosition;

    private float _speed = 2f;
    private float _money;
    private float _moneyInSceen = 8f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        if (_isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPositions[_targetPositions.Count - 1], _speed * Time.deltaTime);

        if (transform.position == _targetPositions[_targetPositions.Count - 1])
        {
            _targetPositions.Remove(_targetPositions.Last());

            if (_targetPositions.Count == 0 )
            {
                _isMoving = false;
            }
        }
    }

    private void SetTargetPosition()
    {
        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = transform.position.z;
        
        _targetPositions.Add(_targetPosition);

        _isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rock"))
        {
            Destroy(gameObject);

            _gameOver = true;
            
            _deadScreen.SetActive(true);
        }
        if (other.CompareTag("Money"))
        {
            _money++;
            
            Destroy(other.gameObject);

            if (_money == _moneyInSceen)
            {
                _winScreen.SetActive(true);
            }
        }
    }
    
}
