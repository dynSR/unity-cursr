using UnityTools.Library.Interfaces;
using UnityEditor;
using UnityEngine;


namespace UnityTools.Systems.CursR.Editor {
    public abstract class AnimationEditorPreviewer : UnityEditor.Editor {
        private float lastTime;
        private int currentFrame;
        private int frameShift;

        protected abstract IAnimation GetAnimation();

        private void OnEnable() {
            EditorApplication.update += OnEditorUpdate;
            lastTime = (float)EditorApplication.timeSinceStartup;
        }

        private void OnDisable() {
            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate() {
            if (target == null || !GetAnimation().HasFrames()) return;

            float time = (float)EditorApplication.timeSinceStartup;
            float deltaTime = time - lastTime;

            if (deltaTime >= 1f / GetAnimation().FrameRate) {
                SetFrameIndex(
                    GetAnimation().Frames.ToArray(),
                    GetAnimation().IsLooping(),
                    ref currentFrame
                );
                lastTime = time;
                Repaint();
            }
        }

        private void SetFrameIndex(Texture2D[] frames, bool isLooping, ref int index) {
            if (isLooping) index = (index + 1) % frames.Length;
            else {
                if (IsAnimationCycleDone(index, frames.Length - 1)) frameShift = index == 0 ? 1 : -1;
                index = (index + frameShift) % frames.Length;
            }
        }

        private static bool IsAnimationCycleDone(int index, int maxFrames) => index == 0 || index == maxFrames;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            DrawAnimationPreview();
        }

        private void DrawAnimationPreview() {
            if (!GetAnimation().HasFrames()) return;

            Texture2D texture = GetAnimation().Frames[currentFrame];
            if (texture == null) return;

            EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);

            GUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(texture, GUILayout.Width(64), GUILayout.Height(64));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }
}