using UnityEngine;
using UnityEngine.Assertions;

namespace GridSystem.Runtime.Utils {
    public static class MouseRaycasterUtils {
        private static Vector3 lastRaycastHitPos = Vector3.zero;

        public static Vector3 GetRaycastHitPointFromMousePosition(Camera sceneCamera, float maxDistance,
            LayerMask layerMask) {
            Assert.IsNotNull(sceneCamera, "The scene main camera could not be found, please define one");

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask)) lastRaycastHitPos = hit.point;
            return lastRaycastHitPos;
        }

        public static GameObject HitObject(Camera sceneCamera, float maxDistance, LayerMask layerMask) {
            Assert.IsNotNull(sceneCamera, "The scene main camera could not be found, please define one");

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);

            return Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask) ? hit.collider.gameObject : null;
        }
    }
}