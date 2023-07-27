using EventBusSystem;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyLegacyZone : PlayerTriggerZone
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private int priceI = 50;
    [SerializeField] private Image image;
    [SerializeField] private float time;
    [SerializeField] private TMP_Text price;

    private bool isTriggerActivated;

    private float currentTime;

    protected override void AwakeFake()
    {
        price.text = priceI.ToString();
    }

    private void Update()
    {
        if (Bootstrap.Instance.PlayerData.IsTutorialFinish)
        {
            Destroy(gameObject);
            trigger.gameObject.SetActive(true);
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
        Bootstrap.Instance.GameData.IsBuyingLegacy = true;
        trigger.gameObject.SetActive(true);
        Bootstrap.Instance.PlayerData.Money -= priceI;
        EventBus.RaiseEvent<IUpdateMoney>(t => t.UpdateMoney());
        Destroy(gameObject); VibrationSystem.PlayVibration();
    }
}