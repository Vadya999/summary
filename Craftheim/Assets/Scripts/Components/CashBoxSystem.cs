using HomaGames.HomaBelly;
using Kuhpik;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class CashBoxSystem : GameSystem
    {
        public override void OnInit()
        {
            game.playerComponent.CollisionListener.TriggerEnterEvent += PutMoney;
            game.playerComponent.CollisionListener.TriggetExitEvent += Exit;
        }

        private void Exit(Transform other)
        {
            var cashBoxComponent = other.GetComponent<CashBoxComponent>();

            if (cashBoxComponent)
            {
                cashBoxComponent.InZone(false);
            }
        }

        private void PutMoney(Transform other)
        {
            var cashBoxComponent = other.GetComponent<CashBoxComponent>();

            if (cashBoxComponent)
            {
                if (game.currentDragItem is MoneyComponent money)
                {
                    cashBoxComponent.InZone(true);
                    game.needTakeMoney = false;
                    player.Money += game.cashValue;
                    game.cashValue = 0;
                    cashBoxComponent.Put(money);

                    player.itemsCompleted++;
                    Bootstrap.Instance.SaveGame();
                    HomaBelly.Instance.TrackDesignEvent($"level_{player.itemsCompleted}_completed");
                }
            }
        }
    }
}