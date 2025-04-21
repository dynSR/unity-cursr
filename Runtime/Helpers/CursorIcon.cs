using System;
using CursR.Runtime.Enums;
using CursR.Runtime.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.Helpers {
    [Serializable]
    public struct CursorIcon : ICursorProperty<Texture2D> {
        [field: SerializeField, HorizontalGroup, HideLabel]
        public CursorSize AssociatedCursorSize { get; private set; }

        [field: SerializeField, HorizontalGroup, HideLabel]
        public Texture2D Value { get; private set; }

        public Texture2D Get() => Value;
    }
}