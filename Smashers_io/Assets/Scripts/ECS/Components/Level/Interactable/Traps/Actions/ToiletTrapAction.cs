using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

namespace Traps.Actions
{
    [Serializable]
    public class ToiletTrapAction : TrapAction
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _upPoint;
        [SerializeField] private Transform _fallPoint;

        [SerializeField] private GameObject _particles;

        public override IEnumerator UseRoutine()
        {
            yield return activeNeighbor.transform
                .DoTransform(_startPoint, 0.25f)
                .WaitForCompletion();

            activeNeighbor.animation.ShowTrapUse(TrapUseID.Toilet);
            yield return new WaitForSeconds(0.75f);

            _particles.SetActive(true);
            activeNeighbor.ShowAngry();
            yield return activeNeighbor.transform
                .DoTransform(_upPoint, 0.25f)
                .WaitForCompletion();

            yield return new WaitForSeconds(0.25f);
            yield return activeNeighbor.transform
                .DoTransform(_fallPoint, 0.25f)
                .WaitForCompletion();
            _particles.SetActive(false);
            yield return new WaitForSeconds(4);
        }
    }
}
