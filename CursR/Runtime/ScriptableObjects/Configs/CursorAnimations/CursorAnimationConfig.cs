using System.Collections.Generic;
using CursR.Runtime.Enums;
using UnityTools.Interfaces;
using CursR.Runtime.Utils;
using UnityTools.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects.Configs.CursorAnimations {
    public class CursorAnimationConfig : ScriptableObject, IAnimation {
        [field: SerializeField] private CursorAnimationType Type { get; set; } = CursorAnimationType.Looping;

        [field: SerializeField,
                Range(CursorUtils.CursorAnimationSpeedMinValue, 60)]
        public int FrameRate { get; private set; } = 1;

        [field: SerializeField, LabelText("With Frames"), Required]
        public List<Texture2D> Frames { get; private set; }

        public bool IsLooping() => Type == CursorAnimationType.Looping;

        #region Editor

#if UNITY_EDITOR
        [ContextMenu("Set Animation Speed To Minimum")]
        private void SetAnimationFrameRateToMin() => FrameRate = CursorUtils.CursorAnimationSpeedMinValue;

        private void OnValidate() => Frames.RemoveWhere(f => f == null);
#endif

        #endregion
    }
}