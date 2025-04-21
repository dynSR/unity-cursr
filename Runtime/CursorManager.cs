using CursR.Runtime.Enums;
using CursR.Runtime.Interfaces;
using CursR.Runtime.ScriptableObjects;
using CursR.Runtime.Services;
using CursR.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityUtils;

namespace CursR.Runtime {
    public class CursorManager : PersistentSingleton<CursorManager> {
        [SerializeField, Title("Settings", bold: true)]
        private CursorLockMode cursorLockMode = CursorLockMode.Confined;

        [SerializeField] private CursorVisibility cursorVisibility = CursorVisibility.Visible;
        [SerializeField, EnumToggleButtons] private CursorSize cursorSize = CursorSize.Small;
        [SerializeField] private CursorLibrary cursorLibrary;

        private CursorState cursorState = CursorState.Unclicked;
        private CursorConfig currentCursorConfig;
        private CursorAnimator cursorAnimator;

        protected override void Awake() {
            base.Awake();
            Init();
        }

        private void OnEnable() {
            GetCursorAnimator().OnEnable();
            IHoverable.OnHover += SetCurrentCursor;
        }

        private void OnDisable() {
            GetCursorAnimator().OnDisable();
            IHoverable.OnHover -= SetCurrentCursor;
        }

        private void Update() => HandleCursorClick();

        private void LateUpdate() {
            if (!CanCurrentCursorBeAnimated()) return;
            GetCursorAnimator().Update();
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
                    GetCurrentCursorIcon(cursorSize),
                    currentCursorConfig.IsCentered
                );
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame) {
                if (cursorState == CursorState.Unclicked) return;
                cursorState = CursorState.Unclicked;
                CursorUtils.SetCursorAppearance(
                    GetCurrentCursorIcon(cursorSize),
                    currentCursorConfig.IsCentered
                );
            }
        }

        private void SetDefaultCursor() => SetCurrentCursor(CursorType.Default);

        private void SetCurrentCursor(CursorType type) {
            if (IsCursorAlreadySet(type)) return;

            currentCursorConfig = cursorLibrary.GetCursorByType(type);

            CursorUtils.SetCursorAppearance(
                GetCurrentCursorIcon(cursorSize),
                currentCursorConfig.IsCentered
            );
            GetCursorAnimator().Init(currentCursorConfig);
        }

        private bool IsCursorAlreadySet(CursorType type) => currentCursorConfig && currentCursorConfig.Type == type;

        private bool CanCurrentCursorBeAnimated() =>
            currentCursorConfig && currentCursorConfig.IsAnimated(cursorSize) && cursorState == CursorState.Unclicked;

        private Texture2D GetCurrentCursorIcon(CursorSize size) {
            Texture2D icon = cursorState == CursorState.Unclicked
                ? currentCursorConfig.GetIconByCursorSize(size)
                : currentCursorConfig.GetClickedIconByCursorSize(size);
            Assert.IsNotNull(icon, "Cursor icon cannot be null");
            return icon;
        }

        private CursorAnimator GetCursorAnimator() {
            if (cursorAnimator != null) return cursorAnimator;
            cursorAnimator = new CursorAnimator();
            Assert.IsNotNull(currentCursorConfig);
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

        public void LockCursor() => SetCursorLockMode(CursorLockMode.Locked);
        public void ConfineCursor() => SetCursorLockMode(CursorLockMode.Confined);
        public void FreeCursor() => SetCursorLockMode(CursorLockMode.None);
        private void SetCursorLockMode(CursorLockMode lockMode) => CursorUtils.SetCursorLockMode(lockMode);

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() {
            SetCursorLockMode(cursorLockMode);
            SetCursorVisibility(cursorVisibility);
        }

        [Button(ButtonSizes.Medium), Title("Editor actions")]
        private void SetCursorToDefault() => SetDefaultCursor();

        [Button(ButtonSizes.Medium)]
        private void SetCursorToHover() => SetCurrentCursor(CursorType.Hover);
#endif

        #endregion
    }
}