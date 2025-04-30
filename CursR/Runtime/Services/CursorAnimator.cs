using CursR.Runtime.Helpers;
using CursR.Runtime.ScriptableObjects.Configs.Cursor;
using CursR.Runtime.ScriptableObjects.Configs.CursorAnimations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityUtils;
using CursR.Runtime.Utils;

namespace CursR.Runtime.Services {
    public class CursorAnimator {
        private CursorConfig cursor;
        private int animationFrameIndex;
        private int frameShift = 1;

        private readonly LoopingTimer loopingTimer = new();

        public void OnEnable() => loopingTimer.AssignActionOnTimerFinished(PlayCursorAnimation);
        public void OnDisable() => loopingTimer.UnassignActionOnTimerFinished(PlayCursorAnimation);

        public void Update() => loopingTimer.Tick(Time.deltaTime);

        public void Init(CursorConfig cursorConfig) {
            cursor = cursorConfig;
            CursorAnimationConfig animation = cursor.GetAnimation();

            if (!loopingTimer.IsStopped()) loopingTimer.Reset(1f / animation.FrameRate);
            else loopingTimer.Start(animation.FrameRate);
        }

        public void Reset() {
            cursor = null;
            loopingTimer.Stop();
        }

        private void PlayCursorAnimation() =>
            CursorUtils.SetCursorAppearance(GetCurrentAnimationFrame(cursor.GetAnimation()), cursor.IsCentered);

        private Texture2D GetCurrentAnimationFrame(CursorAnimationConfig animation) {
            var frames = animation.Frames.ToArray();
            Assert.IsFalse(frames.IsNullOrEmpty(), "No cursor animation frames provided");

            Texture2D currentFrame = frames[animationFrameIndex];
            Assert.IsNotNull(currentFrame, "Current cursor animation frame is not defined");

            SetFrameIndex(frames, animation.IsLooping(), ref animationFrameIndex);
            return currentFrame;
        }

        private void SetFrameIndex(Texture2D[] frames, bool isLooping, ref int index) {
            if (isLooping) index = (index + 1) % frames.Length;
            else {
                if (IsAnimationCycleDone(index, frames.Length - 1)) frameShift = index == 0 ? 1 : -1;
                index = (index + frameShift) % frames.Length;
            }

            Assert.IsFalse(index < 0 && index >= frames.Length,
                "Error: frame index, must be between 0 and " + frames.Length);
        }

        private static bool IsAnimationCycleDone(int index, int maxFrames) => index == 0 || index == maxFrames;
    }
}