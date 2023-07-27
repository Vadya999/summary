using DG.Tweening;
using EventBusSystem;
using HomaGames.HomaBelly;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZoneUnlocker : PlayerTriggerZone
{
    [SerializeField] private bool istutoorial;
    [SerializeField] private Transform activate;

    [SerializeField] private ZoneUnlockConfigurations zoneUnlockConfigurations;
    [SerializeField] private CarZone podiumZone;
    [SerializeField] private Image image;
    [SerializeField] private float time;
    [SerializeField] private TMP_Text price;

    private bool isTriggerActivated;

    private float currentTime;

    protected override void AwakeFake()
    {
        price.text = zoneUnlockConfigurations.Price.ToString();
    }

    private void Update()
    {
        if (istutoorial)
        {
            if (Bootstrap.Instance.PlayerData.IsTutorialFinish)
            {
                podiumZone.UnlockZone();
            }
        }

        if (podiumZone.IsCanBeAttach)
        {
            if (istutoorial)
            {
                activate.gameObject.SetActive(true);
            }

            Destroy(gameObject);
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
        if (Bootstrap.Instance.PlayerData.Money >= zoneUnlockConfigurations.Price)
        {
            if (istutoorial)
            {
                activate.gameObject.SetActive(true);
                activate.transform.DOShakeScale(0.3f, 0.001f);
            }

            Bootstrap.Instance.GameData.IsBuyingParking = true;
            podiumZone.UnlockZone();
            Bootstrap.Instance.PlayerData.Money -= zoneUnlockConfigurations.Price;
            EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
            VibrationSystem.PlayVibration();
            if (podiumZone.ZoneId == 3 || podiumZone.ZoneId == 4 || podiumZone.ZoneId == 5)
                HomaBelly.Instance.TrackDesignEvent($"buy_place_{podiumZone.ZoneId}");
            Bootstrap.Instance.SaveGame();
        }
    }
}