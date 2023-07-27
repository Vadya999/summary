using NaughtyAttributes;
using System;
using System.Linq;
using UnityEngine;

namespace Tutorial.Steps
{
    [Serializable]
    public class TrapPrepareStep : TutorialStep
    {
        [SerializeField] private bool _moveCamera;

        [SerializeField] private bool _canFail;
        [SerializeField, ShowIf(nameof(_canFail))] private int _stepIDOffsetOnFailed;

        private ItemComponent item => gameData.player.activeItem;
        private TrapComponent trap => gameData.activeRoom.traps.First(x => x.requiredItem == item);

        private INeighborPathNode currentNode => gameData.activeRoom.neighbors.First().currentNode;
        private bool canPrepareTrap => !(currentNode.trap == trap && currentNode.inUse);

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
            SetPointerTarget(trap);
        }

        public override void Update()
        {
            if (!canPrepareTrap && _canFail)
            {
                ForceStepID(_stepIDOffsetOnFailed, true);
            }
            if (gameData.player.activeItem == null)
            {
                Complete();
            }
        }

        public override void Exit()
        {
            SetPointerTarget(null);
        }
    }
}
