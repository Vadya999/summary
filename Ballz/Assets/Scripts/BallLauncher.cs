using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallLauncher : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    [SerializeField] private GameObject ballPrefab;
    private LauncherPreview launcherPreview;

    private void Awake()
    {
        launcherPreview = GetComponent<LauncherPreview>();    
    }

    private void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag(worldPosition);
        }
    }

    private void EndDrag(Vector3 worldPosition)
    {
        Vector3 direction = endDragPosition - startDragPosition;
        direction.Normalize();

        var ball = Instantiate(ballPrefab, transform.position, quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(-direction);
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;
        Vector3 directoin = endDragPosition - startDragPosition;
        launcherPreview.SetEndPoint(transform.position - directoin);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        launcherPreview.SetStartPoint(transform.position);
    }
}
