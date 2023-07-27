using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HomaGames.HomaBelly;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Podium : PlayerTriggerZone
{
    [SerializeField] private List<PodiumZone> podiumZones;
    [SerializeField] private Image image;
    [SerializeField] private float time;

    public AttachCarController last { get; private set; }

    private AttachCarQueueController attacheCar;
    private bool isTriggerActivated;
    private float currentTime;

    public int MaxAttach => podiumZones.Count;
    public int AmountPodium => podiumZones.Count(t => t.IsCanBeAttach == false);
    public Transform randomCarPosition => podiumZones[Random.Range(0, podiumZones.Count)].transform;

    protected override void AwakeFake()
    {
        attacheCar = FindObjectOfType<AttachCarQueueController>();
        StartCoroutine(Inititalize());
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
            isTriggerActivated = true;
            Activate();
        }

        if (IsPlayerInZone && currentTime != time)
        {
            if (attacheCar.IsCanBeDetachLegacyCar == false) return;
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
        foreach (var carZone in podiumZones)
        {
            if (attacheCar.IsCanBeDetach == false) return;
            if (carZone.IsCanBeAttach)
            {
                var car = attacheCar.GetCarLegacy();
                if (car == null)
                {
                    return;
                }

                last = car.TransformObject.GetComponent<AttachCarController>();
                carZone.Activate(car.TransformObject.GetComponent<AttachCarController>());
                attacheCar.Recalculate();
                Bootstrap.Instance.ChangeGameState(GameStateID.Result);
                last.enabled = false;
                HomaBelly.Instance.TrackDesignEvent($"collect_car_vip{Bootstrap.Instance.PlayerData.amounLegacy}");
                Save();
            }
        }
    }

    private void Save()
    {
        Bootstrap.Instance.PlayerData.saveCar = new List<int>();
        foreach (var podium in podiumZones)
        {
            if (podium.IsCanBeAttach == false)
            {
                Bootstrap.Instance.PlayerData.saveCar.Add(podium.AttachCarId);
            }
        }

        Bootstrap.Instance.SaveGame();
    }

    public void Attach(AttachCarController attachCarController)
    {
        foreach (var carZone in podiumZones)
        {
            if (carZone.IsCanBeAttach)
            {
                last = attachCarController;
                carZone.Activate(last);
                last.enabled = false;
                break;
            }
        }
    }
}