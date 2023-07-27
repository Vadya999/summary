using System;
using System.Collections;
using UnityEngine;

namespace Traps.Actions
{
    [Serializable]
    public class MicrowaveTrapAction : TrapAction
    {
        [SerializeField] private GameObject _expodedRoot;

        public override IEnumerator UseRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            _expodedRoot.gameObject.SetActive(true);
            activeNeighbor.ShowAngry();
            yield return new WaitForSeconds(0.25f);
            activeNeighbor.animation.ShowTrapUse(TrapUseID.Angry);
            yield return new WaitForSeconds(5f);
        }
    }
}
