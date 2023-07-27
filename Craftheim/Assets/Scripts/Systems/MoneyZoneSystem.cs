using System.Collections;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class MoneyZoneSystem : GameSystem
    {
        private bool playerInzone;
        private bool geted;

        public override void OnInit()
        {
            game.playerComponent.CollisionListener.TriggerEnterEvent += TryGetMoney;
            game.playerComponent.CollisionListener.TriggetExitEvent += Exit;
        }

        public override void OnUpdate()
        {
            if (!game.moneyZoneComponent.CurrentMoney) return;

            if (playerInzone)
            {
                game.moneyZoneComponent.InZone(true);
                Supyrb.Signals.Get<ReplaceCustomerPackageSignal>().Dispatch();
                var money = game.moneyZoneComponent.GetMoney();
                Destroy(money.gameObject.GetComponent<Rigidbody>());
                money.Coll.enabled = false;
                game.playerComponent.PlayerDragItemComponent.AddItemToDrag(money);
            }
        }

        private void Exit(Transform other)
        {
            var zoneMoney = other.GetComponent<MoneyZoneComponent>();

            if (zoneMoney)
            {
                playerInzone = false;
                zoneMoney.InZone(false);
            }
        }

        private void TryGetMoney(Transform other)
        {
            var zoneMoney = other.GetComponent<MoneyZoneComponent>();

            if (zoneMoney)
            {
                playerInzone = true;
            }
        }
    }
}