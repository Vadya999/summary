using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Steps
{
    [Serializable]
    public class SetDoorsState : TutorialStep
    {
        [SerializeField] private List<DoorStatePair> _states;

        public override void Enter()
        {
            foreach (var pairs in _states)
            {
                SetDoorActive(gameData.activeRoom.doors[pairs.doorIndex], pairs.state);
            }
            Complete();
        }

        private void SetDoorActive(DoorComponent door, bool state)
        {
            door.enterLock = !state;
            door.otherDoor.enterLock = !state;
        }

        [Serializable]
        public class DoorStatePair
        {
            public int doorIndex;
            public bool state;
        }
    }
}
