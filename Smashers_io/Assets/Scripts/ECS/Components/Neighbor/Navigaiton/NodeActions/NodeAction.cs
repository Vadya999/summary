using System;
using System.Collections;

namespace Navigation.Nodes.Actions
{
    [Serializable]
    public abstract class NodeAction
    {
        public abstract IEnumerator OnEnterPoint(NeighborComponent neighbor);
    }
}
