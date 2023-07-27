using System.Linq;
using Kuhpik;
using Source.Scripts.Components;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class DragItemsSystem : GameSystem
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float maxTime = 0.4f;
        
        public override void OnInit()
        {
            game.playerComponent.CollisionListener.TriggerEnterEvent += GetItem;
            game.playerComponent.CollisionListener.TriggetExitEvent += Exit;
        }

        public override void OnFixedUpdate()
        {
            if (game.currentDragItem)
            {
                var targetPoint = game.playerComponent.PlayerDragItemComponent.transform;
                var item = game.currentDragItem.transform;
                var distance = Vector3.Distance(targetPoint.position, item.position);
                var time = Time.fixedDeltaTime * distance * speed;

                if (time > maxTime) time = maxTime;

                item.transform.position = Vector3.Slerp(item.position, targetPoint.position, time);
                item.transform.rotation = Quaternion.Slerp(item.rotation, targetPoint.rotation, time);
            }
        }

        private void GetItem(Transform other)
        {
            if (game.needTakeMoney) return;
            
            var showcaseComponent = other.GetComponent<ShowcaseComponent>();

            if (showcaseComponent && game.currentDragItem == null)
            {
                if (showcaseComponent.CanGet(game.neededItemId))
                {
                    showcaseComponent.InZone(true);
                    var item = showcaseComponent.GetItem(game.neededItemId);
                    game.cashValue = (int)game.items.First(x => x.Id == game.neededItemId).Price;
                    game.playerComponent.PlayerDragItemComponent.AddItemToDrag(item, () => showcaseComponent.SpawnItem(item.Id));
                }
            }
        }
        
        private void Exit(Transform other)
        {
            var showcaseComponent = other.GetComponent<ShowcaseComponent>();

            if (showcaseComponent) showcaseComponent.InZone(false);
        }
    }
}