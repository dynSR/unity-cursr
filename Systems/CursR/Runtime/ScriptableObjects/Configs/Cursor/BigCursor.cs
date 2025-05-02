using UnityTools.Systems.CursR.Runtime.Enums;
using UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new big cursor", fileName = "_ Big Cursor",
        order = 2)]
    public sealed class BigCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private BigCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Big;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}