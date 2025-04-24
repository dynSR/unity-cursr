using CursR.Runtime.Enums;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new small cursor", fileName = "_ Small Cursor", order = 0)]
    public sealed class SmallCursor : CursorConfig {
        [SerializeField, ShowIf("isAnimated")] private SmallCursorAnimation cursorAnimation;

        public override CursorSize GetSize() => CursorSize.Small;
        public override CursorAnimationConfig GetAnimation() => cursorAnimation;
    }
}