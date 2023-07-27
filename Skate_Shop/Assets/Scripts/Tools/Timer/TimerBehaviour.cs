using UnityEngine;

namespace UnityTools
{
    public class TimerBehaviour : MonoBehaviour
    {
        [SerializeField] private Timer _timer;

        public bool isReady => _timer.isReady;

        public void Init(float duration)
        {
            _timer = new Timer(duration);
        }

        private void Update()
        {
            _timer.UpdateTimer();
        }

        public void ResetTimer()
        {
            _timer.Reset();
        }

        public void ResetTimer(float newDuration)
        {
            _timer.Reset(newDuration);
        }
    }
}