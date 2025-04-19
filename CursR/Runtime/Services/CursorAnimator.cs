using CursR.Runtime.Helpers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityUtils;

namespace CursR.Runtime.Services {
    public class CursorAnimator {
        private Texture2D[] animationFrames;
        private Vector2 animationHotSpot = Vector2.zero;
        private int animationFrameIndex;
        private readonly LoopingTimer loopingTimer = new();

        public void OnEnable() => loopingTimer.AssignActionOnTimerFinished(PlayCursorAnimation);
        public void OnDisable() => loopingTimer.UnassignActionOnTimerFinished(PlayCursorAnimation);

        public void Update() => loopingTimer.Tick(Time.deltaTime);

        public void Init(Texture2D[] frames, float speed, Vector2 hotSpot) {
            Assert.IsTrue(speed >= CursorService.CursorAnimationSpeedMinValue);
            SetAnimationFrames(frames);
            SetAnimationHotSpot(hotSpot);
            loopingTimer.Reset(speed);
        }

        private void SetAnimationFrames(Texture2D[] newFrames) {
            Assert.IsFalse(newFrames.IsNullOrEmpty(), "No cursor animation frames provided");
            animationFrames = newFrames;
        }

        private void SetAnimationHotSpot(Vector2 newHotSpot) => animationHotSpot = newHotSpot;

        private void PlayCursorAnimation() => CursorService.SetCursorAppearance(
            GetCurrentAnimationFrame(animationFrames),
            animationHotSpot
        );

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