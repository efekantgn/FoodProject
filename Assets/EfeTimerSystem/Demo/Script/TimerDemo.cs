using UnityEngine;
using UnityEngine.Events;
namespace EfeTimer
{
    public class TimerDemo : MonoBehaviour
    {
        public float Duration;

        public UnityEvent<string> OnTimerUpdate;
        public UnityEvent OnTimerComplete;

        private Timer timer;
        private float second;
        private void Awake()
        {
            timer = new Timer();
        }

        private void OnEnable()
        {
            timer.OnTimerUpdate += TimerUpdate;
            timer.OnTimerComplete += TimerComplete;
        }
        private void OnDisable()
        {
            timer.OnTimerUpdate -= TimerUpdate;
            timer.OnTimerComplete -= TimerComplete;
        }

        private void TimerComplete()
        {
            OnTimerComplete?.Invoke();
        }

        private void TimerUpdate(float context)
        {
            OnTimerUpdate?.Invoke(context.ToString("N0"));
        }

        private void Update()
        {
            timer.Tick(Time.deltaTime);
        }

        [ContextMenu("StartTimer")]
        public void StartTimer()
        {
            timer.StartTimer(Duration);
        }

        [ContextMenu("PauseTimer")]
        public void PauseTimer()
        {
            timer.PauseTimer();
        }

        [ContextMenu("ResumeTimer")]
        public void ResumeTimer()
        {
            timer.ResumeTimer();
        }

        [ContextMenu("ResetTimer")]
        public void ResetTimer()
        {
            timer.ResetTimer();
        }
    }
}
