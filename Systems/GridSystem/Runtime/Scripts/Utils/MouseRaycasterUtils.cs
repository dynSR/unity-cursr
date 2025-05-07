using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace GridSystem.Runtime.Utils {
    public static class MouseRaycasterUtils {
        private static Vector3 lastRaycastHitPos = Vector3.zero;

        public static Vector3 GetRaycastHitPointFromMousePosition(Camera camera, float maxDistance,
            LayerMask layerMask) {
            Assert.IsNotNull(camera, "The scene camera could not be found, please define one");

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = camera.nearClipPlane;
            Ray ray = camera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask)) lastRaycastHitPos = hit.point;
            return lastRaycastHitPos;
        }

        public static GameObject HitObject(Camera camera, float maxDistance, LayerMask layerMask) {
            Assert.IsNotNull(camera, "The scene camera could not be found, please define one");

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = camera.nearClipPlane;
            Ray ray = camera.ScreenPointToRay(mousePos);

            return Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask)
                ? hit.collider.gameObject
                : null;
        }
    }
}