using System;
using DG.Tweening;
using Kuhpik;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class CashBoxComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] positions;
        [SerializeField] private ZoneUIComponent zone;

        public event Action OnMoneyPut;
        
        public void Put(MoneyComponent moneyComponent, Action onComplete = null)
        {
            Bootstrap.Instance.GameData.canMove = false;
            Bootstrap.Instance.GameData.playerComponent.PlayerDragItemComponent.RemoveItemToDrag();
            var pos = positions[moneyComponent.CashIndex];
            
            moneyComponent.transform.DORotate(pos.eulerAngles, 0.5f);
            moneyComponent.transform.DOJump(pos.position,1.5f, 1, 0.5f).OnComplete(() =>
            {
                MMVibrationManager.Haptic(HapticTypes.Selection);
                OnMoneyPut?.Invoke();
                moneyComponent.Coll.enabled = true;
                moneyComponent.gameObject.AddComponent<Rigidbody>();
                
                Bootstrap.Instance.GameData.canMove = true;
                onComplete?.Invoke();
            });
        }

        public void InZone(bool value)
        {
            zone.InZone(value);
        }
    }
}