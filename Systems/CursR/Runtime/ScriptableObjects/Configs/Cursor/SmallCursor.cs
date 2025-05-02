using UnityTools.Systems.CursR.Runtime.Enums;
using UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new small cursor", fileName = "_ Small Cursor",
        order = 0)]
    public sealed class SmallCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private SmallCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Small;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}