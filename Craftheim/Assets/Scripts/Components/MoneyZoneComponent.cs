using System;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class MoneyZoneComponent : MonoBehaviour
    {
        [SerializeField] private Transform spawnPos;
        [SerializeField] private ZoneUIComponent zone;

        public event Action OnMoneyGet;
        public MoneyComponent CurrentMoney { get; private set; }
        
        public void CreateMoney(ItemConfig itemConfig)
        {
            CurrentMoney = Instantiate(itemConfig.MoneyComponent, spawnPos.position, Quaternion.identity);
        }

        public MoneyComponent GetMoney()
        {
            MMVibrationManager.Haptic(HapticTypes.Selection);
            var money = CurrentMoney;
            CurrentMoney = null;
            OnMoneyGet?.Invoke();
            return money;
        }

        public void InZone(bool value)
        {
            zone.InZone(value);
        }
    }
}