using UnityTools.Systems.CursR.Runtime.Enums;
using UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.Cursor {
    [CreateAssetMenu(menuName = "Game/CursR/Configs/Cursor/Create new giant cursor", fileName = "_ Giant Cursor",
        order = 3)]
    public sealed class GiantCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private GiantCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Giant;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}