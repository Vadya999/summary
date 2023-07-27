using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityTools.Extentions
{
    public static class ComponentExtensions
    {
        public static bool TryGetComponentInParent<T>(this Collision obj, out T result)
        {
            result = obj.gameObject.GetComponentInParent<T>();
            return result != null;
        }

        public static bool TryGetComponentInChildren<T>(this Collision obj, out T result)
        {
            return obj.gameObject.TryGetComponentInChildren(out result);
        }

        public static bool TryGetComponentInChildren<T>(this GameObject obj, out T result)
        {
            result = obj.GetComponentInChildren<T>();
            return result != null;
        }

        public static void Destory(this GameObject obj)
        {
            Object.Destroy(obj);
        }

        public static RectTransform GetRectTransofrm(this Component transform)
        {
            return transform.GetComponent<RectTransform>();
        }

        public static IEnumerable<Transform> GetChildrens(this Transform transform)
        {
            var result = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                result[i] = transform.GetChild(i);
            }
            return result;
        }

        public static void Invoke(this MonoBehaviour monoBehaviour, Action method, float delay)
        {
            monoBehaviour.StartCoroutine(InvokeWithDelay());
            IEnumerator InvokeWithDelay()
            {
                yield return new WaitForSeconds(delay);
                method();
            }
        }

        public static bool TryGetComponent<T>(this RaycastHit hit, out T t)
        {
            t = hit.collider.gameObject.GetComponent<T>();
            return t != null;
        }

        public static T GetComponentOnObject<T>(this MonoBehaviour component)
        {
            var temp = component.GetComponent<T>();
            if (temp == null)
            {
                temp = component.gameObject.GetComponentInChildren<T>();
            }
            return temp;
        }

        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }

        public static bool HasComponent<T>(this Collision collision)
        {
            return collision.gameObject.GetComponent<T>() != null;
        }

        public static bool TryGetComponent<T>(this Collision collision, out T component)
        {
            component = collision.gameObject.GetComponent<T>();
            return component != null;
        }

        public static bool HasComponent<T>(this Collider other)
        {
            return other.gameObject.GetComponent<T>() != null;
        }
    }
}