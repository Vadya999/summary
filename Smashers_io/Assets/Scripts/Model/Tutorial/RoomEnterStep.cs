using System;
using UnityEngine;

namespace Tutorial.Steps
{
    [Serializable]
    public class RoomEnterStep : TutorialStep
    {
        [SerializeField] private int _roomID;

        private RoomEnterScreen _roomEnterScreen;

        private RoomComponent requiredRoom => gameData.level.rooms[_roomID];

        public override void Enter()
        {
            _roomEnterScreen = GetScreen<RoomEnterScreen>();
            AddUIPointer(_roomEnterScreen.enterButton);
            SetPointerTarget(requiredRoom.enterTrigger);
            SetNotRequiredRoomsState(false);
        }

        public override void Update()
        {
            if (gameData.activeRoom == requiredRoom)
            {
                Complete();
            }
        }

        public override void Exit()
        {
            RemoveUIPointer(_roomEnterScreen.enterButton);
            SetPointerTarget(null);
            SetNotRequiredRoomsState(true);
        }

        private void SetNotRequiredRoomsState(bool state)
        {
            foreach (var room in gameData.level.rooms)
            {
                if (room != requiredRoom)
                {
                    room.enterTrigger.enabled = state;
                }
            }
        }
    }
}
