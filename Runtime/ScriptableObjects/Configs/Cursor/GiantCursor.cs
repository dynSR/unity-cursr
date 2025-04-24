using CursR.Runtime.Enums;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new giant cursor", fileName = "_ Giant Cursor", order = 3)]
    public sealed class GiantCursor : CursorConfig {
        [SerializeField, ShowIf("isAnimated")] private GiantCursorAnimation cursorAnimation;

        public override CursorSize GetSize() => CursorSize.Giant;
        public override CursorAnimationConfig GetAnimation() => cursorAnimation;
    }
}