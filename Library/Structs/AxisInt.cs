using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools.Library.Interfaces;

namespace UnityTools.Library.Structs {
    [Serializable]
    public struct AxisInt : IPairedValues<int, Vector2Int> {
        [field: SerializeField, ToggleLeft] public bool IsUniform { get; private set; }

        [field: SerializeField, HorizontalGroup("xAxis")]
        public int First { get; private set; }

        [field: SerializeField, HorizontalGroup("yAxis")]
        [field: DisableIf("IsUniform")]
        public int Second { get; private set; }

        public AxisInt(bool isUniform, int first, int second) {
            IsUniform = isUniform;
            First = first;
            Second = second;
        }

        public int GetCombinedValue() => First * Second;
        public Vector2Int GetValues() => IsUniform ? new Vector2Int(First, First) : new Vector2Int(First, Second);

        #region Editor

#if UNITY_EDITOR
        private const int buttonWidth = 20;
        private const int buttonHeight = 21;

        [HorizontalGroup("xAxis", Width = buttonWidth),
         Button("", ButtonHeight = buttonHeight, Icon = SdfIconType.ArrowClockwise)]
        public void ResetFirstValue() => First = 0;

        [HorizontalGroup("yAxis", Width = buttonWidth),
         DisableIf("IsUniform"),
         Button("", ButtonHeight = buttonHeight, Icon = SdfIconType.ArrowClockwise)]
        public void ResetSecondValue() => Second = 0;

        [Button("Reset All values", ButtonHeight = buttonHeight, Stretch = false, ButtonAlignment = 1)]
        public void Reset() {
            ResetFirstValue();
            ResetSecondValue();
        }

        private void OnValidate() {
            if (IsUniform) Second = First;
        }

        public void OnBeforeSerialize() => OnValidate();
        public void OnAfterDeserialize() { }
#endif

        #endregion
    }
}