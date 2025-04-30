using UnityEngine;

namespace UnityTools.Extensions {
    public static class TransformExtensions {
        public static bool HasChildren(this Transform trs) => trs.childCount >= 1;
    }
}