using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapArrow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Transform playerCar;
    [SerializeField] private float disableDistance;
    [SerializeField] private Transform player;
    [SerializeField] private Transform target;
    [SerializeField] private Transform arrow;
    [SerializeField] private float upSetting;
    [SerializeField] private float forwardSetting;

    private void Update()
    {
        image.enabled = !(Vector3.Distance(target.position, playerCar.position) < disableDistance);
        RotateToObject();
    }

    public void RotateToObject()
    {
        var playerPosition = player.position;
        playerPosition.y = 0;
        var targerPos = target.position;
        targerPos.y = 0;
        var direction = targerPos - playerPosition;
        Quaternion arrowRotation = Quaternion.LookRotation(direction);

        var pos = playerPosition + Vector3.up * upSetting;
        pos = pos + arrow.forward * forwardSetting;
        arrow.position = Vector3.Lerp(arrow.position, pos, 25 * Time.deltaTime);
        arrow.rotation =
            Quaternion.Lerp(arrow.rotation, arrowRotation, 25 * Time.deltaTime);
    }
}