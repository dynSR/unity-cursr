using System.Collections.Generic;
using CursR.Runtime.Enums;
using CursR.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.CursorAnimations {
    public class CursorAnimationConfig : ScriptableObject {
        [field: SerializeField] private CursorAnimationType Type { get; set; } = CursorAnimationType.Looping;

        [field: SerializeField, LabelText("At Speed"),
                Range(CursorUtils.CursorAnimationSpeedMinValue, 1.0f)]
        public float Speed { get; private set; } = 1.0f;

        [field: SerializeField, LabelText("With Frames"), Required]
        public List<Texture2D> Frames { get; private set; }

        public bool IsLooping() => Type == CursorAnimationType.Looping;

        #region Editor

#if UNITY_EDITOR
        [ContextMenu("Set Animation Speed To Minimum")]
        private void SetAnimationSpeedToMinAcceptedValue() => Speed = CursorUtils.CursorAnimationSpeedMinValue;
#endif

        #endregion
    }
}