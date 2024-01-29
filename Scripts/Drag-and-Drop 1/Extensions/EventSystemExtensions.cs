using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Assets.Sripts.UI.Extensions
{
    static class EventSystemExtensions
    {
        public static T GetFirstComponentUnberPointer<T>(this EventSystem system, PointerEventData eventData) where T : class
        {
            List<RaycastResult> raycastResults = new();
            system.RaycastAll(eventData, raycastResults);

            foreach (var raycast in raycastResults)
                if(raycast.gameObject.TryGetComponent<T>(out T component))
                    return component;

            return null;
        }
    }
}