using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityTools.Structs {
    [System.Serializable]
    public struct AxisIntPair {
        [SerializeField, ToggleLeft, HorizontalGroup("Gap")]
        private bool isHomogeneous;

        [SerializeField, HorizontalGroup("Gap"), MinValue(0)]
        private int x;

        [SerializeField, HorizontalGroup("Gap"), HideIf("isHomogeneous"), MinValue(0)]
        private int y;

        public Vector2Int GetValue() => isHomogeneous ? new Vector2Int(x, x) : new Vector2Int(x, y);

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