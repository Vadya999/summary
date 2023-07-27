using System;
using UnityEngine;

namespace Tutorial.Steps
{
    [Serializable]
    public class SetNeighborState : TutorialStep
    {
        [SerializeField] private bool _canMove;

        public override void Enter()
        {
            foreach (var neighbor in gameData.activeRoom.neighbors)
            {
                neighbor.nodeLock = !_canMove;
                neighbor.animation.ForceWalking(false);
            }
            Complete();
        }
    }
}
