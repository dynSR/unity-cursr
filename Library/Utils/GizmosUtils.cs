using UnityEngine;

namespace UnityTools.Library.Utils {
    public static class GizmosUtils {
        public static void DrawColoredWireCube(Vector3 position, Vector3 scale, Color cellColor) {
            Gizmos.color = cellColor;
            Gizmos.DrawWireCube(position, scale);
        }

        public static void DrawColoredCube(Vector3 position, Vector3 scale, Color cellColor) {
            Gizmos.color = cellColor;
            Gizmos.DrawCube(position, scale);
        }

        public static void DrawColoredWireSphere(Vector3 position, float radius, Color cellColor) {
            Gizmos.color = cellColor;
            Gizmos.DrawWireSphere(position, radius);
        }

        public static void DrawColoredSphere(Vector3 position, float radius, Color cellColor) {
            Gizmos.color = cellColor;
            Gizmos.DrawSphere(position, radius);
        }
    }
}