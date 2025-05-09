﻿using CursorSystem.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace CursorSystem.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Game/Cursor System/Configs/Cursor/Create new big cursor", fileName = "_ Big Cursor",
        order = 2)]
    public sealed class BigCursor : CursorConfig {
        [FormerlySerializedAs("cursorAnimation")] [SerializeField, ShowIf("isAnimated")]
        private BigCursorAnimation animation;

        public override CursorSize GetSize() => CursorSize.Big;
        public override CursorAnimationConfig GetAnimation() => animation;
    }
}