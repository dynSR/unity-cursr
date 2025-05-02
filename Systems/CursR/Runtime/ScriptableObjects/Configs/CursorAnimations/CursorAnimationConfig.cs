using System.Collections.Generic;
using UnityTools.Systems.CursR.Runtime.Enums;
using UnityTools.Library.Interfaces;
using UnityTools.Systems.CursR.Runtime.Utils;
using UnityTools.Library.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.CursorAnimations {
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