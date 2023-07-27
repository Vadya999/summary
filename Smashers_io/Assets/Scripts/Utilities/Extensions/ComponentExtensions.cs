using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UnityTools.Extentions
{
    public static class ComponentExtensions
    {
        public static Bounds GetBounds(this GameObject obj)
        {
            var result = new Bounds();
            var renderers = obj.GetComponentsOnObject<Renderer>();
            foreach (var renderer in renderers)
            {
                result.Encapsulate(renderer.bounds);
            }
            return result;
        }

        public static Bounds GetBoudnds(this Component component)
        {
            return GetBounds(component.gameObject);
        }

        public static void SetButtonState(this Button button, bool state)
        {
            button.enabled = state;
            button.image.color = state ? Color.white : Color.gray;
        }

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
            return GetComponentOnObject<T>(component.gameObject);
        }

        public static T GetComponentOnObject<T>(this GameObject obj)
        {
            if (obj.TryGetComponent(out T result))
            {
                result = obj.GetComponentInChildren<T>();
            }
            return result;
        }

        public static IEnumerable<T> GetComponentsOnObject<T>(this MonoBehaviour component)
        {
            return GetComponentsOnObject<T>(component.gameObject);
        }

        public static IEnumerable<T> GetComponentsOnObject<T>(this GameObject obj)
        {
            var result = new List<T>();
            if (obj.TryGetComponent<T>(out var selfComponent))
            {
                result.Add(selfComponent);
            }
            return result.Concat(obj.GetComponentsInChildren<T>());
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