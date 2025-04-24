using CursR.Runtime.Enums;
using CursR.Runtime.Interfaces;
using CursR.Runtime.ScriptableObjects;
using CursR.Runtime.ScriptableObjects.Configs.Cursor;
using CursR.Runtime.Services;
using CursR.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityUtils;

namespace CursR.Runtime {
    public class CursorManager : PersistentSingleton<CursorManager> {
        [SerializeField, Title("Settings", bold: true), LabelText("Lock Mode")]
        private CursorLockMode cursorLockMode = CursorLockMode.Confined;

        [SerializeField, LabelText("Visibility")]
        private CursorVisibility cursorVisibility = CursorVisibility.Visible;

        [SerializeField, LabelText("Size"), EnumToggleButtons]
        private CursorSize cursorSize = CursorSize.Small;

        [SerializeField, LabelText("Library")] private CursorLibrary cursorLibrary;

        private CursorState cursorState = CursorState.Unclicked;
        private CursorConfig currentCursor;
        private CursorAnimator cursorAnimator;

        protected override void Awake() {
            base.Awake();
            Init();
        }

        private void OnEnable() {
            GetCursorAnimatorInstance().OnEnable();
            IHoverable.OnHover += SetCurrentCursor;
        }

        private void OnDisable() {
            GetCursorAnimatorInstance().OnDisable();
            IHoverable.OnHover -= SetCurrentCursor;
        }

        private void Update() => HandleCursorClick();

        private void LateUpdate() {
            if (!CanCurrentCursorBeAnimated()) return;
            GetCursorAnimatorInstance().Update();
        }

        private void Init() {
            SetDefaultCursor();
            SetCursorVisibility(cursorVisibility);
            SetCursorLockMode(cursorLockMode);
        }

        private void HandleCursorClick() {
            if (Mouse.current.leftButton.wasPressedThisFrame) {
                if (cursorState == CursorState.Clicked) return;
                cursorState = CursorState.Clicked;
                CursorUtils.SetCursorAppearance(
                    GetCurrentCursorIconBasedOnCursorState(),
                    currentCursor.IsCentered
                );
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame) {
                if (cursorState == CursorState.Unclicked) return;
                cursorState = CursorState.Unclicked;
                CursorUtils.SetCursorAppearance(
                    GetCurrentCursorIconBasedOnCursorState(),
                    currentCursor.IsCentered
                );
            }
        }

        private void SetDefaultCursor() => SetCurrentCursor(CursorType.Default);

        private void SetCurrentCursor(CursorType type) {
            if (IsCursorAlreadySet(type)) return;

            currentCursor = cursorLibrary.GetCursorByTypeAndSize(type, cursorSize);

            CursorUtils.SetCursorAppearance(
                GetCurrentCursorIconBasedOnCursorState(),
                currentCursor.IsCentered
            );

            if (currentCursor.IsAnimated()) GetCursorAnimatorInstance().Init(currentCursor);
            else GetCursorAnimatorInstance().Reset();
        }

        private bool IsCursorAlreadySet(CursorType type) => currentCursor != null && currentCursor.Type == type;

        private bool CanCurrentCursorBeAnimated() => cursorState == CursorState.Unclicked && currentCursor &&
                                                     currentCursor.IsAnimated();

        private Texture2D GetCurrentCursorIconBasedOnCursorState() {
            Texture2D icon = cursorState == CursorState.Unclicked
                ? currentCursor.DefaultIcon
                : currentCursor.ClickedIcon;
            Assert.IsNotNull(icon, "Cursor icon cannot be null");
            return icon;
        }

        private CursorAnimator GetCursorAnimatorInstance() {
            if (cursorAnimator != null) return cursorAnimator;
            cursorAnimator = new CursorAnimator();
            Assert.IsNotNull(currentCursor, "Current cursor must be defined");
            return cursorAnimator;
        }

        public void ShowCursor() {
            cursorVisibility = CursorVisibility.Visible;
            SetCursorVisibility(cursorVisibility);
        }

        public void HideCursor() {
            cursorVisibility = CursorVisibility.Hidden;
            SetCursorVisibility(cursorVisibility);
        }

        private void SetCursorVisibility(CursorVisibility visibility) => CursorUtils.SetCursorVisibility(visibility);

        public void LockCursor() => CursorUtils.SetCursorLockMode(CursorLockMode.Locked);
        public void ConfineCursor() => CursorUtils.SetCursorLockMode(CursorLockMode.Confined);
        public void FreeCursor() => CursorUtils.SetCursorLockMode(CursorLockMode.None);
        private void SetCursorLockMode(CursorLockMode lockMode) => CursorUtils.SetCursorLockMode(lockMode);

        private void SetCursorSize(CursorSize size) {
            if (currentCursor == null) return;
            cursorSize = size;
            SetCurrentCursor(currentCursor.Type);
        }

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() {
            SetCursorLockMode(cursorLockMode);
            SetCursorVisibility(cursorVisibility);
            SetCursorSize(cursorSize);
        }

        [Button(ButtonSizes.Medium), Title("Editor actions")]
        private void SetCursorToDefault() => SetDefaultCursor();

        [Button(ButtonSizes.Medium)]
        private void SetCursorToHover() => SetCurrentCursor(CursorType.Hover);
#endif

        #endregion
    }
}