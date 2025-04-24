using CursR.Runtime.Enums;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new big cursor", fileName = "_ Big Cursor", order = 2)]
    public sealed class BigCursor : CursorConfig {
        [SerializeField, ShowIf("isAnimated")] private BigCursorAnimation cursorAnimation;

        public override CursorSize GetSize() => CursorSize.Big;
        public override CursorAnimationConfig GetAnimation() => cursorAnimation;
    }
}