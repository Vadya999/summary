using System;
using System.Collections;
using UnityEngine;

namespace Traps.Actions
{
    [Serializable]
    public class CarpetTrapAciton : TrapAction
    {
        [SerializeField] private Animator _carpetAnimator;

        public override IEnumerator UseRoutine()
        {
            SetUpRotation();
            activeNeighbor.animation.BeginRoomMotion();
            activeNeighbor.animation.ShowTrapUse(TrapUseID.FallFront);
            _carpetAnimator.SetTrigger("Use");
            yield return new WaitForSeconds(6.5f);
            activeNeighbor.animation.StopRootMotion(activeNeighbor.gameObject);
        }

        private void SetUpRotation()
        {
            var currentRotation = _carpetAnimator.transform.rotation;
            var toTargetDirection = activeNeighbor.transform.position - _carpetAnimator.transform.position;
            var toTargetRotation = Quaternion.LookRotation(toTargetDirection, Vector3.up);
            var delta = (int)currentRotation.y - (int)toTargetRotation.y;
            _carpetAnimator.transform.Rotate(0, -delta * 90, 0);

        }
    }
}
