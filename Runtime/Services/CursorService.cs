using UnityEngine;
using CursR.Runtime.Enums;
using UnityEngine.Assertions;

namespace CursR.Runtime.Services {
    public static class CursorService {
        public const float CursorAnimationSpeedMinValue = 0.15f;

        public static void SetCursorAppearance(Texture2D texture2D, Vector2 hotSpot) {
            Assert.IsNotNull(texture2D, "No texture2D defined, impossible to assign it");
            Cursor.SetCursor(texture2D, hotSpot, CursorMode.ForceSoftware);
        }

        public static void SetCursorVisibility(CursorVisibility visibility) {
            if (visibility == CursorVisibility.Visible) {
                Cursor.visible = true;
                return;
            }

            Cursor.visible = false;
        }

        public static void SetCursorLockMode(CursorLockMode lockMode) {
            Cursor.lockState = lockMode;
        }
    }
}