using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EventBusSystem;
using HomaGames.HomaBelly;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapZoneUnlocker : PlayerTriggerZone
{
    [SerializeField] private int currentId;
    [SerializeField] private List<GameObject> attachObjects;
    [SerializeField] private List<GameObject> activateObject;
    [SerializeField] private ZoneUnlockConfigurations zoneUnlockConfigurations;
    [SerializeField] private Image image;
    [SerializeField] private float time;
    [SerializeField] private TMP_Text price;

    private bool isTriggerActivated;

    private float currentTime;

    protected override void AwakeFake()
    {
        StartCoroutine(Initialize());
        price.text = zoneUnlockConfigurations.Price.ToString();
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.4f);
        if (Bootstrap.Instance.PlayerData.unlockMapZone.Contains(currentId) == false)
            foreach (var activateObject in activateObject)
            {
                activateObject.gameObject.SetActive(false);
            }
    }

    private void Update()
    {
        if (Bootstrap.Instance != null && Bootstrap.Instance.PlayerData != null)
        {
            if (Bootstrap.Instance.PlayerData.unlockMapZone.Contains(currentId))
            {
                foreach (var objectAttach in attachObjects)
                {
                    objectAttach.transform.DOScale(Vector3.zero, 0.3f)
                        .OnComplete(() => Destroy(objectAttach.gameObject));
                }

                foreach (var activateObject in activateObject)
                {
                    activateObject.SetActive(true);
                }

                gameObject.SetActive(false);
            }
        }

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
        if (Bootstrap.Instance.PlayerData.Money > zoneUnlockConfigurations.Price)
        {
            Bootstrap.Instance.PlayerData.unlockMapZone.Add(currentId);
            Bootstrap.Instance.PlayerData.Money -= zoneUnlockConfigurations.Price;
            EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
            Bootstrap.Instance.SaveGame();
            VibrationSystem.PlayVibration();
            HomaBelly.Instance.TrackDesignEvent($"buy_zone_{currentId}");
        }
    }
}