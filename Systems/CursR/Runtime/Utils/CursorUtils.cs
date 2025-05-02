using UnityEngine;
using UnityTools.Systems.CursR.Runtime.Enums;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace UnityTools.Systems.CursR.Runtime.Utils {
    public static class CursorUtils {
        public const int CursorAnimationSpeedMinValue = 1;

        public static void SetCursorAppearance(Texture2D texture2D, bool isCentered) {
            Vector2 center = new(texture2D.width * 0.5f, texture2D.height * 0.5f);
            Vector2 hotSpot = isCentered ? center : Vector2.zero;
            SetCursorAppearance(texture2D, hotSpot);
        }

        private static void SetCursorAppearance(Texture2D texture2D, Vector2 hotSpot) {
            Assert.IsNotNull(texture2D, "No texture2D defined, impossible to assign it");
            Cursor.SetCursor(texture2D, hotSpot, CursorMode.ForceSoftware);
        }

        public static void SetCursorVisibility(CursorVisibility visibility) =>
            Cursor.visible = visibility == CursorVisibility.Visible;

        public static void SetCursorLockMode(CursorLockMode lockMode) => Cursor.lockState = lockMode;
        public static bool IsCursorOverUIElement() => EventSystem.current.IsPointerOverGameObject();
    }
}