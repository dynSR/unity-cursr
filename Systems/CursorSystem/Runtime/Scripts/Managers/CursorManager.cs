using CursorSystem.Runtime.Enums;
using CursorSystem.Runtime.ScriptableObjects;
using CursorSystem.Runtime.Services;
using CursorSystem.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityTools.Library.Interfaces;
using UnityUtils;

namespace CursorSystem.Runtime.Managers {
    public class CursorManager : PersistentSingleton<CursorManager> {
        [SerializeField, Title("Settings", bold: true), LabelText("Lock Mode")]
        private CursorLockMode cursorLockMode = CursorLockMode.Confined;

        [SerializeField, LabelText("Visibility")]
        private CursorVisibility cursorVisibility = CursorVisibility.Visible;

        [SerializeField, LabelText("Size"), EnumToggleButtons]
        private CursorSize cursorSize = CursorSize.Small;

        [SerializeField, LabelText("Library")] private CursorLibrary cursorLibrary;

        private CursorState cursorState = CursorState.Unclicked;
        private CursorConfig cursorConfig;
        private readonly CursorAnimator cursorAnimator = new();
        private readonly MouseButtonsProcessor mouseButtonsProcessor = new();

        protected override void Awake() {
            base.Awake();
            Init();
        }

        private void OnEnable() {
            cursorAnimator.OnEnable();
            IHoverable.OnHover += SetCurrentCursor;
        }

        private void OnDisable() {
            cursorAnimator.OnDisable();
            IHoverable.OnHover -= SetCurrentCursor;
        }

        private void Update() => mouseButtonsProcessor.HandleMouseButtonsState(ref cursorState);

        private void LateUpdate() {
            if (!CanCurrentCursorBeAnimated()) return;
            cursorAnimator.Update();
        }

        private void Init() {
            SetDefaultCursor();
            SetCursorVisibility(cursorVisibility);
            SetCursorLockMode(cursorLockMode);
        }

        private void SetDefaultCursor() => SetCurrentCursor(CursorType.Default);

        private void SetCurrentCursor(CursorType type, CursorVisibility visibility, CursorLockMode lockMode) {
            SetCursorLockMode(lockMode);
            SetCursorVisibility(visibility);
            SetCurrentCursor(type);
        }

        private void SetCurrentCursor(CursorType type, CursorLockMode lockMode) {
            SetCursorLockMode(lockMode);
            SetCurrentCursor(type);
        }

        private void SetCurrentCursor(CursorType type, CursorVisibility visibility) {
            SetCursorVisibility(visibility);
            SetCurrentCursor(type);
        }

        private void SetCurrentCursor(CursorType type) {
            if (IsCursorAlreadySet(type)) return;

            cursorConfig = cursorLibrary.GetCursorByTypeAndSize(type, cursorSize);
            CursorUtils.SetCursorAppearance(
                GetCursorIconBasedOnCursorState(),
                cursorConfig.IsCentered
            );

            mouseButtonsProcessor.SetCursorConfig(cursorConfig);
            if (cursorConfig.IsAnimated()) cursorAnimator.Init(cursorConfig);
            else cursorAnimator.Reset();
        }

        private bool IsCursorAlreadySet(CursorType type) => cursorConfig is not null && cursorConfig.Type == type;

        private bool CanCurrentCursorBeAnimated() => cursorState == CursorState.Unclicked
                                                     && cursorConfig is not null
                                                     && cursorConfig.IsAnimated();

        private Texture2D GetCursorIconBasedOnCursorState() {
            Texture2D icon = cursorState == CursorState.Unclicked
                ? cursorConfig.DefaultIcon
                : cursorConfig.ClickIcon;
            Assert.IsNotNull(icon, "Cursor icon cannot be null");
            return icon;
        }

        private static void SetCursorVisibility(CursorVisibility visibility) =>
            CursorUtils.SetCursorVisibility(visibility);

        private static void SetCursorLockMode(CursorLockMode lockMode) => CursorUtils.SetCursorLockMode(lockMode);

        private void SetCursorSize(CursorSize size) {
            if (cursorConfig == null) return;
            cursorSize = size;
            SetCurrentCursor(cursorConfig.Type);
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