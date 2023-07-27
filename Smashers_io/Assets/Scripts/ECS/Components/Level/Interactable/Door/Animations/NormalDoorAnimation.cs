using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

namespace Door.Animations
{
    [Serializable]
    public class NormalDoorAnimation : DoorAnimation
    {
        public override IEnumerator OpenRoutine(DoorComponent door, TargetInfo target)
        {
            Init(door);

            SetTargetInpuState(target, false);
            yield return SetUpTarget(target);
            target.animations.ShowDoorOpen();
            _door.animations.Open();
            yield return new WaitForSeconds(0.4f);
            _door.otherDoor.animations.Open();
            yield return target.root.DoMoveNormalized(_door.exitPoint, 3).WaitForCompletion();
            _door.animations.Close();
            _door.otherDoor.animations.Close();
            SetTargetInpuState(target, true);
        }
    }
}
