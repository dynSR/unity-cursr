using System.Collections.Generic;
using CursorSystem.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityTools.Library.Extensions;

namespace CursorSystem.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Game/Cursor System/Create new cursor library", fileName = "Cursor Library _")]
    public class CursorLibrary : ScriptableObject {
        [field: SerializeField, ListDrawerSettings(ShowFoldout = false)]
        private List<CursorConfig> Cursors { get; set; } = new();

        public CursorConfig GetCursorByTypeAndSize(CursorType type, CursorSize size) {
            Assert.IsFalse(IsEmpty(), "It seems that no cursor has been defined in the cursor's library");
            CursorConfig foundCursorConfig = Cursors.Find(cursor => cursor.Type == type && cursor.GetSize() == size);
            Assert.IsTrue(CursorExistsByType(type), "Cursor cannot be found with type: " + type);
            Assert.IsTrue(CursorExistsBySize(size), "Cursor cannot be found with size: " + size);
            return foundCursorConfig;
        }

        public CursorConfig GetCursorBySize(CursorSize size) {
            Assert.IsFalse(IsEmpty(), "It seems that no cursor has been defined in the cursor's library");
            CursorConfig foundCursorConfig = Cursors.Find(cursor => cursor.GetSize() == size);
            Assert.IsNotNull(foundCursorConfig, "Cursor cannot be found with size : " + size);
            return foundCursorConfig;
        }

        public CursorConfig GetCursorByType(CursorType type) {
            Assert.IsFalse(IsEmpty(), "It seems that no cursor has been defined in the cursor's library");
            CursorConfig foundCursorConfig = Cursors.Find(cursor => cursor.Type == type);
            Assert.IsNotNull(foundCursorConfig, "Cursor cannot be found with type : " + type);
            return foundCursorConfig;
        }

        private bool CursorExistsByType(CursorType type) => Cursors.Exists(c => c.Type == type);
        private bool CursorExistsBySize(CursorSize size) => Cursors.Exists(c => c.GetSize() == size);

        private bool IsEmpty() => Cursors.IsEmpty();

        #region Editor

#if UNITY_EDITOR
        private void OnValidate() => Cursors.RemoveWhere(c => c == null);
#endif

        #endregion
    }
}