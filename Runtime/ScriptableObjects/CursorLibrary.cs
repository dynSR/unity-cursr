using System.Collections.Generic;
using CursR.Runtime.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace CursR.Runtime.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Cursor/Library", fileName = "New cursor library")]
    public class CursorLibrary : ScriptableObject {
        [field: SerializeField, ListDrawerSettings(ShowFoldout = false)]
        private List<CursorConfig> Cursors { get; set; } = new();

        public CursorConfig GetCursorByType(CursorType type) {
            Assert.IsFalse(IsEmpty(), "It seems that no cursor has been defined in the cursor's library");
            CursorConfig foundCursorConfig = Cursors.Find(cursor => cursor.Type == type);
            Assert.IsNotNull(foundCursorConfig, "Cursor cannot be found with type : " + type);
            return foundCursorConfig;
        }

        private bool IsEmpty() => Cursors.Count == 0;
    }
}