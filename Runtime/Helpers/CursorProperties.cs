using System;
using System.Collections.Generic;
using CursR.Runtime.Enums;
using CursR.Runtime.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace CursR.Runtime.Helpers {
    [Serializable]
    public class CursorProperties<T, TProperty> where T : ICursorProperty<TProperty> {
        [field: SerializeField,
                ListDrawerSettings(DraggableItems = false, ShowFoldout = false, HideRemoveButton = true)]
        private List<T> Elements { get; set; } = new(
            Enum.GetValues(typeof(CursorSize)).Length
        );

        public TProperty GetPropertyByCursorSize(CursorSize size) {
            Assert.IsFalse(Elements.Count == 0, "No cursor icon defined !");
            T foundProperty = Elements.Find(property => property.AssociatedCursorSize == size);
            return foundProperty.Get();
        }

        public int Count() => Elements.Count;
    }
}