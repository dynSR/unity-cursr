using CursorSystem.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace CursorSystem.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Game/Cursor System/Configs/Cursor/Create new small cursor", fileName = "_ Small Cursor",
        order = 0)]
    public sealed class SmallCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private SmallCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Small;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}