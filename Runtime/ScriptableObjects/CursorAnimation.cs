using System.Collections.Generic;
using CursR.Runtime.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Cursor/Animation", fileName = "New cursor animation")]
    public class CursorAnimation : ScriptableObject {
        [field: SerializeField, Title("Animation Settings", bold: true),
                MinValue(CursorService.CursorAnimationSpeedMinValue),
                ContextMenuItem("Reset Animation Speed", "SetAnimationSpeedToMinValue")]
        private float AnimationSpeed { get; set; } = 1.0f;

        [field: SerializeField, PropertySpace(SpaceBefore = 11)]
        private List<Texture2D> AnimationFrames { get; set; } = new();

        public float GetAnimationSpeed() => AnimationSpeed;
        public List<Texture2D> GetAnimationFrames() => AnimationFrames;

        #region Editor
#if UNITY_EDITOR
        [ContextMenu("SetAnimationSpeedToMinValue")]
        private void SetAnimationSpeedToMinValue() => AnimationSpeed = CursorService.CursorAnimationSpeedMinValue;
#endif
        #endregion
    }
}