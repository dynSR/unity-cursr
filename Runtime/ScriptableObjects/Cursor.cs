using CursR.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Cursor/Icon", fileName = "New cursor icon")]
    public class Cursor : ScriptableObject {
        [field: SerializeField, Title("Cursor settings", bold: true)]
        public CursorType Type { get; set; }

        [field: SerializeField] public bool IsCentered { get; private set; } = false;


        [field: SerializeField, PreviewField(height: 64), Required]
        private Texture2D Icon { get; set; }

        [field: SerializeField, Title("Cursor animation settings", bold: true)]
        private bool isAnimated = true;

        [field: SerializeField, Required] private CursorAnimation Animation { get; set; }

        public Texture2D GetIcon() => Icon;
        public CursorAnimation GetAnimation() => Animation;
        public bool IsAnimated() => isAnimated && Animation.GetAnimationFrames().Count >= 2;

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() {
            if (Animation == null) Debug.LogError("Cursor animation must be defined, at least the default one");
        }
#endif

        #endregion
    }
}