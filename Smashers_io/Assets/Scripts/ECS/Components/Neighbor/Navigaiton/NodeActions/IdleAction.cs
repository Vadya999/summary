using System;
using System.Collections;
using UnityEngine;

namespace Navigation.Nodes.Actions
{
    [Serializable]
    public class IdleAction : NodeAction
    {
        [SerializeField] private float _waitDuration;

        public override IEnumerator OnEnterPoint(NeighborComponent neighbor)
        {
            neighbor.idle.SetActive(true);
            neighbor.animation.isWalking = false;
            neighbor.animation.isManual = true;
            yield return new WaitForSeconds(_waitDuration);
            neighbor.animation.isManual = false;
            neighbor.idle.SetActive(false);
        }
    }
}
