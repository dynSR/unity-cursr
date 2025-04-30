using UnityTools.Interfaces;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using UnityEditor;

namespace CursR.Editor {
    [CustomEditor(typeof(CursorAnimationConfig), true)]
    public class CursorAnimationConfigEditor : AnimationEditorPreviewer {
        protected override IAnimation GetAnimation() => ((CursorAnimationConfig)target);
    }
}