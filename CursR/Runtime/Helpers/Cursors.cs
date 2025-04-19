using System;
using System.Collections.Generic;
using CursR.Runtime.Enums;
using UnityEngine;
using UnityEngine.Assertions;
using Cursor = CursR.Runtime.ScriptableObjects.Cursor;

namespace CursR.Runtime.Helpers {
    [Serializable]
    public class Cursors {
        [field: SerializeField] private List<Cursor> CursorPool { get; set; } = new();

        public Cursor GetCursorByType(CursorType type) {
            Assert.IsTrue(Enum.IsDefined(typeof(CursorType), type));
            return CursorPool.Find(cursor => cursor.Type == type);
        }
    }
}