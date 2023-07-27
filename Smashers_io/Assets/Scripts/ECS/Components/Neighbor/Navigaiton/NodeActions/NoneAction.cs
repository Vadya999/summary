using System;
using System.Collections;

namespace Navigation.Nodes.Actions
{
    [Serializable]
    public class NoneAction : NodeAction
    {
        public override IEnumerator OnEnterPoint(NeighborComponent neighbor)
        {
            yield return null;
        }
    }
}
