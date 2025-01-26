using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private ProceduralImage _graphic;
    [SerializeField] private float _timerDuration = 5f; // Duration of the timer in seconds
    public Action OnTimerComplete; // Callback for when the timer completes

    private float _elapsedTime = 0f;
    private bool _timerRunning = true;

    void Update()
    {
        if (_timerRunning && _elapsedTime < _timerDuration)
        {
            _elapsedTime += Time.deltaTime;
            var fillAmount = Mathf.Clamp01(1 - _elapsedTime / _timerDuration);
            _graphic.fillAmount = fillAmount;

            if (_elapsedTime >= _timerDuration)
            {
                _timerRunning = false;
                OnTimerComplete?.Invoke(); // Invoke the callback

                Debug.Log("Timer complete, game over if no player has died");
            }
        }
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
        _timerRunning = true;
    }
}
