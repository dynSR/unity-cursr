using System.Collections.Generic;
using CursR.Runtime.Enums;
using CursR.Runtime.Helpers;
using CursR.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Cursor/Animation", fileName = "New cursor animation")]
    public class CursorAnimationConfig : ScriptableObject {
        [field: SerializeField, Title("Animation Settings", bold: true),
                MinValue(CursorUtils.CursorAnimationSpeedMinValue),
                ContextMenuItem("Reset Animation Speed", "SetAnimationSpeedToMinValue")]
        private float AnimationSpeed { get; set; } = 1.0f;

        [field: SerializeField, PropertySpace(SpaceBefore = 11),
                InfoBox("To avoid problems, add a list of elements for each CursorSize in the game")]
        private CursorProperties<CursorAnimation, List<Texture2D>> AnimationFrames { get; set; } = new();

        public float GetAnimationSpeed() => AnimationSpeed;

        public List<Texture2D> GetAnimationFramesByCursorSize(CursorSize size) =>
            AnimationFrames.GetPropertyByCursorSize(size);

        #region Editor

#if UNITY_EDITOR
        [ContextMenu("SetAnimationSpeedToMinValue")]
        private void SetAnimationSpeedToMinValue() => AnimationSpeed = CursorUtils.CursorAnimationSpeedMinValue;
#endif

        #endregion
    }
}