using System;

namespace CursR.Runtime.Helpers {
    public class LoopingTimer {
        private bool isRunning;
        private float initialDuration = 1f;
        private float currentTime;

        private Action onTimerFinished = delegate { };

        public void AssignActionOnTimerFinished(Action @event) => onTimerFinished += @event;
        public void UnassignActionOnTimerFinished(Action @event) => onTimerFinished -= @event;

        public void Tick(float deltaTime) {
            if (!isRunning) return;

            if (isRunning && IsFinished()) {
                Reset();
                onTimerFinished?.Invoke();
                return;
            }

            currentTime -= deltaTime;
        }

        private void Resume() => isRunning = true;
        private void Pause() => isRunning = false;

        public void Reset(float newDuration) {
            Pause();
            initialDuration = newDuration;
            Reset();
            Resume();
        }

        private void Reset() => currentTime = initialDuration;

        private bool IsFinished() => currentTime <= 0;
    }
}