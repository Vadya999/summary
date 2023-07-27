using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

namespace Door.Animations
{
    [Serializable]
    public class WallDoorAnimation : DoorAnimation
    {
        public override IEnumerator OpenRoutine(DoorComponent door, TargetInfo target)
        {
            Init(door);

            SetTargetInpuState(target, false);
            yield return SetUpTarget(target);
            yield return EnterFirstDoor(door, target);
            yield return EnterSecondDoor(door, target);
            SetTargetInpuState(target, true);
        }

        private IEnumerator EnterFirstDoor(DoorComponent door, TargetInfo target)
        {
            target.animations.ForceWalking(false);
            target.animations.ShowDoorOpen();
            _door.animations.Open();
            yield return new WaitForSeconds(0.4f);
            target.animations.ForceWalking(true);
            yield return target.root.DoMoveNormalized(GetExitPoint(), 3).WaitForCompletion();
            _door.otherDoor.animations.Open();
            yield return new WaitForSeconds(0.4f);
            _door.animations.Close();
        }

        private IEnumerator EnterSecondDoor(DoorComponent door, TargetInfo target)
        {
            target.root.transform.position = GetEnterPoint();
            target.root.transform.forward = -door.otherDoor.transform.forward;
            target.animations.ForceWalking(true);

            yield return target.root.transform.DoMoveNormalized(_door.exitPoint, 3).WaitForCompletion();
            _door.otherDoor.animations.Close();
        }

        private Vector3 GetExitPoint()
        {
            return _door.animationSpawnPoint.position;
        }

        private Vector3 GetEnterPoint()
        {
            return _door.otherDoor.animationSpawnPoint.position;
        }
    }
}
