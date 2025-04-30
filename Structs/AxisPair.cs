using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityTools.Structs {
    [System.Serializable]
    public struct AxisPair {
        [SerializeField, ToggleLeft, HorizontalGroup("Gap")]
        private bool isHomogeneous;

        [SerializeField, HorizontalGroup("Gap"), MinValue(0)]
        private float x;

        [SerializeField, HorizontalGroup("Gap"), HideIf("isHomogeneous"), MinValue(0)]
        private float y;

        public Vector2 GetValue() => isHomogeneous ? new Vector2(x, x) : new Vector2(x, y);

        #region Editor

#if UNITY_EDITOR
        public void Reset() {
            x = 0;
            y = 0;
        }
#endif

        #endregion
    }
}