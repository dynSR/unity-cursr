using CursR.Runtime.Enums;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new medium cursor", fileName = "_ Medium Cursor", order = 1)]
    public sealed class MediumCursor : CursorConfig {
        [SerializeField, ShowIf("isAnimated")] private MediumCursorAnimation cursorAnimation;

        public override CursorSize GetSize() => CursorSize.Medium;
        public override CursorAnimationConfig GetAnimation() => cursorAnimation;
    }
}