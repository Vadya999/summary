using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaitingPointTrigger : PlayerTriggerZone
{
    [SerializeField] private List<CarZone> carZones;
    [SerializeField] private float time;
    [SerializeField] private Image image;

    private AttachCarQueueController playerQueue;
    private bool isTriggerActivated;
   
    private float currentTime;

    protected override void AwakeFake()
    {
        playerQueue = FindObjectOfType<AttachCarQueueController>();
    }

    private void Update()
    {
        if (currentTime >= time && isTriggerActivated == false)
        {
            isTriggerActivated = true;
            ActivateTrigger();
        }

        if (IsPlayerInZone && currentTime != time)
        {
            if (playerQueue.IsCanBeDetachCar == false) return;
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

    private void ActivateTrigger()
    {
        var carZonesSort = carZones.OrderBy(e=>e.IsAttachEmpty ? 0 : 1).ToList();
        foreach (var carZone in carZonesSort)
        {
            if (playerQueue.IsCanBeDetach == false) break;
            if (carZone.IsCanBeAttach)
            {
                var car = playerQueue.GetCarWithoutDestroyAndLegacy();
                if (car == null) break;
                var attachZone = carZone.TryToDetach();
                if (attachZone != null)
                {
                    playerQueue.AttachWithoutJump(attachZone);
                    carZone.Attach(car.TransformObject.GetComponent<AttachCarController>());
                    break;
                }
                else
                {
                    carZone.Attach(car.TransformObject.GetComponent<AttachCarController>());
                }
            }
        }
        playerQueue.Recalculate();
        
    }
    

    protected override void PlayerExitZone()
    {
        
    }
}