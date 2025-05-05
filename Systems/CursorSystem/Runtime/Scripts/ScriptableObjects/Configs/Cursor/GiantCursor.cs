using CursorSystem.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace CursorSystem.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new giant cursor", fileName = "_ Giant Cursor",
        order = 3)]
    public sealed class GiantCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private GiantCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Giant;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}