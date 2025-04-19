using CursR.Runtime.Helpers;
using CursR.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;
using UnityUtils;
using Cursor = CursR.Runtime.ScriptableObjects.Cursor;

namespace CursR.Runtime.Services {
    public class CursorAnimator {
        private Cursor animatedCursor;
        private Texture2D[] animationFrames;

        private int animationFrameIndex;
        private readonly LoopingTimer loopingTimer = new();

        public void OnEnable() => loopingTimer.AssignActionOnTimerFinished(PlayCursorAnimation);
        public void OnDisable() => loopingTimer.UnassignActionOnTimerFinished(PlayCursorAnimation);

        public void Update() => loopingTimer.Tick(Time.deltaTime);

        public void Init(Cursor cursor) {
            animatedCursor = cursor;
            CursorAnimation cursorAnimation = animatedCursor.GetAnimation();
            SetAnimationFrames(cursorAnimation.GetAnimationFrames().ToArray());
            loopingTimer.Reset(cursorAnimation.GetAnimationSpeed());
        }

        private void SetAnimationFrames(Texture2D[] newFrames) {
            Assert.IsNotNull(newFrames, "No cursor animation frames provided");
            animationFrames = newFrames;
        }

        private void PlayCursorAnimation() =>
            CursorService.SetCursorAppearance(GetCurrentAnimationFrame(animationFrames), animatedCursor.IsCentered);

        private Texture2D GetCurrentAnimationFrame(Texture2D[] frames) {
            Assert.IsFalse(frames.IsNullOrEmpty(), "No cursor animation frames provided");

            Texture2D currentFrame = frames[animationFrameIndex];
            Assert.IsNotNull(currentFrame, "Current cursor animation frame is not defined");
            IncrementFrameIndex(frames);
            return currentFrame;
        }

        private void IncrementFrameIndex(Texture2D[] frames) {
            animationFrameIndex = (animationFrameIndex + 1) % frames.Length;
            Assert.IsFalse(animationFrameIndex < 0, "Error: frame index, must be greater than 0");
            Assert.IsFalse(animationFrameIndex >= frames.Length,
                "Error: frame index, must be lower or equal to: " + frames.Length);
        }
    }
}