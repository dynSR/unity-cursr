using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityTools.Systems.CursR.Runtime.Enums;
using UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.Cursor;
using UnityTools.Systems.CursR.Runtime.Utils;
using UnityTools.Library.Extensions;
using UnityEngine;

namespace UnityTools.Systems.CursR.Runtime.Services {
    public class MouseButtonsProcessor {
        private CursorConfig cursorConfig;
        private readonly List<ButtonControl> mouseButtons = new();

        public void SetCursorConfig(CursorConfig config) => cursorConfig = config;

        public void HandleMouseButtonsState(ref CursorState state) {
            foreach (ButtonControl button in GetButtonControls()) {
                HandleMouseButtonClick(button, ref state);
                HandleMouseButtonRelease(button, ref state);
            }
        }

        private ButtonControl[] GetButtonControls() {
            if (mouseButtons.IsEmpty()) {
                mouseButtons.AddUnique(Mouse.current.leftButton);
            }

            return mouseButtons.ToArray();
        }

        private void HandleMouseButtonClick(ButtonControl button, ref CursorState state) {
            if (state == CursorState.Clicked || !button.wasPressedThisFrame) return;
            state = CursorState.Clicked;
            CursorUtils.SetCursorAppearance(
                cursorConfig.ClickIcon,
                cursorConfig.IsCentered
            );
        }

        private void HandleMouseButtonRelease(ButtonControl button, ref CursorState state) {
            if (state == CursorState.Unclicked || !button.wasReleasedThisFrame) return;
            state = CursorState.Unclicked;
            CursorUtils.SetCursorAppearance(
                cursorConfig.DefaultIcon,
                cursorConfig.IsCentered
            );
        }
    }
}