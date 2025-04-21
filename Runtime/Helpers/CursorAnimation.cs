using System;
using System.Collections.Generic;
using CursR.Runtime.Enums;
using CursR.Runtime.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CursR.Runtime.Helpers {
    [Serializable]
    public struct CursorAnimation : ICursorProperty<List<Texture2D>> {
        [field: SerializeField, HideLabel] public CursorSize AssociatedCursorSize { get; private set; }

        [field: SerializeField, HideLabel, ListDrawerSettings(ShowFoldout = true)]
        public List<Texture2D> Value { get; private set; }
    }
}