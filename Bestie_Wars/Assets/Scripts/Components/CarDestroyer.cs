using System.Collections;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class CarDestroyer : PlayerTriggerZone
{
    [SerializeField] private DestroyZone podiumZones;
    [SerializeField] private Image image;
    [SerializeField] private float time;

    private AttachCarQueueController attacheCar;
    private bool isTriggerActivated;

    private float currentTime;

    protected override void AwakeFake()
    {
        StartCoroutine(Inititalize());
        attacheCar = FindObjectOfType<AttachCarQueueController>();
    }


    private IEnumerator Inititalize()
    {
        yield return new WaitForSeconds(0.5f);
        if (Bootstrap.Instance.PlayerData.IsTutorialFinish == false)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (currentTime >= time && isTriggerActivated == false)
        {
            Activate();
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
        if (podiumZones.IsCanBeAttach)
        {
            var car = attacheCar.GetCarWithDestroy(false);
            if (car == null)
            {
                return;
            }

            podiumZones.StartDestroy(car.TransformObject.GetComponent<AttachCarController>());
            attacheCar.Recalculate();
        }
    }
}