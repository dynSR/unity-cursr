using UnityEditor;
using UnityEngine;

namespace UnityTools.Library.Utils {
    public static class PrevisualizationUtils {
        public static void DrawColoredWireCube(Vector3 position, Vector3 scale, Color color) {
            Gizmos.color = color;
            Gizmos.DrawWireCube(position, scale);
        }

        public static void DrawColoredCube(Vector3 position, Vector3 scale, Color color) {
            Gizmos.color = color;
            Gizmos.DrawCube(position, scale);
        }

        public static void DrawColoredWireSphere(Vector3 position, float radius, Color color) {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(position, radius);
        }

        public static void DrawColoredSphere(Vector3 position, float radius, Color color) {
            Gizmos.color = color;
            Gizmos.DrawSphere(position, radius);
        }

        public static void DrawText(Vector3 position, string text) {
            Handles.Label(position, text);
        }
    }
}