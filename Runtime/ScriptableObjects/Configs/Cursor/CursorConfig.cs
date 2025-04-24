using CursR.Runtime.Enums;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.Cursor {
    public abstract class CursorConfig : ScriptableObject {
        public abstract CursorSize GetSize();
        public abstract CursorAnimationConfig GetAnimation();

        [field: SerializeField, Title("Settings", bold: true), ToggleLeft]
        public bool IsCentered { get; private set; }

        [field: SerializeField] public CursorType Type { get; set; }

        [field: SerializeField, Required] public Texture2D DefaultIcon { get; set; }

        [field: SerializeField, Required] public Texture2D ClickedIcon { get; set; }

        [field: SerializeField, Title("Cursor animation", bold: true), ToggleLeft]
        protected bool isAnimated = true;

        public bool IsAnimated() => isAnimated && GetAnimation().Frames.Count >= 2;
    }
}