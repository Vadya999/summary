using DG.Tweening;
using UnityEngine;
using UnityTools.Helpers;

namespace UnityTools.Extentions
{
    public static class TweenExtensions
    {
        public static Tween DoMoveXZ(this Transform transfom, Vector3 target, float duration)
        {
            transfom.DOMoveX(target.x, duration);
            return transfom.DOMoveZ(target.z, duration);
        }

        public static Tweener DoTransform(this Component transform, Vector3 targetPositin, Quaternion targetRotation, float duration)
        {
            transform.transform.DOMove(targetPositin, duration);
            return transform.transform.DORotate(targetRotation.eulerAngles, duration);
        }

        public static Tweener DoTransform(this Transform transform, Transform target, float duration)
        {
            transform.DOMove(target.position, duration);
            return transform.DORotate(target.eulerAngles, duration);
        }

        public static Tweener DoTransform(this Component monoBehaviour, Component target, float duration)
        {
            return monoBehaviour.transform.DoTransform(target.transform, duration);
        }

        public static Tweener DoInsert(this Transform transform, Transform target, float duration)
        {
            transform.DOMove(target.position, duration);
            return transform.DoDisapear(duration);
        }

        public static Tweener DoInsert(this Transform transform, Vector3 target, float duration)
        {
            transform.DOMove(target, duration);
            return transform.DoDisapear(duration);
        }

        public static Tweener DoDisapear(this Transform transform, float duration)
        {
            return transform.DOScale(TweenHelper.zeroSize, duration);
        }

        public static Tweener DoAppears(this Transform transform, float duration)
        {
            var temp = transform.localScale;
            transform.localScale = TweenHelper.zeroSize;
            return transform.DOScale(temp, duration);
        }

        public static Tweener DoPickup(this Transform objectToPickup, Transform point, float duration)
        {
            objectToPickup.transform.parent = point;
            objectToPickup.transform.DOLocalMove(Vector3.zero, duration);
            return objectToPickup.transform.DOLocalRotate(Vector3.zero, 0.5f);
        }
    }
}