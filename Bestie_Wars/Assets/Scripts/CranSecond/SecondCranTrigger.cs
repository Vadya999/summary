using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class SecondCranTrigger : PlayerTriggerZone
{
    [SerializeField] private SecondCranController secondCranController;
    [SerializeField] private Image image;
    [SerializeField] private float time;

    private AttachCarQueueController attacheCar;
    private bool isTriggerActivated;

    private float currentTime;

    protected override void AwakeFake()
    {
        attacheCar = FindObjectOfType<AttachCarQueueController>();
    }

    private void Update()
    {
        if (currentTime >= time && isTriggerActivated == false)
        {
            Activate();
            isTriggerActivated = true;
        }

        if (IsPlayerInZone && currentTime != time)
        {
            if (attacheCar.IsCanBeDetachDestoryCar == false) return;
            currentTime += Time.deltaTime;
            if (currentTime > time) currentTime = time;
        }
        else
        {
            if (IsPlayerInZone == false && currentTime != 0)
            {
                currentTime -= Time.deltaTime;
                isTriggerActivated = false;
                if (currentTime < 0) currentTime = 0;
            }
        }

        image.fillAmount = currentTime / time;
    }

    private void Activate()
    {
        if (attacheCar.IsCanBeDetach == false) return;
        Bootstrap.Instance.ChangeGameState(GameStateID.SecondCranRotate);
        Bootstrap.Instance.GetSystem<CameraSystem>().ChangeCarCamera(CameraType.SecondCran);
        secondCranController.StartDestroy();
    }
}