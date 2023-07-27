using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EventBusSystem;
using HomaGames.HomaBelly;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectZoneUnlocker : PlayerTriggerZone
{
    [SerializeField] private bool isLoker;
    [SerializeField] private bool isParkomat;
    [SerializeField] private int id;
    [SerializeField] private List<Transform> activate;
    [SerializeField] private List<Transform> disable;
    [SerializeField] private ZoneUnlockConfigurations zoneUnlockConfigurations;

    [SerializeField] private Image image;
    [SerializeField] private float time;
    [SerializeField] private TMP_Text price;

    private bool isTriggerActivated;

    private float currentTime;

    protected override void AwakeFake()
    {
        price.text = zoneUnlockConfigurations.Price.ToString();
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.3f);
        if (Bootstrap.Instance.PlayerData.unlockZone.Contains(id))
        {
            Show();
        }
        else
        {
            foreach (var a in activate)
            {
                a.gameObject.SetActive(false);
            }
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
        if (Bootstrap.Instance.PlayerData.Money >= zoneUnlockConfigurations.Price)
        {
            if (isParkomat)
            {
                HomaBelly.Instance.TrackDesignEvent($"buy_parkingmeter_{Bootstrap.Instance.PlayerData.unlockParomats}");
                Bootstrap.Instance.PlayerData.unlockParomats++;
            }

            if (isLoker)
            {
                HomaBelly.Instance.TrackDesignEvent($"buy_press2");
            }
            Bootstrap.Instance.PlayerData.unlockZone.Add(id);
            Bootstrap.Instance.PlayerData.Money -= zoneUnlockConfigurations.Price;
            EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
            EventBus.RaiseEvent<IBoughtNewZone>(t => t.BoughtZone());
            VibrationSystem.PlayVibration();
            Bootstrap.Instance.SaveGame();
            Show();
        }
    }

    private void Show()
    {
        foreach (var disa in disable)
        {
            disa.gameObject.SetActive(false);
        }

        foreach (var activate in activate)
        {
            activate.gameObject.SetActive(true);
            activate.DOShakeScale(activate.transform.localScale.x, 0.3f);
        }

        Destroy(gameObject);
    }
}