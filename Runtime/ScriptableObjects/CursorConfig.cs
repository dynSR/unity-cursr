using CursR.Runtime.Enums;
using CursR.Runtime.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Cursor/New", fileName = "New cursor")]
    public class CursorConfig : ScriptableObject {
        [field: SerializeField, Title("Cursor settings", bold: true)]
        public CursorType Type { get; set; }

        [field: SerializeField] public bool IsCentered { get; private set; }

        [field: SerializeField, Title("Cursor animation settings", bold: true)]
        private bool isAnimated = true;

        [field: SerializeField, Required] public CursorAnimationConfig AnimationConfig { get; private set; }

        [field: SerializeField, Title("Cursor icons", bold: true), Required]
        private CursorProperties<CursorIcon, Texture2D> Properties { get; set; }

        [field: SerializeField, Required, Title("Clicked cursor icons", bold: true)]
        private CursorProperties<CursorIcon, Texture2D> ClickedProperties { get; set; }

        public Texture2D GetIconByCursorSize(CursorSize size) => Properties.GetPropertyByCursorSize(size);
        public Texture2D GetClickedIconByCursorSize(CursorSize size) => ClickedProperties.GetPropertyByCursorSize(size);

        public bool IsAnimated(CursorSize size) =>
            isAnimated && AnimationConfig.GetAnimationFramesByCursorSize(size).Count >= 2;

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() {
            if (AnimationConfig == null)
                Debug.LogError("Cursor animation must be defined, at least the default one", this);
        }
#endif

        #endregion
    }
}