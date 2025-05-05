using UnityTools.Library.Interfaces;
using CursorSystem.Runtime.ScriptableObjects;
using UnityEditor;

namespace CursorSystem.Editor {
    [CustomEditor(typeof(CursorAnimationConfig), true)]
    public class CursorAnimationConfigEditor : AnimationEditorPreviewer {
        protected override IAnimation GetAnimation() => (CursorAnimationConfig)target;
    }
}