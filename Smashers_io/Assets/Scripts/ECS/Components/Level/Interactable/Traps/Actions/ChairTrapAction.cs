using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

namespace Traps.Actions
{
    [Serializable]
    public class ChairTrapAction : TrapAction
    {
        [SerializeField] private Transform _sitPoint;
        [SerializeField] private ChairAnimationComponent _chairAnimation;

        public override IEnumerator UseRoutine()
        {
            activeNeighbor.animation.isManual = true;
            yield return activeNeighbor.transform.DoTransform(_sitPoint, 0.25f).WaitForCompletion();
            activeNeighbor.animation.ShowSit();
            yield return new WaitForSeconds(0.5f);
            _chairAnimation.ShowBroken();
            activeNeighbor.ShowAngry();
            activeNeighbor.animation.ShowTrapUse(TrapUseID.Chair);
            yield return new WaitForSeconds(8);
            _chairAnimation.Fix();
            activeNeighbor.animation.isManual = false;
        }
    }
}
