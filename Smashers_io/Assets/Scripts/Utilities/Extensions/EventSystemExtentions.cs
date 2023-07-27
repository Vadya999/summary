using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityTools.Extentions
{
    public static class EventSystemExtentions
    {
        public static bool TryRaycast<T>(this EventSystem eventSystem, out T component, T expect = null) where T : MonoBehaviour
        {
            var pointerEventData = new PointerEventData(eventSystem) { position = Input.mousePosition };
            var raycastResults = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, raycastResults);
            component = raycastResults
                .Select(x => x.gameObject.GetComponent<T>())
                .FirstOrDefault(x => x != null && x != expect);
            return component != null;
        }
    }
}