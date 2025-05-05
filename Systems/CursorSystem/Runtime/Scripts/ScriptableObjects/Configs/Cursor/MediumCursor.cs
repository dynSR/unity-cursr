using CursorSystem.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace CursorSystem.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new medium cursor", fileName = "_ Medium Cursor",
        order = 1)]
    public sealed class MediumCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private MediumCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Medium;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}