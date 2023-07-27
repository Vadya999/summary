using System;
using System.Collections;

namespace Traps.Actions
{
    [Serializable]
    public abstract class TrapAction
    {
        public NeighborComponent activeNeighbor { get; private set; }

        public void SetNeighbor(NeighborComponent neighbor)
        {
            activeNeighbor = neighbor;
        }

        public abstract IEnumerator UseRoutine();
    }

}
