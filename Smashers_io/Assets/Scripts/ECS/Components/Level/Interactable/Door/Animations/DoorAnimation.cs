using DG.Tweening;
using Kuhpik;
using System;
using System.Collections;
using UnityEngine;

namespace Door.Animations
{
    [Serializable]
    public abstract class DoorAnimation
    {
        public abstract IEnumerator OpenRoutine(DoorComponent door, TargetInfo target);

        protected DoorComponent _door;

        protected void Init(DoorComponent door)
        {
            _door = door;
        }

        protected void SetTargetInpuState(TargetInfo target, bool value)
        {
            if (target.animations == Bootstrap.Instance.GameData.player.aniamtion)
            {
                PlayerInput.isEnabled = value;
            }
            _door.otherDoor.enterLock = !value;
            _door.enterLock = !value;
            target.animations.isManual = !value;
        }

        protected IEnumerator SetUpTarget(TargetInfo target)
        {
            target.root.DOMove(_door.enterPoint, 0.3f);
            yield return target.root.DORotate(Quaternion.LookRotation(_door.transform.forward, Vector3.up).eulerAngles, 0.3f).WaitForCompletion();
        }
    }
}
