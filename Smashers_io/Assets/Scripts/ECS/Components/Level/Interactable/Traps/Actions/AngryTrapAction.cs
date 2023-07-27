using System;
using System.Collections;
using UnityEngine;

namespace Traps.Actions
{
    [Serializable]
    public class AngryTrapAction : TrapAction
    {
        public override IEnumerator UseRoutine()
        {
            activeNeighbor.animation.ShowTrapUse(TrapUseID.Angry);
            yield return new WaitForSeconds(7.5f);
        }
    }
}
