using UnityTools.Library.Interfaces;
using UnityTools.Systems.CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using UnityEditor;

namespace UnityTools.Systems.CursR.Editor {
    [CustomEditor(typeof(CursorAnimationConfig), true)]
    public class CursorAnimationConfigEditor : AnimationEditorPreviewer {
        protected override IAnimation GetAnimation() => (CursorAnimationConfig)target;
    }
}