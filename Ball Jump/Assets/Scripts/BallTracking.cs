using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTracking : MonoBehaviour
{
    private Beam _beam;
    private Ball _ball;
    private Vector3 _cameraPosition;
    private Vector3 _minimakBallPosition;
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _lenght;

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _beam = FindObjectOfType<Beam>();

        _cameraPosition = _ball.transform.position;
        _minimakBallPosition = _ball.transform.position;
        
        TrackBall();
    }

    private void Update()
    {
        if (_ball.transform.position.y < _minimakBallPosition.y)
        {
            TrackBall();
            _minimakBallPosition = _ball.transform.position;
        }
    }

    private void TrackBall()
    {
        Vector3 beamPosition = _beam.transform.position;
        beamPosition.y = _ball.transform.position.y;
        
        _cameraPosition = _ball.transform.position;
        Vector3 direction = (beamPosition - _ball.transform.position).normalized + _directionOffset;
        _cameraPosition = direction * _lenght;
        transform.position = _cameraPosition;
        transform.LookAt(_ball.transform);
    }
}
