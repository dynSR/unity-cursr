using System;

namespace CursR.Runtime.Helpers {
    public class LoopingTimer {
        private bool isRunning;
        private float initialDuration = 1f;
        private float currentTime;
        private bool isStopped;

        private Action onTimerFinished = delegate { };

        public void AssignActionOnTimerFinished(Action @event) => onTimerFinished += @event;
        public void UnassignActionOnTimerFinished(Action @event) => onTimerFinished -= @event;

        public void Tick(float deltaTime) {
            if (!isRunning) return;

            if (isRunning && IsFinished()) {
                ResetCurrentTime();
                onTimerFinished?.Invoke();
                return;
            }

            currentTime -= deltaTime;
        }

        public void Start(float duration) => Reset(duration);

        public void Stop() {
            isRunning = false;
            initialDuration = 1;
            isStopped = true;
        }

        public void Reset(float newDuration) {
            Pause();
            initialDuration = newDuration;
            isStopped = false;
            ResetCurrentTime();
            Resume();
        }

        private void Resume() => isRunning = true;
        private void Pause() => isRunning = false;


        private void ResetCurrentTime() => currentTime = initialDuration;

        private bool IsFinished() => IsStopped() || currentTime <= 0;
        public bool IsStopped() => isStopped;
    }
}