using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

namespace Traps.Actions
{

    [Serializable]
    public class GenericTrapAction : TrapAction
    {
        [SerializeField] private float _duraiton;
        [SerializeField] private TrapUseID _trapID;

        [SerializeField] private bool _moveToStarPoint;
        [SerializeField, ShowIf(nameof(_moveToStarPoint))] private Transform _startPoint;

        [SerializeField] private bool _applyRootMotion = true;

        public override IEnumerator UseRoutine()
        {
            if (_moveToStarPoint)
            {
                yield return activeNeighbor.transform
                    .DoTransform(_startPoint, 0.25f)
                    .WaitForCompletion();
            }
            if (_applyRootMotion) activeNeighbor.animation.BeginRoomMotion();
            activeNeighbor.animation.ShowTrapUse(_trapID);
            yield return new WaitForSeconds(_duraiton);
            if (_applyRootMotion) activeNeighbor.animation.StopRootMotion(activeNeighbor.gameObject);
        }
    }
}
