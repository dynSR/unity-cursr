using CursR.Runtime.Enums;
using CursR.Runtime.Helpers;
using CursR.Runtime.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityUtils;
using Cursor = CursR.Runtime.ScriptableObjects.Cursor;

namespace CursR.Runtime {
    public class CursorManager : PersistentSingleton<CursorManager> {
        [SerializeField, Title("Settings", bold: true)]
        private CursorLockMode cursorLockMode = CursorLockMode.Confined;

        [SerializeField] private CursorVisibility cursorVisibility = CursorVisibility.Visible;

        [SerializeField] private Cursors allCursors;
        private Cursor currentCursor;
        private CursorAnimator cursorAnimator;

        protected override void Awake() {
            base.Awake();
            Init();
        }

        private void OnEnable() => GetCursorAnimator().OnEnable();
        private void OnDisable() => GetCursorAnimator().OnDisable();

        private void Init() {
            SetDefaultCursor();
            SetCursorVisibility(cursorVisibility);
            SetCursorLockMode(cursorLockMode);
        }

        private void Update() {
            if (!currentCursor.IsAnimated()) return;
            GetCursorAnimator().Update();
        }

        private void SetDefaultCursor() => SetCurrentCursor(CursorType.Default);

        private void SetCurrentCursor(CursorType cursorType) {
            currentCursor = allCursors.GetCursorByType(cursorType);
            CursorService.SetCursorAppearance(
                currentCursor.GetIcon(),
                currentCursor.GetHotSpot()
            );

            GetCursorAnimator().Init(
                currentCursor.GetAnimation().GetAnimationFrames().ToArray(),
                currentCursor.GetAnimation().GetAnimationSpeed(),
                currentCursor.GetHotSpot()
            );
        }

        public void ShowCursor() {
            cursorVisibility = CursorVisibility.Visible;
            SetCursorVisibility(cursorVisibility);
        }

        public void HideCursor() {
            cursorVisibility = CursorVisibility.Hidden;
            SetCursorVisibility(cursorVisibility);
        }

        private void SetCursorVisibility(CursorVisibility visibility) => CursorService.SetCursorVisibility(visibility);

        public void LockCursor() => SetCursorLockMode(CursorLockMode.Locked);
        public void ConfineCursor() => SetCursorLockMode(CursorLockMode.Confined);
        public void FreeCursor() => SetCursorLockMode(CursorLockMode.None);
        private void SetCursorLockMode(CursorLockMode lockMode) => CursorService.SetCursorLockMode(lockMode);

        private CursorAnimator GetCursorAnimator() {
            if (cursorAnimator != null) return cursorAnimator;
            cursorAnimator = new CursorAnimator();
            Assert.IsNotNull(currentCursor);
            return cursorAnimator;
        }

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