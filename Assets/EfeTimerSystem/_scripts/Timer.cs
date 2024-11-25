using System;
using UnityEngine;
namespace EfeTimer
{

    public class Timer
    {
        // Timer süresi
        public float Duration { get; private set; }
        private float elapsedTime = 0f;

        // Timer durumu
        private bool isRunning = false;

        // Olaylar
        public event Action OnTimerStart;
        public event Action<float> OnTimerUpdate; // Geriye kalan süre
        public event Action OnTimerComplete;

        /// <summary>
        /// Timer'ı başlatır.
        /// </summary>
        /// <param name="duration">Süre (saniye)</param>
        public void StartTimer(float duration)
        {
            Duration = duration;
            elapsedTime = 0f;
            isRunning = true;
            OnTimerStart?.Invoke();
        }

        /// <summary>
        /// Timer'ı duraklatır.
        /// </summary>
        public void PauseTimer()
        {
            isRunning = false;
        }

        /// <summary>
        /// Timer'ı yeniden başlatır.
        /// </summary>
        public void ResumeTimer()
        {
            isRunning = true;
        }

        /// <summary>
        /// Timer'ı sıfırlar ve durdurur.
        /// </summary>
        public void ResetTimer()
        {
            isRunning = false;
            elapsedTime = 0f;
        }

        /// <summary>
        /// Timer'ın ilerlemesini sağlayan fonksiyon.
        /// Her çağrıldığında verilen deltaTime kadar süre ekler, olayları tetikler ve 
        /// timer tamamlanmışsa ilgili işlemleri gerçekleştirir.
        /// </summary>
        /// <param name="deltaTime">
        /// Timer'ı ilerletmek için geçen süre. Genellikle Time.deltaTime kullanılır.
        /// </param>
        public void Tick(float deltaTime)
        {
            if (!isRunning) return;

            elapsedTime += deltaTime;
            float remainingTime = Mathf.Max(Duration - elapsedTime, 0f);

            // Güncelleme olayını tetikle
            OnTimerUpdate?.Invoke(remainingTime);

            // Timer tamamlandıysa
            if (elapsedTime >= Duration)
            {
                isRunning = false;
                OnTimerComplete?.Invoke();
            }
        }
    }
}
