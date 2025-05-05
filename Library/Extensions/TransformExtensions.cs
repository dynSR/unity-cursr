using UnityEngine;

namespace UnityTools.Library.Extensions {
    public static class TransformExtensions {
        public static bool HasChildren(this Transform trs) => trs.childCount >= 1;

        public static T GetOrAddComponent<T>(this Transform trs) where T : Component {
            return trs.gameObject.TryGetComponent(out T comp) ? comp : trs.gameObject.AddComponent<T>();
        }
    }
}