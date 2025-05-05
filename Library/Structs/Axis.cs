using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityTools.Library.Structs {
    [Serializable]
    public struct Axis : ISerializationCallbackReceiver {
        [field: SerializeField, ToggleLeft] private bool IsHomogeneous { get; set; }

        [field: SerializeField, HorizontalGroup("xAxis")]
        private double X { get; set; }

        [field: SerializeField, HorizontalGroup("yAxis"), DisableIf("IsHomogeneous")]
        private double Y { get; set; }

        public Axis(double x, double y) {
            IsHomogeneous = false;
            X = x;
            Y = y;
        }

        public float GetX() => (float)X;
        public float GetY() => (float)Y;

        public float GetIntX() => (int)X;
        public float GetIntY() => (int)Y;

        public float GetCombinedValue() => (float)(X * Y);
        public int GetCombinedValueInt() => (int)(X * Y);

        public Vector2 GetValues() =>
            IsHomogeneous ? new Vector2((float)X, (float)X) : new Vector2((float)X, (float)Y);

        public Vector2Int GetValuesInt() =>
            IsHomogeneous ? new Vector2Int((int)X, (int)X) : new Vector2Int((int)X, (int)Y);

        #region Editor

#if UNITY_EDITOR
        private const int buttonWidth = 20;
        private const int buttonHeight = 21;

        [HorizontalGroup("xAxis", Width = buttonWidth),
         Button("", ButtonHeight = buttonHeight, Icon = SdfIconType.ArrowClockwise)]
        public void ResetX() => X = 0;

        [HorizontalGroup("yAxis", Width = buttonWidth),
         DisableIf("IsHomogeneous"),
         Button("", ButtonHeight = buttonHeight, Icon = SdfIconType.ArrowClockwise)]
        public void ResetY() => Y = 0;

        [Button("Reset All values", ButtonHeight = buttonHeight, Stretch = false, ButtonAlignment = 1)]
        public void Reset() {
            ResetX();
            ResetY();
        }

        private void OnValidate() {
            if (IsHomogeneous) Y = X;
        }

        public void OnBeforeSerialize() => OnValidate();
        public void OnAfterDeserialize() { }
#endif

        #endregion
    }
}