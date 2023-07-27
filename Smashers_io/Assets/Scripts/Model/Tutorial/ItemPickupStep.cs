using System;
using UnityEngine;

namespace Tutorial.Steps
{
    [Serializable]
    public class ItemPickupStep : TutorialStep
    {
        [SerializeField] private int _itemIndex;
        [SerializeField] private bool _moveCamera;

        private ItemComponent item => gameData.activeRoom.items[_itemIndex];

        public override void Enter()
        {
            if (_moveCamera)
            {
                MoveCameraToPoint(item.transform, ActivePointer);
            }
            else
            {
                ActivePointer();
            }
        }

        private void ActivePointer()
        {
            SetPointerTarget(item);
        }

        public override void Exit()
        {
            SetPointerTarget(null);
        }

        public override void Update()
        {
            if (gameData.player.activeItem != null)
            {
                Complete();
            }
        }
    }
}
