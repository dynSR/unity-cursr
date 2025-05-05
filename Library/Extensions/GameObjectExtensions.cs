using UnityEngine;

namespace UnityTools.Library.Extensions {
    public static class GameObjectExtensions {
        public static void SetPosition(this GameObject go, Vector3 position) {
            if (go.transform.position == position) return;
            go.transform.position = position;
        }

        public static void SetLocalPosition(this GameObject go, Vector3 position) {
            if (go.transform.localPosition == position) return;
            go.transform.localPosition = position;
        }

        public static void SetScale(this GameObject go, Vector3 scale) {
            if (go.transform.localScale == scale) return;
            go.transform.localScale = scale;
        }

        public static bool HasChildren(this GameObject go) => go.transform.HasChildren();

        public static void SetLocalRotation(this GameObject go, Vector3 rotation) {
            if (go.transform.localEulerAngles == rotation) return;
            go.transform.localEulerAngles = rotation;
        }

        public static void SetParent(this GameObject go, Transform parent) {
            go.transform.SetParent(parent);
        }
    }
}