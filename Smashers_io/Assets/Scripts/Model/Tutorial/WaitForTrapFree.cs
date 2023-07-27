using System;
using System.Linq;

namespace Tutorial.Steps
{
    [Serializable]
    public class WaitForTrapFree : TutorialStep
    {
        private ItemComponent item => gameData.player.activeItem;
        private TrapComponent trap => gameData.activeRoom.traps.First(x => x.requiredItem == item);

        private INeighborPathNode currentNode => gameData.activeRoom.neighbors.First().currentNode;
        private bool canPrepareTrap => !(currentNode.trap == trap && currentNode.inUse);

        public override void Update()
        {
            if (canPrepareTrap)
            {
                Complete();
            }
        }
    }
}
